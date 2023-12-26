using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceSkuCosts
    {
        [JsonProperty(PropertyName = "meterID")]
        public string MeterID { get; private set; }

        [JsonProperty(PropertyName = "quantity")]
        public long? Quantity { get; private set; }

        [JsonProperty(PropertyName = "extendedUnit")]
        public string ExtendedUnit { get; private set; }

        public ResourceSkuCosts()
        {
        }

        public ResourceSkuCosts(string meterID = null, long? quantity = null, string extendedUnit = null)
        {
            MeterID = meterID;
            Quantity = quantity;
            ExtendedUnit = extendedUnit;
        }
    }
}
