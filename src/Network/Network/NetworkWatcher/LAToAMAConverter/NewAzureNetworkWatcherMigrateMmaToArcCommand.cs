using System;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Network.Models;
using System.Linq;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.Common.Authentication.ResourceManager;
using Microsoft.Azure.Management.Network.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    [Cmdlet("New", AzureRMConstants.AzureRMPrefix + "AzureNetworkWatcherMigrateMmaToArc"), OutputType(typeof(PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor))]
    public class NewAzureNetworkWatcherMigrateMmaToArcCommand : LaToAmaConnectionMonitorBaseCmdlet
    {

        private IAzureTokenCache _cache;
        private IProfileOperations _profile;

        /// <summary>
        /// Gets or sets the query.
        /// </summary>sub
        [Parameter(Mandatory = false, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Resource Graph query")]
        [AllowEmptyString]
        public string Query
        {
            get;
            set;
        }

        [Parameter(Mandatory = false, ParameterSetName = CommonConstants.ParamSetNameByWorkspaceId, HelpMessage = "The workspace ID.")]
        [ValidateNotNullOrEmpty]
        public string WorkspaceId { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The timespan to bound the query by, pass any number in hours.")]
        public int TimespanInHrs { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "List of MMA machine connection monitor.")]
        [ValidateNotNullOrEmpty]
        public PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor[] MMAWorkspaceConnectionMonitors { get; set; }

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

            if (MMAWorkspaceConnectionMonitors?.Count() > 0)
            {
                var cmWithArmEndpoints = MigrateCMs(MMAWorkspaceConnectionMonitors).GetAwaiter().GetResult();
                WriteObject(cmWithArmEndpoints);

                IList<ConnectionMonitorResult> outputCMs = cmWithArmEndpoints.Select(cm => MapPSMmaWorkspaceMachineConnectionMonitorToConnectionMonitorResult(cm)).ToList();

            }
        }
    }
}
