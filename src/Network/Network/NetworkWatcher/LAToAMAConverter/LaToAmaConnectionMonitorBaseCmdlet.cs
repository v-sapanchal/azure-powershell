
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

            foreach (var subs in subscriptionsList)
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
                            //WriteInformation($"Connection Monitor Details : {JsonConvert.SerializeObject(cmDetails, Formatting.Indented)}\n", new string[] { "PSHOST" });
                        }
                    }
                    else
                    {
                        cmDetails.AddRange(ExtractCmResourceDetails(resources.CoalesceEnumerable().SelectArray(res => res.ToObject<JObject>()).ToList()));
                        //WriteInformation($"Connection Monitor Details : {JsonConvert.SerializeObject(cmDetails, Formatting.Indented)}\n", new string[] { "PSHOST" });
                    }
                });
            }

            return cmDetails;
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