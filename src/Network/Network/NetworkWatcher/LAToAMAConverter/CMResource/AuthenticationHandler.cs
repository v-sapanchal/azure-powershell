using Microsoft.Rest;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    /// <summary>
    /// The authentication handler.
    /// </summary>
    public class AuthenticationHandler : DelegatingHandler
    {
        /// <summary>
        /// The service client credentials.
        /// </summary>
        private readonly ServiceClientCredentials clientCredentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationHandler" /> class.
        /// </summary>
        /// <param name="cloudCredentials">The credentials.</param>
        public AuthenticationHandler(ServiceClientCredentials cloudCredentials)
        {
            clientCredentials = cloudCredentials;

        }

        /// <summary>
        /// Add the authentication token to the outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await clientCredentials
                .ProcessHttpRequestAsync(request: request, cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);

            return await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
