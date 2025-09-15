using System;
using MassTransit;

namespace Ar.Common.Transaction;

public static class PipeContextExtensions
    {
        public static bool HasPayloadType<T>(this PipeContext context, out T response) where T : class
        {
            var has = context.HasPayloadType(typeof(T));
            response = has ? context.GetPayload<T>(): default(T)!;
            return has;
        }
    }
