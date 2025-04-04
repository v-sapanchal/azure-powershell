// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Compute.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Describes the additional replica set information.
    /// </summary>
    public partial class AdditionalReplicaSet
    {
        /// <summary>
        /// Initializes a new instance of the AdditionalReplicaSet class.
        /// </summary>
        public AdditionalReplicaSet()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the AdditionalReplicaSet class.
        /// </summary>
        /// <param name="storageAccountType">Specifies the storage account type
        /// to be used to create the direct drive replicas. Possible values
        /// include: 'Standard_LRS', 'Standard_ZRS', 'Premium_LRS',
        /// 'PremiumV2_LRS'</param>
        /// <param name="regionalReplicaCount">The number of direct drive
        /// replicas of the Image Version to be created.This Property is
        /// updatable</param>
        public AdditionalReplicaSet(string storageAccountType = default(string), int? regionalReplicaCount = default(int?))
        {
            StorageAccountType = storageAccountType;
            RegionalReplicaCount = regionalReplicaCount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets specifies the storage account type to be used to
        /// create the direct drive replicas. Possible values include:
        /// 'Standard_LRS', 'Standard_ZRS', 'Premium_LRS', 'PremiumV2_LRS'
        /// </summary>
        [JsonProperty(PropertyName = "storageAccountType")]
        public string StorageAccountType { get; set; }

        /// <summary>
        /// Gets or sets the number of direct drive replicas of the Image
        /// Version to be created.This Property is updatable
        /// </summary>
        [JsonProperty(PropertyName = "regionalReplicaCount")]
        public int? RegionalReplicaCount { get; set; }

    }
}
