using Hyak.Common;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.WindowsAzure.Commands.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.Azure.Commands.Network
{
    public class NetworkWatcherUtility
    {
        /// <summary>
        /// Processes the parameters to return a valid resource Id.
        /// </summary>
        /// <param name="subscriptionId">The subscription.</param>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="resourceType">The resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="resourceName">The resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        /// <param name="extensionResourceType">The extension resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="extensionResourceName">The extension resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        public static string GetResourceId(Guid? subscriptionId, string resourceGroupName, string resourceType, string resourceName, string extensionResourceType = null, string extensionResourceName = null)
        {
            if (subscriptionId == null && !string.IsNullOrWhiteSpace(resourceGroupName))
            {
                throw new InvalidOperationException("A resource group cannot be specified without a subscription.");
            }

            var resourceId = new StringBuilder();

            if (subscriptionId != null)
            {
                resourceId.AppendFormat("/subscriptions/{0}", Uri.EscapeDataString(subscriptionId.Value.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(resourceGroupName))
            {
                resourceId.AppendFormat("/resourceGroups/{0}", Uri.EscapeDataString(resourceGroupName));
            }

            if (!string.IsNullOrWhiteSpace(resourceType))
            {
                resourceId.Append(ProcessResourceTypeAndName(resourceType: resourceType, resourceName: resourceName));
            }

            if (!string.IsNullOrWhiteSpace(extensionResourceType))
            {
                resourceId.Append(ProcessResourceTypeAndName(resourceType: extensionResourceType, resourceName: extensionResourceName));
            }

            return resourceId.ToString();
        }

        /// <summary>
        /// Processes a resource type string and a resource name string and 
        /// </summary>
        /// <param name="resourceType">The resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="resourceName">The resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        private static string ProcessResourceTypeAndName(string resourceType, string resourceName)
        {
            var resourceId = new StringBuilder();
            var resourceTypeTokens = resourceType.SplitRemoveEmpty('/');
            var resourceNameTokens = resourceName.CoalesceString().SplitRemoveEmpty('/');

            resourceId.AppendFormat("/providers/{0}", Uri.EscapeDataString(resourceTypeTokens.First()));

            for (int i = 1; i < resourceTypeTokens.Length; ++i)
            {
                resourceId.AppendFormat("/{0}", Uri.EscapeDataString(resourceTypeTokens[i]));

                if (resourceNameTokens.Length > i - 1)
                {
                    resourceId.AppendFormat("/{0}", Uri.EscapeDataString(resourceNameTokens[i - 1]));
                }
            }

            return resourceId.ToString();
        }

        /// <summary>
        /// Creates an <see cref="HttpClient"/>
        /// </summary>
        /// <param name="primaryHandlers">The handlers that will be added to the top of the chain.</param>
        public virtual HttpClient CreateHttpClient(params DelegatingHandler[] primaryHandlers)
        {
            var delegateHandlers = new DelegatingHandler[]
            {
                new AuthenticationHandler(cloudCredentials: credentials),
                new UserAgentHandler(headerValues: headerValues),
                new CmdletInfoHandler(cmdletHeaderValues: cmdletHeaderValues),
                new TracingHandler(),
                new RetryHandler(),
            };

            var pipeline = (HttpMessageHandler)(new HttpClientHandler());
            var reversedHandlers = primaryHandlers.CoalesceEnumerable().Concat(delegateHandlers).ToArray().Reverse();
            foreach (var handler in reversedHandlers)
            {
                handler.InnerHandler = pipeline;
                pipeline = handler;
            }

            return new HttpClient(pipeline);
        }
    }
}
