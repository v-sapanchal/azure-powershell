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

        [Parameter(Mandatory = false, ParameterSetName = CommonConstants.ParamSetNameByWorkspaceId, HelpMessage = "The workspace ID.")]
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

            if (MMAWorkspaceConnectionMonitors?.Count() > 0)
            {
                //var cmList = MapPSMmaWorkspaceMachineConnectionMonitorToConnectionMonitorResult(MMAWorkspaceConnectionMonitors);
                // For LA work space logs query
                GetData(MMAWorkspaceConnectionMonitors);
                
                var allArcResources = await GetNetworkAgentLAWorkSpaceData(MMAWorkspaceConnectionMonitors);

                if (allArcResources?.Any(a => a != null) == true)
                {
                    var allArcResourcesHasData = allArcResources?.Where(w => w?.Tables?.Count > 0).SelectMany(s => s.Tables).Where(w => w.Rows.Count > 0);
                    //Need to refactor this code for distinct resource Id and take result
                    int noOfTakeResult = 100;
                    var getArcResourceIdsRows = allArcResourcesHasData?.SelectMany(s => s.Rows.Take(noOfTakeResult).Select(row => $"'{row[s.Columns.IndexOf(s.Columns.First(c => c.Name == "ResourceId"))]}'"));
                    string combinedArcIds = string.Join(", ", getArcResourceIdsRows);
                    string customQueryForArg = string.Format(CommonConstants.CustomQueryForArg, combinedArcIds);

                    // For ARG Query to get the ARC resource details
                    QueryForArg(customQueryForArg);
                }
                else
                {
                    WriteInformation($"No records", new string[] { "PSHOST" });
                }
            }
        }
    }
}
