// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.Network.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.Network.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.ResourceManager;
using Microsoft.Azure.Management.Internal.Resources.Models;
using Newtonsoft.Json;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    [Cmdlet("Get", AzureRMConstants.AzureRMPrefix + "NetworkWatcherMmaWorkspaceMachineConnectionMonitor", DefaultParameterSetName = "SetByName"), OutputType(typeof(PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor))]

    public class GetAzureNetworkWatcherMmaWorkspaceMachineConnectionMonitor : LaToAmaConnectionMonitorBaseCmdlet
    {
        private IAzureTokenCache _cache;
        private IProfileOperations _profile;

        /// <summary>
        /// Gets or sets the Work Space Id.
        /// </summary>sub
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Workspace Id, fetch only those CM resources which has LA workspace endpoints")]
        [AllowEmptyString]
        public string WorkSpaceId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Work Space Id.
        /// </summary>sub
        [Parameter(Mandatory = false, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Subscription Id, under this subscription you get all CM resources")]
        [AllowEmptyString]
        public string SubscriptionId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Work Space Id.
        /// </summary>sub
        [Parameter(Mandatory = false, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Region, fetch only those CM resources which comes under this region")]
        [AllowEmptyString]
        public string Region
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the CM Endpoint Type MMAWorkspaceNetwork or MMAWorkspaceMachine
        /// </summary>sub
        [Parameter(Mandatory = false, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "CMEndpointType, fetch only those CM resources has matched endpoint type")]
        [AllowEmptyString]
        public string CMEndpointType
        {
            get;
            set;
        }

        public override void Execute()
        {
            base.Execute();
            _cache = AzureSession.Instance.TokenCache;
            _profile = AzureRmProfileProvider.Instance.GetProfile<AzureRmProfile>();
            var profileClient = new RMProfileClient(_profile);
            SubscriptionAndTenantClient = profileClient.SubscriptionAndTenantClient;
            var endpoint = DefaultContext.Environment.GetEndpoint(AzureEnvironment.Endpoint.ResourceManager);
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ApplicationException(
                    "The endpoint for the Azure Resource Manager service is not set. Please report this issue via GitHub or contact Microsoft customer support.");
            }

            List<string> subscriptionIds = new List<string>();

            if (!string.IsNullOrEmpty(this.SubscriptionId))
            {
                subscriptionIds.Add(this.SubscriptionId);
            }
            else
            {
                // Fetch all Subscriptions
                subscriptionIds = GetAllSubscriptionsByUserContext(_profile, _cache)?.Select(s => s.Id).ToList();
            }

            IEnumerable<GenericResource> allCMs = GetConnectionMonitorBySubscriptions(subscriptionIds, this.Region);
            IEnumerable<ConnectionMonitorResult> allCmHasMMAWorkspaceMachine = GetConnectionMonitorHasMMAWorkspaceMachineEndpoint(allCMs, this.CMEndpointType ?? CommonConstants.MMAWorkspaceMachineEndpointResourceType, this.WorkSpaceId)?.GetAwaiter().GetResult();
            if (allCmHasMMAWorkspaceMachine?.Count() > 0)
            {
                WriteInformation($"Total number of Connection Monitors which has MMAWorkspace Endpoints : {allCmHasMMAWorkspaceMachine?.Count()}\n", new string[] { "PSHOST" });

                List<PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor> psMmaWorkspaceMachineConnectionMonitorList = new List<PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor>();
                foreach (var connectionMonitor in allCmHasMMAWorkspaceMachine)
                {
                    psMmaWorkspaceMachineConnectionMonitorList.Add(MapConnectionMonitorResultToPSMmaWorkspaceMachineConnectionMonitor(connectionMonitor));
                }
                WriteObject(psMmaWorkspaceMachineConnectionMonitorList, true);
                //WriteInformation($"List of Connection Monitors, which has MMAWorkspace endpoints :\n{JsonConvert.SerializeObject(allCmHasMMAWorkspaceMachine, Formatting.Indented)}\n", new string[] { "PSHOST" });
            }
            else
            {
                WriteInformation($"Connection Monitors don't have any MMAWorkspace Endpoints.\n", new string[] { "PSHOST" });
            }
        }
    }
}
