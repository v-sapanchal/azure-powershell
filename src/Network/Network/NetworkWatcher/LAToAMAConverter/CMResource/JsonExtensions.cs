using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public static class JsonExtensions
    {
        /// <summary>
        /// The JSON media type serializer.
        /// </summary>
        public static readonly JsonSerializer JsonMediaTypeSerializer = JsonSerializer.Create(JsonExtensions.MediaSerializationSettings);

        /// <summary>
        /// The JSON object type serializer.
        /// </summary>
        public static readonly JsonSerializer JsonObjectTypeSerializer = JsonSerializer.Create(JsonExtensions.ObjectSerializationSettings);

        /// <summary>
        /// The JSON object serialization settings.
        /// </summary>
        public static readonly JsonSerializerSettings ObjectSerializationSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesWithOverridesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new TimeSpanConverter(),
                new StringEnumConverter(new DefaultNamingStrategy()),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal },
            },
        };

        /// <summary>
        /// The JSON media serialization settings.
        /// </summary>
        public static readonly JsonSerializerSettings MediaSerializationSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesWithOverridesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new TimeSpanConverter(),
                new StringEnumConverter(new DefaultNamingStrategy()),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal },
            },
        };

        /// <summary>
        /// Checks if a conversion from the supplied <see cref="JToken"/> to a <typeparamref name="TType"/> can be made.
        /// </summary>
        /// <typeparam name="TType">The type to convert to.</typeparam>
        /// <param name="jobject">The <see cref="JObject"/>.</param>
        /// <param name="result">The result.</param>
        public static bool TryConvertTo<TType>(this JToken jobject, out TType result)
        {
            if (jobject == null)
            {
                result = default(TType);
                return true;
            }

            try
            {
                result = jobject.ToObject<TType>(JsonExtensions.JsonMediaTypeSerializer);
                return !object.Equals(result, default(TType));
            }
            catch (FormatException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (JsonException)
            {
            }

            result = default(TType);
            return false;
        }

        /// <summary>
        /// Serialize object to JToken.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static JToken ToJToken(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is PSObject psObject)
            {
                var jObject = new JObject();
                if (psObject.BaseObject is object[] psArray)
                {
                    var jArray = new JArray();
                    foreach (var item in psArray)
                    {
                        jArray.Add(item.ToJToken());
                    }

                    return jArray;
                }

                foreach (var property in psObject.Properties)
                {
                    jObject.Add(new JProperty(property.Name, property.Value.ToJToken()));
                }

                return jObject;
            }

            if (obj is PSMemberInfoCollection<PSPropertyInfo> psCollection)
            {
                var jObject = new JObject();
                foreach (var member in psCollection)
                {
                    jObject.Add(new JProperty(member.Name, member.Value.ToJToken()));
                }

                return jObject;
            }

            if (obj is object[] objArray)
            {
                var jArray = new JArray();
                foreach (var item in objArray)
                {
                    jArray.Add(item.ToJToken());
                }

                return jArray;
            }

            return JToken.FromObject(obj, JsonExtensions.JsonObjectTypeSerializer);
        }
    }
}
