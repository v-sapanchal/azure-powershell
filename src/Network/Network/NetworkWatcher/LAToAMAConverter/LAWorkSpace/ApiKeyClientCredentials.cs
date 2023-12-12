using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public class ApiKeyClientCredentials : ServiceClientCredentials
    {
        private string token;

        public ApiKeyClientCredentials(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("token must not be null or empty");
            }

            this.token = token;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-api-key", token);
            return Task.FromResult(result: true);
        }
    }
}
