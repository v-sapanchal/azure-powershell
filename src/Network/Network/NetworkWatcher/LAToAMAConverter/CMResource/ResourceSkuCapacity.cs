using Microsoft.Azure.Commands.Common.Compute.Version_2018_04.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceSkuCapacity
    {
        [JsonProperty(PropertyName = "minimum")]
        public long? Minimum { get; private set; }

        [JsonProperty(PropertyName = "maximum")]
        public long? Maximum { get; private set; }

        [JsonProperty(PropertyName = "default")]
        public long? DefaultProperty { get; private set; }

        [JsonProperty(PropertyName = "scaleType")]
        public ResourceSkuCapacityScaleType? ScaleType { get; private set; }

        public ResourceSkuCapacity()
        {
        }

        public ResourceSkuCapacity(long? minimum = null, long? maximum = null, long? defaultProperty = null, ResourceSkuCapacityScaleType? scaleType = null)
        {
            Minimum = minimum;
            Maximum = maximum;
            DefaultProperty = defaultProperty;
            ScaleType = scaleType;
        }
    }
}
