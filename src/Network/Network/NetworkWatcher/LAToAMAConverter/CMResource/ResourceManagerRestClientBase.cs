using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource.Extensions;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceManagerRestClientBase
    {
        /// <summary>
        /// The azure http client wrapper to use.
        /// </summary>
        private readonly HttpClientHelper httpClientHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManagerRestClientBase"/> class.
        /// </summary>
        /// <param name="httpClientHelper">The azure http client wrapper to use.</param>
        public ResourceManagerRestClientBase(HttpClientHelper httpClientHelper)
        {
            this.httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result from the content of the response message.
        /// </summary>
        /// <typeparam name="TResponseType">The type of the result of response from the server.</typeparam>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<TResponseType> SendRequestAsync<TResponseType>(
            HttpMethod httpMethod,
            Uri requestUri,
            JObject content,
            CancellationToken cancellationToken)
        {
            using (var response = await
                SendRequestAsync(httpMethod: httpMethod, requestUri: requestUri, content: content, cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                return await response
                    .ReadContentAsJsonAsync<TResponseType>()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod httpMethod,
            Uri requestUri,
            JToken content,
            CancellationToken cancellationToken)
        {
            var contentString = content == null ? string.Empty : content.ToString();
            // minify JOSN payload to avoid payload too large error
            if (!string.IsNullOrEmpty(contentString))
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject(contentString);
                    contentString = JsonConvert.SerializeObject(obj, Formatting.None);
                }
                catch
                {
                    // leave contentString as it is
                }
            }
            using (var httpContent = new StringContent(content: contentString, encoding: Encoding.UTF8, mediaType: "application/json"))
            using (var request = new HttpRequestMessage(method: httpMethod, requestUri: requestUri) { Content = httpContent })
            {
                return await SendRequestAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <typeparam name="TResponseType">The type of the result of response from the server.</typeparam>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<TResponseType> SendRequestAsync<TResponseType>(
            HttpMethod httpMethod,
            Uri requestUri,
            CancellationToken cancellationToken)
        {
            using (var response = await
                SendRequestAsync(httpMethod: httpMethod, requestUri: requestUri, cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                return await response
                    .ReadContentAsJsonAsync<TResponseType>()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod httpMethod,
            Uri requestUri,
            CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(method: httpMethod, requestUri: requestUri))
            {
                return await SendRequestAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <param name="request">The http request to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            using (var httpClient = httpClientHelper.CreateHttpClient())
            {
                try
                {
                    var response = await httpClient
                        .SendAsync(request: request, cancellationToken: cancellationToken)
                        .ConfigureAwait(continueOnCapturedContext: false);

                    if (!response.StatusCode.IsSuccessfulRequest())
                    {
                        throw new Exception("Not IsSuccessfulRequest");
                    }

                    return response;
                }
                catch (Exception exception)
                {
                    if (exception is OperationCanceledException && !cancellationToken.IsCancellationRequested)
                    {
                        throw new Exception("ProjectResources.OperationFailedWithTimeOut");
                    }

                    throw;
                }
            }
        }
    }
}
