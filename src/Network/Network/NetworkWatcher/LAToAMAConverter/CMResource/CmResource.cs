using Microsoft.Azure.Commands.Common.Compute.Version_2018_04.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    public class CmResource<TProperties>
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public TProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets the id for the resource.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource definition.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Type { get; set; }

        /// <summary>  
        /// Gets or sets the resource <c>sku</c>.  
        /// </summary>  
        [JsonProperty(Required = Required.Default)]
        public ResourceSku Sku { get; set; }

        /// <summary>
        /// Gets or sets the kind of the resource definition.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the resource location.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the <c>etag</c>.
        /// </summary>
        [JsonProperty(Required = Required.Default, PropertyName = "etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the resource plan.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public ResourcePlan Plan { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the changed time.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public DateTime? ChangedTime { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public InsensitiveDictionary<string> Tags { get; set; }

        /// <summary>
        /// The identity assigned to the resource.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public ResourceIdentity Identity { get; set; }
    }
}
