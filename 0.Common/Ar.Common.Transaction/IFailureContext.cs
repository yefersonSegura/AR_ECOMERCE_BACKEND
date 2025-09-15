using System;
using MassTransit;

namespace Ar.Common.Transaction;

public interface IFailureContext<T> : PipeContext where T : PipeContext
    {
        T WrappedContext { get; }

        bool IsValid { get; }

        IEnumerable<string> Errors { get; }

        IEnumerable<string> Warnings { get; }

        void AddError(string code, string message);

        void AddWarning(string code, string message);

        bool SendToApproval { get; set; }
    }
