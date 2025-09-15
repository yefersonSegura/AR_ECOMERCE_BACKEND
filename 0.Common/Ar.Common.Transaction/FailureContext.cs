using MassTransit;
using MassTransit.Middleware;

namespace Ar.Common.Transaction;

public class FailureContext<T> : BasePipeContext, IFailureContext<T> where T : PipeContext
{
    private readonly List<string> _validationMessages = new List<string>();
    public bool SendToApproval { get; set; }
    public FailureContext(T wrappedContext)
    {
        WrappedContext = wrappedContext;
    }

    public bool IsValid => TryGetPayload(out IEnumerable<string> errors) && errors.Any();

    public IEnumerable<string> Errors => _validationMessages.AsEnumerable();

    public IEnumerable<string> Warnings => _validationMessages.AsEnumerable();

    public T WrappedContext { get; }

    public void AddError(string code, string message)
    {
        if (!string.IsNullOrEmpty(code))
            _validationMessages.Add($"{code}-{message}");
        else
            _validationMessages.Add(message);
    }

    public void AddWarning(string code, string message)
    {
        if (!string.IsNullOrEmpty(code))
            _validationMessages.Add($"{code}-{message}");
        else
            _validationMessages.Add(message);
    }

    public FailureContext<T> WithError(string code, string message)
    {
        AddError(code, message);
        return this;
    }

    public FailureContext<T> WithWarning(string code, string message)
    {
        AddWarning(code, message);
        return this;
    }
}

