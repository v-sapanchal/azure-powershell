using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    /// <summary>
    /// The cmdlet info handler.
    /// </summary>
    public class CmdletInfoHandler : DelegatingHandler
    {
        /// <summary>
        /// The product info to add as headers.
        /// </summary>
        private readonly Dictionary<string, string> cmdletHeaderValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdletInfoHandler" /> class.
        /// </summary>
        /// <param name="cmdletHeaderValues">The product info to add as headers.</param>
        public CmdletInfoHandler(Dictionary<string, string> cmdletHeaderValues)
        {
            this.cmdletHeaderValues = cmdletHeaderValues;
        }

        /// <summary>
        /// Add the custom headers to the outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            foreach (KeyValuePair<string, string> kvp in cmdletHeaderValues)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }

            return await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
