using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceManagerRestRestClient : ResourceManagerRestClientBase
    {
        /// <summary>
        /// The endpoint that this client will communicate with.
        /// </summary>
        public Uri EndpointUri { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManagerRestRestClient"/> class.
        /// </summary>
        /// <param name="endpointUri">The endpoint that this client will communicate with.</param>
        /// <param name="httpClientHelper">The azure http client wrapper to use.</param>
        public ResourceManagerRestRestClient(Uri endpointUri, HttpClientHelper httpClientHelper)
            : base(httpClientHelper: httpClientHelper)
        {
            EndpointUri = endpointUri;
        }

        /// <summary>
        /// Lists all the resources under a single the subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription Id.</param>
        /// <param name="apiVersion">The API version to use.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="top">The number of resources to fetch.</param>
        /// <param name="filter">The filter query.</param>
        public Task<ResponseWithContinuation<TType[]>> ListResources<TType>(
            Guid subscriptionId,
            string apiVersion,
            CancellationToken cancellationToken,
            int? top = null,
            string filter = null)
        {
            var resourceId = ResourceIdUtility.GetResourceId(
                subscriptionId: subscriptionId,
                resourceGroupName: null,
                resourceType: null,
                resourceName: null);

            var requestUri = GetResourceManagementRequestUri(
                resourceId: resourceId,
                action: "resources",
                top: top == null ? null : string.Format("$top={0}", top.Value),
                odataQuery: string.IsNullOrWhiteSpace(filter) ? null : string.Format("$filter={0}", filter),
                apiVersion: apiVersion);

            return SendRequestAsync<ResponseWithContinuation<TType[]>>(
                httpMethod: HttpMethod.Get,
                requestUri: requestUri,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get the next batch of resource from a <see cref="ResponseWithContinuation{TType}"/> object's next link.
        /// </summary>
        /// <param name="nextLink">The next link used to get the rest of the results.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the request.</param>
        public Task<ResponseWithContinuation<TType[]>> ListNextBatch<TType>(
            string nextLink,
            CancellationToken cancellationToken)
        {
            var requestUri = new Uri(nextLink);

            return this.SendRequestAsync<ResponseWithContinuation<TType[]>>(
                httpMethod: HttpMethod.Get,
                requestUri: requestUri,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Generates a resource management request <see cref="Uri"/> based on the input parameters. Supports both subscription and tenant level resource and the condensed resource type and name format.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="apiVersion">The API version.</param>
        /// <param name="action">The action.</param>
        /// <param name="odataQuery">The OData query.</param>
        /// <param name="top">Top resources - used in list queries.</param>
        public Uri GetResourceManagementRequestUri(
            string resourceId,
            string apiVersion,
            string action = null,
            string odataQuery = null,
            string top = null)
        {
            var resourceIdStringBuilder = new StringBuilder(resourceId.CoalesceString().TrimEnd('/'));

            if (!string.IsNullOrWhiteSpace(action))
            {
                resourceIdStringBuilder.AppendFormat("/{0}", action);
            }

            var parts = new[]
            {
                top,
                odataQuery,
                string.Format("api-version={0}", apiVersion)
            };

            var queryString = parts.Where(part => !string.IsNullOrWhiteSpace(part)).ConcatStrings("&");

            resourceIdStringBuilder.AppendFormat("?{0}", queryString);

            var relativeUri = resourceIdStringBuilder.ToString()
                .Select(character => char.IsWhiteSpace(character) ? "%20" : character.ToString())
                .ConcatStrings();

            return new Uri(baseUri: EndpointUri, relativeUri: relativeUri);
        }
    }
}
