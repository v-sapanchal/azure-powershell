// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Ssh.Helpers.HybridCompute.Models
{
    using System.Linq;

    /// <summary>
    /// Reports the state and behavior of dependent services.
    /// </summary>
    public partial class ServiceStatuses
    {
        /// <summary>
        /// Initializes a new instance of the ServiceStatuses class.
        /// </summary>
        public ServiceStatuses()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ServiceStatuses class.
        /// </summary>

        /// <param name="extensionService">The state of the extension service on the Arc-enabled machine.
        /// </param>

        /// <param name="guestConfigurationService">The state of the guest configuration service on the Arc-enabled machine.
        /// </param>
        public ServiceStatuses(ServiceStatus extensionService = default(ServiceStatus), ServiceStatus guestConfigurationService = default(ServiceStatus))

        {
            this.ExtensionService = extensionService;
            this.GuestConfigurationService = guestConfigurationService;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets the state of the extension service on the Arc-enabled machine.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "extensionService")]
        public ServiceStatus ExtensionService {get; set; }

        /// <summary>
        /// Gets or sets the state of the guest configuration service on the
        /// Arc-enabled machine.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "guestConfigurationService")]
        public ServiceStatus GuestConfigurationService {get; set; }
    }
}