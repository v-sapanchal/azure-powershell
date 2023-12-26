using Hyak.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource.Extensions;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    /// <summary>
    /// Tracing handler.
    /// </summary>
    public class TracingHandler : DelegatingHandler
    {
        /// <summary>
        /// Trace the outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!TracingAdapter.IsEnabled)
            {
                return await base.SendAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }

            var invocationId = TracingAdapter.NextInvocationId.ToString();
            try
            {
                TracingAdapter.SendRequest(invocationId: invocationId, request: request);
                var response = await base.SendAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
                TracingAdapter.ReceiveResponse(invocationId: invocationId, response: response);
                return response;
            }
            catch (Exception ex)
            {
                if (IsFatal(ex))
                {
                    throw;
                }

                TracingAdapter.Error(invocationId: invocationId, ex: ex);
                throw;
            }


        }

        /// <summary>
        /// Test if an exception is a fatal exception. 
        /// </summary>
        /// <param name="ex">Exception object.</param>
        public static bool IsFatal(Exception ex)
        {
            if (ex is AggregateException)
            {
                return ex.Cast<AggregateException>().Flatten().InnerExceptions.Any(exception => IsFatal(exception));
            }

            if (ex.InnerException != null && IsFatal(ex.InnerException))
            {
                return true;
            }

            return
                ex is TypeInitializationException ||
                ex is AppDomainUnloadedException ||
                ex is ThreadInterruptedException ||
                ex is AccessViolationException ||
                ex is InvalidProgramException ||
                ex is BadImageFormatException ||
                ex is StackOverflowException ||
                ex is ThreadAbortException ||
                ex is OutOfMemoryException ||
                ex is SecurityException ||
                ex is SEHException;
        }
    }
}
