using Newtonsoft.Json.Serialization;
using System;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    /// <summary>
    /// Overrides the default CamelCase resolver to respect property name set in the <c>JsonPropertyAttribute</c>.
    /// </summary>
    internal class CamelCasePropertyNamesWithOverridesContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// Creates dictionary contract
        /// </summary>
        /// <param name="objectType">The object type.</param>
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            var contract = base.CreateDictionaryContract(objectType);

            // TODO: Remove IfDef code
#if NETSTANDARD
            contract.DictionaryKeyResolver = keyName => keyName;
#else
            contract.PropertyNameResolver = propertyName => propertyName;
#endif
            return contract;
        }
    }
}
