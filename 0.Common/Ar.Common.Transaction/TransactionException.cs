using System;

namespace Ar.Common.Transaction;

public class TransactionException : Exception
{
    public TransactionException()
    {
    }

    public TransactionException(string message) : base(message)
    {

    }
}
