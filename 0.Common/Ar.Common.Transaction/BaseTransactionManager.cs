using System;
using AR.Common.domain;
using AR.Common.Dto;
using AR.Core.Common.Interfaces;
using Autofac;
using MassTransit;

namespace Ar.Common.Transaction;

public abstract class BaseTransactionManager
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IComponentContext componentContext;
    protected BaseTransactionManager(IComponentContext componentContext, ITransactionRepository transactionRepository)
    {
        this.componentContext = componentContext;
        this.transactionRepository = transactionRepository;
    }
    public async Task<BaseResponseDto> Process<TDoc>(TDoc document)
    where TDoc : BaseDocument
    {
        if (document?.transaction == null)
            throw new TransactionException("Document/Transaction is null");

        TransactionContext context = new TransactionContext(document);
        return await ExecuteTransaction(document.transaction, context);
    }
    public async Task<ResponseDto<BaseDocument>> ExecuteTransaction(TransactionInfo transaction, TransactionContext context)
    {
        ResponseDto<BaseDocument> response = new();
        var modules = new List<TransactionActivityDto>();
        IPipe<TransactionContext> pipePre;
        try
        {
            var getDocumentType = await transactionRepository.GetDocumentTransaction(transaction.Operation.id, transaction.DocumentType.DocumentTypeCode);
            if (getDocumentType.Id == 0)
            {
                throw new TransactionException("Error al obtener el tipo de documento a generar");
            }
            transaction.DocumentType = getDocumentType;
            modules = await transactionRepository.GetActivities(transaction.Operation.id);
            pipePre = BuildPrePipeline<ITransactionContext>(modules);
            await pipePre.Send(context);

            if (context.HasPayloadType(out TransactionException expPost))
            {
                response.Message = "[Operation]:Error al procesar operación";
                response.Errors.Add(expPost.Message);
            }
            else
            {
                response.IsSuccessful = true;
                response.Message = "La operacion fue completada satisfactoriamente";
            }

            response.Data = context.Document;

            return response;
        }
        catch (Exception ex)
        {
            response.Status = ex.HResult;
            response.Message = ex.Message;
        }
        return response;
    }
    protected virtual IPipe<T> BuildPrePipeline<T>(List<TransactionActivityDto> modules)
    where T : class, ITransactionContext
    {
        var failedPipe = Pipe.New<IFailureContext<T>>(cfg =>
        {
            cfg.UseFilter(componentContext.Resolve<IFilter<IFailureContext<T>>>());
        });

        var pipe = Pipe.New<T>(cfg =>
        {
            foreach (var m in modules.OrderBy(s => s.Sort))
            {
                cfg.UseFilter(
                    componentContext.ResolveNamed<IFilter<T>>(
                        m.ActivityKey, new NamedParameter("failurePipe", failedPipe)));
            }
        });

        return pipe;
    }

}
