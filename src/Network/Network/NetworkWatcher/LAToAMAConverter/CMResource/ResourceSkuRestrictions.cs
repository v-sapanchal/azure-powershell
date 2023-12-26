using Microsoft.Azure.Commands.Common.Compute.Version_2018_04.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class ResourceSkuRestrictions
    {
        [JsonProperty(PropertyName = "type")]
        public ResourceSkuRestrictionsType? Type { get; private set; }

        [JsonProperty(PropertyName = "values")]
        public IList<string> Values { get; private set; }

        [JsonProperty(PropertyName = "restrictionInfo")]
        public ResourceSkuRestrictionInfo RestrictionInfo { get; private set; }

        [JsonProperty(PropertyName = "reasonCode")]
        public ResourceSkuRestrictionsReasonCode? ReasonCode { get; private set; }

        public ResourceSkuRestrictions()
        {
        }

        public ResourceSkuRestrictions(ResourceSkuRestrictionsType? type = null, IList<string> values = null, ResourceSkuRestrictionInfo restrictionInfo = null, ResourceSkuRestrictionsReasonCode? reasonCode = null)
        {
            Type = type;
            Values = values;
            RestrictionInfo = restrictionInfo;
            ReasonCode = reasonCode;
        }
    }
}
