
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

using AutoMapper;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication.ResourceManager;
using Microsoft.Azure.Commands.Network.Models;
using Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource;
using Microsoft.Azure.Commands.Profile.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Azure.Management.ResourceGraph.Models;
using Microsoft.Azure.Management.ResourceGraph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using MNM = Microsoft.Azure.Management.Network.Models;
using Microsoft.Azure.Management.OperationalInsights.Models;
using Microsoft.Azure.OperationalInsights;
using System.Collections;
using Microsoft.Azure.Commands.OperationalInsights.Client;
using Microsoft.Rest;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    public abstract class LaToAmaConnectionMonitorBaseCmdlet : ConnectionMonitorBaseCmdlet
    {
        private CancellationTokenSource cancellationSource = null;
        public Action<string> WarningLog;
        public ISubscriptionClientWrapper SubscriptionAndTenantClient = null;
        /// <summary>
        /// Gets the cancellation source.
        /// </summary>
        internal CancellationToken? CancellationToken
        {
            get
            {
                return cancellationSource == null ? new CancellationTokenSource().Token : cancellationSource.Token;
            }
        }

        private OperationalInsightsDataClient _operationalInsightsDataClient;
        internal OperationalInsightsDataClient OperationalInsightsDataClient
        {
            get
            {
                if (_operationalInsightsDataClient == null)
                {
                    ServiceClientCredentials clientCredentials = AzureSession.Instance.AuthenticationFactory.GetServiceClientCredentials(DefaultContext, AzureEnvironment.ExtendedEndpoint.OperationalInsightsEndpoint);

                    _operationalInsightsDataClient =
                        AzureSession.Instance.ClientFactory.CreateCustomArmClient<OperationalInsightsDataClient>(clientCredentials);
                    _operationalInsightsDataClient.Preferences.IncludeRender = false;
                    _operationalInsightsDataClient.Preferences.IncludeStatistics = false;
                    _operationalInsightsDataClient.NameHeader = "LogAnalyticsPSClient";

                    Uri targetUri = null;
                    DefaultContext.Environment.TryGetEndpointUrl(
                        AzureEnvironment.ExtendedEndpoint.OperationalInsightsEndpoint, out targetUri);
                    if (targetUri == null)
                    {
                        throw new Exception("Operational Insights is not supported in this Azure Environment");
                    }

                    _operationalInsightsDataClient.BaseUri = targetUri;

                    if (targetUri.AbsoluteUri.Contains("localhost"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    }
                }

                return _operationalInsightsDataClient;
            }
            set
            {
                _operationalInsightsDataClient = value;
            }
        }

        private OperationalInsightsClient operationalInsightsClient;
        internal OperationalInsightsClient OperationalInsightsClient
        {
            get
            {
                if (operationalInsightsClient == null)
                {
                    operationalInsightsClient = new OperationalInsightsClient(DefaultProfile.DefaultContext);
                }

                return operationalInsightsClient;
            }
            set
            {
                operationalInsightsClient = value;
            }
        }

        public IEnumerable<AzureSubscription> GetAllSubscriptionsByUserContext(IProfileOperations profile, IAzureTokenCache cache)
        {
            var tenantId = DefaultContext.Tenant.Id;
            return ListAllSubscriptionsForTenant(tenantId, profile, cache);
        }

        /// <summary>
        /// Get All the connection Monitors under user context subscriptions
        /// </summary>
        /// <param name="subscriptionsList">user context subscriptions</param>
        /// <returns>collection of all the ConnectionMonitor Resource Detail</returns>
        public IEnumerable<ConnectionMonitorResourceDetail> GetConnectionMonitorBySubscriptions(IEnumerable<AzureSubscription> subscriptionsList)
        {
            List<ConnectionMonitorResourceDetail> cmDetails = new List<ConnectionMonitorResourceDetail>();

            Parallel.ForEach(subscriptionsList, subs =>
            {
                PaginatedResponseHelper.ForEach(
                    getFirstPage: async () => await ListResourcesInSubscription(new Guid(subs.Id), CommonUtility.ConnectionMonitorResourceType, ""),
                    getNextPage: async nextLink => await GetNextLink<JObject>(nextLink),
                    cancellationToken: CancellationToken,
                    action: resources =>
                    {
                        if (resources.CoalesceEnumerable().FirstOrDefault().TryConvertTo(out CmResource<JToken> resource))
                        {
                            var genericResources = resources.CoalesceEnumerable().Where(res => res != null).SelectArray(res => res.ToResource());

                            foreach (var batch in genericResources.Batch())
                            {
                                var items = batch;
                                var powerShellObjects = items.SelectArray(genericResource => genericResource.ToJToken());
                                cmDetails.AddRange(ExtractCmResourceDetails(powerShellObjects.Select(s => s.ToObject<JObject>()).ToList()));
                            }
                        }
                        else
                        {
                            cmDetails.AddRange(ExtractCmResourceDetails(resources.CoalesceEnumerable().SelectArray(res => res.ToObject<JObject>()).ToList()));
                        }
                    });
            });

            return cmDetails;
        }

        public PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor MapConnectionMonitorResultToPSMmaWorkspaceMachineConnectionMonitor(ConnectionMonitorResult connectionMonitor)
        {
            PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor psMmaWorkspaceMachineConnectionMonitor = new PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor()
            {
                Name = connectionMonitor.Name,
                Id = connectionMonitor.Id,
                Etag = connectionMonitor.Etag,
                ProvisioningState = connectionMonitor.ProvisioningState,
                Type = connectionMonitor.Type,
                Location = connectionMonitor.Location,
                StartTime = connectionMonitor.StartTime,
                ConnectionMonitorType = connectionMonitor.ConnectionMonitorType,
                Endpoints = new List<PSNetworkWatcherConnectionMonitorEndpointObject>()
            };

            if (connectionMonitor.TestGroups != null)
            {
                foreach (ConnectionMonitorTestGroup testGroup in connectionMonitor.TestGroups)
                {
                    if (testGroup.Sources != null)
                    {
                        foreach (string sourceEndpointName in testGroup.Sources)
                        {
                            ConnectionMonitorEndpoint sourceEndpoint = GetEndpoinByName(connectionMonitor.Endpoints, sourceEndpointName);

                            PSNetworkWatcherConnectionMonitorEndpointObject EndpointObject =
                                NetworkResourceManagerProfile.Mapper.Map<PSNetworkWatcherConnectionMonitorEndpointObject>(sourceEndpoint);

                            psMmaWorkspaceMachineConnectionMonitor.Endpoints.Add(EndpointObject);
                        }
                    }

                    if (testGroup.Destinations != null)
                    {
                        foreach (string destinationEndpointName in testGroup.Destinations)
                        {
                            ConnectionMonitorEndpoint destinationEndpoint = GetEndpoinByName(connectionMonitor.Endpoints, destinationEndpointName);

                            PSNetworkWatcherConnectionMonitorEndpointObject EndpointObject =
                                NetworkResourceManagerProfile.Mapper.Map<PSNetworkWatcherConnectionMonitorEndpointObject>(destinationEndpoint);

                            psMmaWorkspaceMachineConnectionMonitor.Endpoints.Add(EndpointObject);
                        }
                    }
                }
            }

            return psMmaWorkspaceMachineConnectionMonitor;
        }

        public IEnumerable<ConnectionMonitorResult> MapPSMmaWorkspaceMachineConnectionMonitorToConnectionMonitorResult(IEnumerable<PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor> connectionMonitors)
        {
            List<ConnectionMonitorResult> cmResults = new List<ConnectionMonitorResult>();
            foreach (var connectionMonitor in connectionMonitors)
            {
                ConnectionMonitorResult connectionMonitorResult = new ConnectionMonitorResult(
                    connectionMonitor.Name, connectionMonitor.Id, connectionMonitor.Etag, connectionMonitor.Type,
                    connectionMonitor.Location);

                connectionMonitorResult.Endpoints = new List<ConnectionMonitorEndpoint>();

                if (connectionMonitor.Endpoints != null)
                {
                    foreach (PSNetworkWatcherConnectionMonitorEndpointObject endpoint in connectionMonitor.Endpoints)
                    {
                        ConnectionMonitorEndpoint EndpointObject =
                            NetworkResourceManagerProfile.Mapper.Map<ConnectionMonitorEndpoint>(endpoint);

                        connectionMonitorResult.Endpoints.Add(EndpointObject);

                    }
                }

                cmResults.Add(connectionMonitorResult);
            }

            return cmResults;
        }

        /// <summary>
        /// Get All the CMs which has MMAWorkspaceMachine as endpoint
        /// </summary>
        /// <param name="connectionMonitors">Basic details of CM like id, name , location, type</param>
        /// <param name="endpointType">endpointType = MMAWorkspaceMachine</param>
        public async Task<List<ConnectionMonitorResult>> GetConnectionMonitorHasMMAWorkspaceMachineEndpoint(IEnumerable<ConnectionMonitorResourceDetail> connectionMonitors, string endpointType)
        {
            List<Task<ConnectionMonitorResult>> listCM = new List<Task<ConnectionMonitorResult>>();
            foreach (var cm in connectionMonitors)
            {
                string subscriptionId = GetSubscriptionIdByResourceId(cm.Id);
                // Need to discuss, only changing subsid client.SetCurrentContext(subscriptionId, Tenant, name);
                if (DefaultContext.Subscription.Id != subscriptionId)
                {
                    DefaultContext.Subscription.Id = subscriptionId;
                    NetworkClient = new NetworkClient(DefaultContext);
                }
                ConnectionMonitorDetails cmBasicDetails = GetConnectionMonitorDetails(cm.Id);
                // WriteInformation($"{JsonConvert.SerializeObject(cmBasicDetails, Formatting.None)} and Subscription {subscriptionId}", new string[] { "PSHOST" });
                listCM.Add(ConnectionMonitors.GetAsync(cmBasicDetails.ResourceGroupName, cmBasicDetails.NetworkWatcherName, cmBasicDetails.ConnectionMonitorName));
            }

            var listConnectionMonitorResult = await Task.WhenAll(listCM);
            return listConnectionMonitorResult.Where(w => w.Endpoints.Any(a => a.Type == endpointType)).ToList();
        }

        /// <summary>
        /// Get the ARC resource details from connection monitor list(which contains MMAWorkspaceMachine endpoints)
        /// </summary>
        /// <param name="mmaMachineCMs">All CMs which contains MMAWorkspaceMachine endpoints</param>
        /// <returns>OperationalInsightsQueryResults data which contains ARC resource details</returns>
        public async Task<List<Azure.OperationalInsights.Models.QueryResults>> GetNetworkAgentLAWorkSpaceData(IEnumerable<ConnectionMonitorResult> mmaMachineCMs)
        {
            var cmEndPoints = mmaMachineCMs?.Select(s => s.Endpoints);
            var cmAllMMAEndpoints = cmEndPoints?.SelectMany(s => s.Where(w => w != null && w.Type == "MMAWorkspaceMachine"));
            var getDistinctWorkSpaceAndAddress = cmAllMMAEndpoints?.GroupBy(g => new { g.ResourceId, g.Address }).Select(s => s.FirstOrDefault());
            return await QueryForLaWorkSpaceNetworkAgentData(getDistinctWorkSpaceAndAddress);
        }

        public void QueryForArg(string query)
        {
            ResourceGraphClient rgClient = AzureSession.Instance.ClientFactory.CreateArmClient<ResourceGraphClient>(DefaultContext, AzureEnvironment.Endpoint.ResourceManager);
            QueryRequest request = new QueryRequest
            {
                Query = query
            };
            QueryResponse response = rgClient.Resources(request);
            var data = JsonConvert.DeserializeObject<object>(response.Data.ToString());
            WriteInformation($"Arc resources details:===============================\n{JsonConvert.SerializeObject(data, Formatting.Indented)}\n", new string[] { "PSHOST" });
        }

        /// <summary>
        /// Query for La work space for getting data by passing the Query or using hardcoded one
        /// </summary>
        public void QueryForLaWorkSpace(string workspaceId, string query)
        {
            IList<string> workspaces = new List<string>() { workspaceId };
            OperationalInsightsDataClient.WorkspaceId = workspaceId;
            var data = OperationalInsightsDataClient.Query(query ?? CommonUtility.Query, CommonUtility.TimeSpanForLAQuery, workspaces);
            var resultData = data.Results;
            //var tabularFormatData = PSQueryResponse.Create(data);
            WriteInformation($"{JsonConvert.SerializeObject(resultData.ToList(), Formatting.Indented)}\n", new string[] { "PSHOST" });
        }

        private async Task<List<Azure.OperationalInsights.Models.QueryResults>> QueryForLaWorkSpaceNetworkAgentData(IEnumerable<ConnectionMonitorEndpoint> allDistantCMEndpoints)
        {
            var endpointsGroupedBySubsAndRG = allDistantCMEndpoints?
                                             .GroupBy(g => new
                                             {
                                                 subs = NetworkWatcherUtility.GetSubscription(g.ResourceId),
                                                 rg = NetworkWatcherUtility.GetResourceValue(g.ResourceId, "/resourceGroups")
                                             })
                                             .OrderBy(g => g.Key.subs).ThenBy(g => g.Key.rg)
                                             .SelectMany(g => g);

            var arcResourceIdDetails = endpointsGroupedBySubsAndRG?.Select(addressToWorkSpace => GetNetworkingDataAsync(addressToWorkSpace))
                .Where(networkingData => networkingData != null);

            var getAllArcResourceDetails = await Task.WhenAll(arcResourceIdDetails);
            return getAllArcResourceDetails.ToList();
        }

        private async Task<Azure.OperationalInsights.Models.QueryResults> GetNetworkingDataAsync(ConnectionMonitorEndpoint addressToWorkSpace)
        {
            try
            {
                IList<string> workspaces = new List<string>() { addressToWorkSpace.ResourceId };
                string subscriptionId = NetworkWatcherUtility.GetSubscription(addressToWorkSpace.ResourceId);
                string workSpaceRG = NetworkWatcherUtility.GetResourceValue(addressToWorkSpace.ResourceId, "/resourceGroups");
                if (DefaultContext.Subscription.Id != subscriptionId)
                {
                    DefaultContext.Subscription.Id = subscriptionId;
                    _operationalInsightsDataClient = null;
                    operationalInsightsClient = null;
                }

                var listWorkspaces = OperationalInsightsClient.FilterPSWorkspaces(workSpaceRG, null);

                if (!listWorkspaces.Any(a => a.ResourceId == addressToWorkSpace?.ResourceId))
                {
                    WriteInformation($"Please remove or update this endpoint, this workspace resource '{addressToWorkSpace.ResourceId}' doesn't exist and it's being used in this endpoint.\n Endpoint Details :\n{JsonConvert.SerializeObject(addressToWorkSpace, Formatting.Indented)}\n", new string[] { "PSHOST" });
                    return null;
                }

                OperationalInsightsDataClient.WorkspaceId = addressToWorkSpace.ResourceId;
                return await OperationalInsightsDataClient.QueryAsync(CommonUtility.Query, CommonUtility.TimeSpanForLAQuery, workspaces);
            }
            catch (Exception ex)
            {
                WriteInformation($"This is error while performing on this resource Id {addressToWorkSpace.ResourceId}, Error:  {ex}", new string[] { "PSHOST" });
                return null;
            }
        }

        /// <summary>
        /// List All Subscriptions For User Tenant
        /// </summary>
        /// <param name="tenantId">User Tenant ID</param>
        /// <param name="profile">IProfileOperations object</param>
        /// <param name="cache">IAzureTokenCache object</param>
        /// <returns>collection of AzureSubscription</returns>
        private IEnumerable<AzureSubscription> ListAllSubscriptionsForTenant(string tenantId, IProfileOperations profile, IAzureTokenCache cache)
        {
            IAzureAccount account = profile.DefaultContext.Account;
            IAzureEnvironment environment = profile.DefaultContext.Environment;
            SecureString password = null;
            string promptBehavior = ShowDialog.Never;
            IAccessToken accessToken = null;
            try
            {
                accessToken = AcquireAccessToken(account, environment, tenantId, password, promptBehavior, null, cache);
            }
            catch (Exception e)
            {
                WriteWarningMessage(e.Message);
                //WriteDebugMessage(string.Format(ProfileMessages.UnableToAqcuireToken, tenantId, e.ToString()));
                return new List<AzureSubscription>();
            }

            return SubscriptionAndTenantClient?.ListAllSubscriptionsForTenant(accessToken, account, environment);
        }

        private IAccessToken AcquireAccessToken(IAzureAccount account, IAzureEnvironment environment, string tenantId, SecureString password,
           string promptBehavior, Action<string> promptAction, IAzureTokenCache cache, string resourceId = AzureEnvironment.Endpoint.ActiveDirectoryServiceEndpointResourceId)
        {
            if (account.Type == AzureAccount.AccountType.AccessToken)
            {
                tenantId = tenantId ?? account.GetCommonTenant();
                return new SimpleAccessToken(account, tenantId);
            }

            return AzureSession.Instance.AuthenticationFactory.Authenticate(
                account,
                environment,
                tenantId,
                password,
                promptBehavior,
                promptAction,
                cache,
                resourceId);
        }

        /// <summary>
        /// Write warning message
        /// </summary>
        /// <param name="message">warning message</param>
        private void WriteWarningMessage(string message)
        {
            if (WarningLog != null)
            {
                WarningLog(message);
            }
        }

        /// <summary>
        /// Gets the resources in a subscription.
        /// </summary>
        private async Task<ResponseWithContinuation<JObject[]>> ListResourcesInSubscription(Guid SubscriptionId, string ResourceType, string ODataQuery)
        {
            var filterQuery = QueryFilterBuilder
                .CreateFilter(
                    subscriptionId: SubscriptionId.ToString(),
                    resourceGroup: null,
                    resourceType: ResourceType,
                    resourceName: null,
                    tagName: null,
                    tagValue: null,
                    filter: ODataQuery);

            return await
                GetResourcesClient()
                .ListResources<JObject>(
                    subscriptionId: SubscriptionId,
                    apiVersion: "2016-09-01",
                    filter: filterQuery,
                    cancellationToken: CancellationToken.Value)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Gets the next set of resources using the <paramref name="nextLink"/>
        /// </summary>
        /// <param name="nextLink">The next link.</param>
        private Task<ResponseWithContinuation<TType[]>> GetNextLink<TType>(string nextLink)
        {
            return
                GetResourcesClient()
                .ListNextBatch<TType>(nextLink: nextLink, cancellationToken: CancellationToken.Value);
        }


        /// <summary>
        /// Gets a new instance of the <see cref="ResourceManagerRestRestClient"/>.
        /// </summary>
        private ResourceManagerRestRestClient GetResourcesClient()
        {
            var endpoint = DefaultContext.Environment.GetEndpoint(AzureEnvironment.Endpoint.ResourceManager);

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ApplicationException(
                    "The endpoint for the Azure Resource Manager service is not set. Please report this issue via GitHub or contact Microsoft customer support.");
            }

            var endpointUri = new Uri(endpoint, UriKind.Absolute);

            return new ResourceManagerRestRestClient(
                endpointUri: endpointUri,
                httpClientHelper: HttpClientHelperFactory.Instance
                .CreateHttpClientHelper(
                        credentials: AzureSession.Instance.AuthenticationFactory
                                                 .GetServiceClientCredentials(
                                                    DefaultContext,
                                                    AzureEnvironment.Endpoint.ResourceManager),
                        headerValues: AzureSession.Instance.ClientFactory.UserAgents,
                        cmdletHeaderValues: GetCmdletHeaders()));
        }

        /// <summary>
        /// Get Cmdlet Headers
        /// </summary>
        /// <returns>Dictionary of headers</returns>
        private Dictionary<string, string> GetCmdletHeaders()
        {
            return new Dictionary<string, string>
            {
                {"ParameterSetName", ParameterSetName },
                {"CommandName", CommandRuntime.ToString() }
            };
        }

        /// <summary>
        /// Extract CM Resource Details
        /// </summary>
        /// <param name="cmResourceObjects">collection of JObject of Cm resource</param>
        /// <returns>List of ConnectionMonitorResourceDetail></returns>
        private static List<ConnectionMonitorResourceDetail> ExtractCmResourceDetails(List<JObject> cmResourceObjects)
        {
            List<ConnectionMonitorResourceDetail> connectionMonitors = new List<ConnectionMonitorResourceDetail>();
            cmResourceObjects.ForEach(cm =>
            {
                connectionMonitors.Add(new ConnectionMonitorResourceDetail
                {
                    Id = cm["Id"]?.Value<string>(),
                    Name = cm["Name"]?.Value<string>(),
                    Location = cm["Location"]?.Value<string>(),
                    Type = cm["Type"]?.Value<string>()
                });
            });

            return connectionMonitors;
        }

        /// <summary>
        /// Get the subscription id from resource id
        /// </summary>
        /// <param name="resourceId">resource id</param>
        /// <returns>subscription id</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string GetSubscriptionIdByResourceId(string resourceId)
        {
            string[] array = resourceId.Split(new char[1] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length < 8)
            {
                throw new ArgumentException("Invalid format of the resource identifier.", "idFromServer");
            }

            return array[1];
        }
    }
}