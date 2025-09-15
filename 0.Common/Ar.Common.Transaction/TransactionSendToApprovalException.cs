using System;

namespace Ar.Common.Transaction;

public class TransactionSendToApprovalException : Exception
{
    public TransactionSendToApprovalException()
    {
    }

    public TransactionSendToApprovalException(string message) : base(message)
    {

    }
}
