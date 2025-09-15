using System;
using AR.Common.domain;
using AR.Common.Dto;

namespace Ar.Common.Transaction;

public interface ITransactionManager
{
    Task<BaseResponseDto> Process<TDoc>(TDoc document) where TDoc : BaseDocument;
}
