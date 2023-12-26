using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceSkuCapabilities
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; private set; }

        public ResourceSkuCapabilities()
        {
        }

        public ResourceSkuCapabilities(string name = null, string value = null)
        {
            Name = name;
            Value = value;
        }
    }
}
