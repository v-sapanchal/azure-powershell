using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    internal class CustomDelegatingHandler : DelegatingHandler
    {
        internal const string InternalNameHeader = "csharpsdk";

        internal OperationalInsightsDataClient Client { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string text = "csharpsdk";
            if (!string.IsNullOrWhiteSpace(Client.NameHeader))
            {
                text = text + "," + Client.NameHeader;
            }

            request.Headers.Add("prefer", Client.Preferences.ToString());
            request.Headers.Add("x-ms-app", text);
            request.Headers.Add("x-ms-client-request-id", Client.RequestId ?? Guid.NewGuid().ToString());
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
