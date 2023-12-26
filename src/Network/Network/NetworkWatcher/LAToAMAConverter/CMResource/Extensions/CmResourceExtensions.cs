using Microsoft.WindowsAzure.Commands.Utilities.Common;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource.Extensions
{
    internal static class CmResourceExtensions
    {
        /// <summary>
        /// The type name of the generic resource.
        /// </summary>
        public static readonly string MicrosoftAzureResource = "Microsoft.Azure.Resource";

        /// <summary>
        /// Converts a <see cref="JToken"/> to a <see cref="CmResource{JToken}"/>.
        /// </summary>
        /// <param name="jtoken">The <see cref="JToken"/>.</param>
        internal static CmResource<JToken> ToResource(this JToken jtoken)
        {
            return jtoken.ToObject<CmResource<JToken>>(JsonExtensions.JsonMediaTypeSerializer);
        }

        /// <summary>
        /// Converts a <see cref="CmResource{JToken}"/> object into a <see cref="PSObject"/> object.
        /// </summary>
        /// <param name="resource">The <see cref="CmResource{JToken}"/> object.</param>
        internal static PSObject ToPsObject(this CmResource<JToken> resource)
        {
            var resourceType = string.IsNullOrEmpty(resource.Id)
                ? null
                : ResourceIdUtility.GetResourceType(resource.Id);

            var extensionResourceType = string.IsNullOrEmpty(resource.Id)
                ? null
                : ResourceIdUtility.GetExtensionResourceType(resource.Id);

            var objectDefinition = new Dictionary<string, object>
            {
                { "Name", resource.Name },
                { "ResourceId", string.IsNullOrEmpty(resource.Id) ? null : resource.Id },
                { "ResourceName", string.IsNullOrEmpty(resource.Id) ? null : ResourceIdUtility.GetResourceName(resource.Id) },
                { "ResourceType", resourceType },
                { "ExtensionResourceName", string.IsNullOrEmpty(resource.Id) ? null : ResourceIdUtility.GetExtensionResourceName(resource.Id) },
                { "ExtensionResourceType", extensionResourceType },
                { "Kind", resource.Kind },
                { "ResourceGroupName", string.IsNullOrEmpty(resource.Id) ? null : ResourceIdUtility.GetResourceGroupName(resource.Id) },
                { "Location", resource.Location },
                { "SubscriptionId", string.IsNullOrEmpty(resource.Id) ? null : ResourceIdUtility.GetSubscriptionId(resource.Id) },
                { "Tags", TagsHelper.GetTagsHashtable(resource.Tags) },
                { "Plan", resource.Plan.ToJToken().ToPsObject() },
                { "Properties", GetProperties(resource) },
                { "CreatedTime", resource.CreatedTime },
                { "ChangedTime", resource.ChangedTime },
                { "ETag", resource.ETag },
                { "Sku", resource.Sku.ToJToken().ToPsObject() },
                { "Identity", resource.Identity?.ToJToken().ToPsObject() }
            };

            var resourceTypeName = resourceType == null && extensionResourceType == null
                ? null
                : (resourceType + extensionResourceType).Replace('/', '.');

            var psObject =
                PowerShellUtilities.ConstructPSObject(
                resourceTypeName,
                objectDefinition.Where(kvp => kvp.Value != null).SelectManyArray(kvp => new[] { kvp.Key, kvp.Value }));

            psObject.TypeNames.Add(MicrosoftAzureResource);
            return psObject;
        }

        /// <summary>
        /// Gets the properties object
        /// </summary>
        /// <param name="resource">The <see cref="CmResource{JToken}"/> object.</param>
        private static object GetProperties(CmResource<JToken> resource)
        {
            if (resource.Properties == null)
            {
                return null;
            }

            return resource.Properties.ToPsObject();
        }
    }
}
