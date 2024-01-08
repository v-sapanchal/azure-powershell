using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    public class IgnorePropertiesResolver: DefaultContractResolver
    {
        private readonly List<string> _propertyNameToExclude;

        public IgnorePropertiesResolver(List<string> propertyNameToExclude)
        {
            _propertyNameToExclude = propertyNameToExclude;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (this._propertyNameToExclude.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }
            return property;
        }
    }
}
