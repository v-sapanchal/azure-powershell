// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.Synapse.Models
{
    using System.Linq;

    /// <summary>
    /// Library response details
    /// </summary>
    [Microsoft.Rest.Serialization.JsonTransformation]
    public partial class LibraryResource : SubResource
    {
        /// <summary>
        /// Initializes a new instance of the LibraryResource class.
        /// </summary>
        public LibraryResource()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the LibraryResource class.
        /// </summary>

        /// <param name="id">Fully qualified resource ID for the resource. Ex -
        /// /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/{resourceProviderNamespace}/{resourceType}/{resourceName}
        /// </param>

        /// <param name="name">The name of the resource
        /// </param>

        /// <param name="type">The type of the resource. E.g. &#34;Microsoft.Compute/virtualMachines&#34; or
        /// &#34;Microsoft.Storage/storageAccounts&#34;
        /// </param>

        /// <param name="etag">Resource Etag.
        /// </param>

        /// <param name="propertiesName">Name of the library.
        /// </param>

        /// <param name="path">Storage blob path of library.
        /// </param>

        /// <param name="containerName">Storage blob container name.
        /// </param>

        /// <param name="uploadedTimestamp">The last update time of the library.
        /// </param>

        /// <param name="propertiesType">Type of the library.
        /// </param>

        /// <param name="provisioningStatus">Provisioning status of the library/package.
        /// </param>

        /// <param name="creatorId">Creator Id of the library/package.
        /// </param>
        public LibraryResource(string id = default(string), string name = default(string), string type = default(string), string etag = default(string), string propertiesName = default(string), string path = default(string), string containerName = default(string), System.DateTime? uploadedTimestamp = default(System.DateTime?), string propertiesType = default(string), string provisioningStatus = default(string), string creatorId = default(string))

        : base(id, name, type, etag)
        {
            this.PropertiesName = propertiesName;
            this.Path = path;
            this.ContainerName = containerName;
            this.UploadedTimestamp = uploadedTimestamp;
            this.PropertiesType = propertiesType;
            this.ProvisioningStatus = provisioningStatus;
            this.CreatorId = creatorId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets name of the library.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.name")]
        public string PropertiesName {get; set; }

        /// <summary>
        /// Gets or sets storage blob path of library.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.path")]
        public string Path {get; set; }

        /// <summary>
        /// Gets or sets storage blob container name.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.containerName")]
        public string ContainerName {get; set; }

        /// <summary>
        /// Gets the last update time of the library.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.uploadedTimestamp")]
        public System.DateTime? UploadedTimestamp {get; private set; }

        /// <summary>
        /// Gets or sets type of the library.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.type")]
        public string PropertiesType {get; set; }

        /// <summary>
        /// Gets provisioning status of the library/package.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.provisioningStatus")]
        public string ProvisioningStatus {get; private set; }

        /// <summary>
        /// Gets creator Id of the library/package.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.creatorId")]
        public string CreatorId {get; private set; }
    }
}