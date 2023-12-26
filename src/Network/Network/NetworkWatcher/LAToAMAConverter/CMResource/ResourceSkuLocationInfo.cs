using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceSkuLocationInfo
    {
        [JsonProperty(PropertyName = "location")]
        public string Location { get; private set; }

        [JsonProperty(PropertyName = "zones")]
        public IList<string> Zones { get; private set; }

        public ResourceSkuLocationInfo()
        {
        }

        public ResourceSkuLocationInfo(string location = null, IList<string> zones = null)
        {
            Location = location;
            Zones = zones;
        }
    }
}
