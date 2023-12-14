using Microsoft.Azure.Commands.ResourceManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public static class PaginatedResponseHelper
    {
        public static void ForEach<TType>(Func<Task<ResponseWithContinuation<TType[]>>> getFirstPage, Func<string, Task<ResponseWithContinuation<TType[]>>> getNextPage, CancellationToken? cancellationToken, Action<TType[]> action)
        {
            ResponseWithContinuation<TType[]> responseWithContinuation = null;
            while (cancellationToken.HasValue && !cancellationToken.Value.IsCancellationRequested && (responseWithContinuation == null || !string.IsNullOrWhiteSpace(responseWithContinuation.NextLink)))
            {
                cancellationToken.Value.ThrowIfCancellationRequested();
                responseWithContinuation = responseWithContinuation == null ? getFirstPage().Result : getNextPage(responseWithContinuation.NextLink).Result;
                if (responseWithContinuation == null)
                {
                    break;
                }

                action(responseWithContinuation.Value);
            }
        }

        public static TType[] Enumerate<TType>(Func<Task<ResponseWithContinuation<TType[]>>> getFirstPage, Func<string, Task<ResponseWithContinuation<TType[]>>> getNextPage, CancellationToken? cancellationToken)
        {
            List<TType> list = new List<TType>();
            ResponseWithContinuation<TType[]> responseWithContinuation = null;
            while (cancellationToken.HasValue && !cancellationToken.Value.IsCancellationRequested && (responseWithContinuation == null || !string.IsNullOrWhiteSpace(responseWithContinuation.NextLink)))
            {
                cancellationToken.Value.ThrowIfCancellationRequested();
                responseWithContinuation = responseWithContinuation == null ? getFirstPage().Result : getNextPage(responseWithContinuation.NextLink).Result;
                if (responseWithContinuation == null)
                {
                    return list.ToArray();
                }

                list.AddRange(responseWithContinuation.Value.Coalesce());
            }

            return list.ToArray();
        }

        public static IEnumerable<TSource> Coalesce<TSource>(this IEnumerable<TSource> source)
        {
            return source ?? Enumerable.Empty<TSource>();
        }
    }
}
