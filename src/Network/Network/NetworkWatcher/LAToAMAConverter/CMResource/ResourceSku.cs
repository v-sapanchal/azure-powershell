using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceSku
    {
        [JsonProperty(PropertyName = "resourceType")]
        public string ResourceType { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "tier")]
        public string Tier { get; private set; }

        [JsonProperty(PropertyName = "size")]
        public string Size { get; private set; }

        [JsonProperty(PropertyName = "family")]
        public string Family { get; private set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; private set; }

        [JsonProperty(PropertyName = "capacity")]
        public ResourceSkuCapacity Capacity { get; private set; }

        [JsonProperty(PropertyName = "locations")]
        public IList<string> Locations { get; private set; }

        [JsonProperty(PropertyName = "locationInfo")]
        public IList<ResourceSkuLocationInfo> LocationInfo { get; private set; }

        [JsonProperty(PropertyName = "apiVersions")]
        public IList<string> ApiVersions { get; private set; }

        [JsonProperty(PropertyName = "costs")]
        public IList<ResourceSkuCosts> Costs { get; private set; }

        [JsonProperty(PropertyName = "capabilities")]
        public IList<ResourceSkuCapabilities> Capabilities { get; private set; }

        [JsonProperty(PropertyName = "restrictions")]
        public IList<ResourceSkuRestrictions> Restrictions { get; private set; }

        public ResourceSku()
        {
        }

        public ResourceSku(string resourceType = null, string name = null, string tier = null, string size = null, string family = null, string kind = null, ResourceSkuCapacity capacity = null, IList<string> locations = null, IList<ResourceSkuLocationInfo> locationInfo = null, IList<string> apiVersions = null, IList<ResourceSkuCosts> costs = null, IList<ResourceSkuCapabilities> capabilities = null, IList<ResourceSkuRestrictions> restrictions = null)
        {
            ResourceType = resourceType;
            Name = name;
            Tier = tier;
            Size = size;
            Family = family;
            Kind = kind;
            Capacity = capacity;
            Locations = locations;
            LocationInfo = locationInfo;
            ApiVersions = apiVersions;
            Costs = costs;
            Capabilities = capabilities;
            Restrictions = restrictions;
        }
    }
}
