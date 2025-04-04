// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models
{
    using System.Linq;

    /// <summary>
    /// Cluster recovery point properties.
    /// </summary>
    public partial class ClusterRecoveryPointProperties
    {
        /// <summary>
        /// Initializes a new instance of the ClusterRecoveryPointProperties class.
        /// </summary>
        public ClusterRecoveryPointProperties()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ClusterRecoveryPointProperties class.
        /// </summary>

        /// <param name="recoveryPointTime">The recovery point time.
        /// </param>

        /// <param name="recoveryPointType">The recovery point type.
        /// Possible values include: &#39;NotSpecified&#39;, &#39;ApplicationConsistent&#39;,
        /// &#39;CrashConsistent&#39;</param>

        /// <param name="providerSpecificDetails">The provider specific details for the recovery point.
        /// </param>
        public ClusterRecoveryPointProperties(System.DateTime? recoveryPointTime = default(System.DateTime?), string recoveryPointType = default(string), ClusterProviderSpecificRecoveryPointDetails providerSpecificDetails = default(ClusterProviderSpecificRecoveryPointDetails))

        {
            this.RecoveryPointTime = recoveryPointTime;
            this.RecoveryPointType = recoveryPointType;
            this.ProviderSpecificDetails = providerSpecificDetails;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets the recovery point time.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "recoveryPointTime")]
        public System.DateTime? RecoveryPointTime {get; set; }

        /// <summary>
        /// Gets or sets the recovery point type. Possible values include: &#39;NotSpecified&#39;, &#39;ApplicationConsistent&#39;, &#39;CrashConsistent&#39;
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "recoveryPointType")]
        public string RecoveryPointType {get; set; }

        /// <summary>
        /// Gets or sets the provider specific details for the recovery point.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "providerSpecificDetails")]
        public ClusterProviderSpecificRecoveryPointDetails ProviderSpecificDetails {get; set; }
    }
}