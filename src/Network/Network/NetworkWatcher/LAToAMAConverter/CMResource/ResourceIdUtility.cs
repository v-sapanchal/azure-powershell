using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public static class ResourceIdUtility
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
        /// Gets a resource type
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="includeProviderNamespace">Indicates if the provider namespace should be included in the resource name.</param>
        public static string GetExtensionResourceType(string resourceId, bool includeProviderNamespace = true)
        {
            return ResourceIdUtility.GetExtensionResourceTypeOrName(resourceId: resourceId, includeProviderNamespace: includeProviderNamespace, getResourceName: false);
        }

        /// <summary>
        /// Gets either a resource type or resource Id based on the value of the <paramref name="getResourceName"/> parameter.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="getResourceName">When set to true returns a resource name, otherwise a resource type.</param>
        /// <param name="includeProviderNamespace">Indicates if the provider namespace should be included in the resource name.</param>
        private static string GetExtensionResourceTypeOrName(string resourceId, bool getResourceName, bool includeProviderNamespace = true)
        {
            return ResourceIdUtility.IsExtensionResourceId(resourceId)
                ? ResourceIdUtility.GetResourceTypeOrName(resourceId: resourceId, getResourceName: getResourceName, includeProviderNamespace: includeProviderNamespace, useLastSegment: true)
                : null;
        }

        /// <summary>
        /// Checks whether a resource id contains an extension resource.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        private static bool IsExtensionResourceId(string resourceId)
        {
            return resourceId
                .SplitRemoveEmpty('/')
                .Count(segment => segment.EqualsInsensitively("Providers")) == 2;
        }

        /// <summary>
        /// Gets either a resource type or resource Id based on the value of the <paramref name="getResourceName"/> parameter.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="getResourceName">When set to true returns a resource name, otherwise a resource type.</param>
        /// <param name="includeProviderNamespace">Indicates if the provider namespace should be included in the resource name.</param>
        /// <param name="useLastSegment">Seek the last segment instead of the first match.</param>
        private static string GetResourceTypeOrName(string resourceId, bool getResourceName, bool includeProviderNamespace = true, bool useLastSegment = false)
        {
            var substring = ResourceIdUtility.GetSubstringAfterSegment(
                resourceId: resourceId,
                segmentName: "Providers",
                selectLastSegment: useLastSegment);

            var segments = substring.CoalesceString().SplitRemoveEmpty('/');

            if (!segments.Any())
            {
                return null;
            }

            var providerNamespace = segments.First();

            var segmentString = segments.Skip(1)
                .TakeWhile(segment => !segment.EqualsInsensitively("Providers"))
                .Where((segment, index) => getResourceName ? index % 2 != 0 : index % 2 == 0)
                .ConcatStrings("/");

            return getResourceName
                ? segmentString
                : includeProviderNamespace
                    ? string.Format("{0}/{1}", providerNamespace, segmentString)
                    : segmentString;
        }

        /// <summary>
        /// Gets a the substring after a segment.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="segmentName">The segment name.</param>
        /// <param name="selectLastSegment">When set to true, gets the last segment (default) otherwise gets the first one.</param>
        private static string GetSubstringAfterSegment(string resourceId, string segmentName, bool selectLastSegment = true)
        {
            var segment = string.Format("/{0}/", segmentName.Trim('/').ToUpperInvariant());

            var index = selectLastSegment
                ? resourceId.LastIndexOf(segment, StringComparison.InvariantCultureIgnoreCase)
                : resourceId.IndexOf(segment, StringComparison.InvariantCultureIgnoreCase);

            return index < 0
                ? null
                : resourceId.Substring(index + segment.Length);
        }

        /// <summary>
        /// Gets a resource name
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        public static string GetResourceName(string resourceId)
        {
            return ResourceIdUtility.GetResourceTypeOrName(resourceId: resourceId, getResourceName: true);
        }

        /// <summary>
        /// Gets a resource name
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        public static string GetExtensionResourceName(string resourceId)
        {
            return ResourceIdUtility.GetExtensionResourceTypeOrName(resourceId: resourceId, getResourceName: true);
        }

        /// <summary>
        /// Gets the name of the resource group from the resource id.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        public static string GetResourceGroupName(string resourceId)
        {
            return ResourceIdUtility.GetNextSegmentAfter(resourceId: resourceId, segmentName: "ResourceGroups");
        }

        /// <summary>
        /// Gets the next segment after the one specified in <paramref name="segmentName"/>.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="segmentName">The segment name.</param>
        /// <param name="selectLastSegment">When set to true, gets the last segment (default) otherwise gets the first one.</param>
        private static string GetNextSegmentAfter(string resourceId, string segmentName, bool selectLastSegment = false)
        {
            var segment = ResourceIdUtility
                .GetSubstringAfterSegment(
                    resourceId: resourceId,
                    segmentName: segmentName,
                    selectLastSegment: selectLastSegment)
                .SplitRemoveEmpty('/')
                .FirstOrDefault();

            return string.IsNullOrWhiteSpace(segment)
                ? null
                : segment;
        }

        /// <summary>
        /// Gets the subscription id from the resource id.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        public static string GetSubscriptionId(string resourceId)
        {
            return ResourceIdUtility.GetNextSegmentAfter(resourceId: resourceId, segmentName: "Subscriptions");
        }

        /// <summary>
        /// Gets a resource type
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="includeProviderNamespace">Indicates if the provider namespace should be included in the resource name.</param>
        public static string GetResourceType(string resourceId, bool includeProviderNamespace = true)
        {
            return ResourceIdUtility.GetResourceTypeOrName(resourceId: resourceId, includeProviderNamespace: includeProviderNamespace, getResourceName: false);
        }
    }
}
