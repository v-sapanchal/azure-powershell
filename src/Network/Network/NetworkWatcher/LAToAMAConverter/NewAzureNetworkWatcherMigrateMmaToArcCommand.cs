using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Network.Models;
using Microsoft.Azure.Management.ResourceGraph;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Security;
using Microsoft.Azure.Commands.Profile.Models;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.Common.Authentication.ResourceManager;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Azure.Commands.Common.Strategies;
using Microsoft.Azure.Management.Network;
using Microsoft.Rest;
using System.Net;
using Microsoft.Azure.Commands.OperationalInsights.Client;
using Microsoft.Azure.OperationalInsights;
using Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource;
using PaginatedResponseHelper = Microsoft.Azure.Commands.ResourceManager.Common.PaginatedResponseHelper;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    [Cmdlet("New", AzureRMConstants.AzureRMPrefix + "AzureNetworkWatcherMigrateMmaToArc"), OutputType(typeof(PSAzureNetworkWatcherMigrateMmaToArc))]
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

        [Parameter(Mandatory = false, ParameterSetName = CommonUtility.ParamSetNameByWorkspaceId, HelpMessage = "The workspace ID.")]
        [ValidateNotNullOrEmpty]
        public string WorkspaceId { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The timespan to bound the query by, pass any number in hours.")]
        public int TimespanInHrs { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "List of MMA machine connection monitor.")]
        [ValidateNotNullOrEmpty]
        public PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor[] MMAWorkspaceConnectionMonitors { get; set; }

        public override async void Execute()
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

            //var endpointUri = new Uri(endpoint, UriKind.Absolute);
            //EndpointUri = endpointUri;

            //// Fetch all Subscriptions
            //IEnumerable<AzureSubscription> subscriptions = GetAllSubscriptionsByUserContext();
            //IEnumerable<ConnectionMonitorResourceDetail> allCMs = GetConnectionMonitorBySubscriptions(subscriptions);
            //IEnumerable<ConnectionMonitorResult> allCmHasMMAWorkspaceMachine = await GetConnectionMonitorHasMMAWorkspaceMachineEndpoint(allCMs, "MMAWorkspaceMachine");
            //if (allCmHasMMAWorkspaceMachine?.Count() > 0)
            //{
            //    WriteInformation($"Total number of Connection Monitors which has MMAWorkspace Endpoints : {allCmHasMMAWorkspaceMachine?.Count()}\n", new string[] { "PSHOST" });
            //    WriteInformation($"List of Connection Monitors, which has MMAWorkspace endpoints :\n{JsonConvert.SerializeObject(allCmHasMMAWorkspaceMachine, Formatting.Indented)}\n", new string[] { "PSHOST" });
            //}
            //else
            //{
            //    WriteInformation($"Connection Monitors don't have any MMAWorkspace Endpoints.\n", new string[] { "PSHOST" });
            //}

            if (MMAWorkspaceConnectionMonitors?.Count() > 0)
            {
                var cmList = MapPSMmaWorkspaceMachineConnectionMonitorToConnectionMonitorResult(MMAWorkspaceConnectionMonitors);
                // For LA work space logs query
                var allArcResources = await GetNetworkAgentLAWorkSpaceData(cmList);
                var allArcResourcesHasData = allArcResources.Where(w => w?.Tables?.Count > 0).SelectMany(s => s.Tables).Where(w => w.Rows.Count > 0);
                // WriteInformation($"{JsonConvert.SerializeObject(allArcResourcesHasData.Select(s => s.Rows.Take(100)), Formatting.None)}", new string[] { "PSHOST" });

                int noOfTakeResult = 100;
                //Need to refactor this code for distinct resource Id
                var getArcResourceIdsRows = allArcResourcesHasData.SelectMany(s => s.Rows.Take(noOfTakeResult).Select(row => $"'{row[s.Columns.IndexOf(s.Columns.First(c => c.Name == "ResourceId"))]}'"));
                string combinedArcIds = string.Join(", ", getArcResourceIdsRows);
                string customQueryForArg = string.Format(CommonUtility.CustomQueryForArg, combinedArcIds);

                // For ARG Query to get the ARC resource details
                QueryForArg(customQueryForArg);
            }
        }
    }
}
