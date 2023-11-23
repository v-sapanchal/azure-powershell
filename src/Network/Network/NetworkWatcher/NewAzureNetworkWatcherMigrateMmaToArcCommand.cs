namespace Microsoft.Azure.Commands.Network
{
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
    using System.Text;
    using System.Net.Http;
    using System.Dynamic;
    using Microsoft.Azure.Management.Network.Models;
    using Microsoft.Azure.Commands.Common.Strategies;
    using Microsoft.Azure.Management.Network;

    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "AzureNetworkWatcherMigrateMmaToArc"), OutputType(typeof(PSAzureNetworkWatcherMigrateMmaToArc))]
    public class NewAzureNetworkWatcherMigrateMmaToArcCommand : ConnectionMonitorBaseCmdlet
    {
        public Action<string> WarningLog;
        private IAzureTokenCache _cache;
        private IProfileOperations _profile;

        /// <summary>
        /// The cancellation source.
        /// </summary>
        private CancellationTokenSource cancellationSource = null;

        /// <summary>
        /// Gets the cancellation source.
        /// </summary>
        protected CancellationToken? CancellationToken
        {
            get
            {
                return this.cancellationSource == null ? new CancellationTokenSource().Token : (CancellationToken?)this.cancellationSource.Token;
            }
        }

        public ISubscriptionClientWrapper SubscriptionAndTenantClient = null;

        /// <summary>
        /// The endpoint that this client will communicate with.
        /// </summary>
        public Uri EndpointUri { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>sub
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Resource Graph query")]
        [AllowEmptyString]
        public string Query
        {
            get;
            set;
        }

        ///// <summary>
        ///// Gets or sets the Work Space Id.
        ///// </summary>sub
        //[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Work Space Id")]
        //[AllowEmptyString]
        //public string WorkSpaceId
        //{
        //    get;
        //    set;
        //}

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

            var endpointUri = new Uri(endpoint, UriKind.Absolute);
            EndpointUri = endpointUri;

            // Fetch all Subscriptions
            IEnumerable<AzureSubscription> subscriptions = GetAllSubscriptionsByUserContext();
            IEnumerable<ConnectionMonitorResourceDetail> allCMs = await GetConnectionMonitorBySubscriptions(subscriptions, "");
            IEnumerable<ConnectionMonitorResult> allCmHasMMAWorkspaceMachine = GetConnectionMonitorHasMMAWorkspaceMachineEndpoint(allCMs, "MMAWorkspaceMachine");
            WriteObject(allCmHasMMAWorkspaceMachine);

            // For Query for ARG
            // this.QueryForArg(this.Query);
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
            WriteObject(data);
        }

        public IEnumerable<AzureSubscription> GetAllSubscriptionsByUserContext()
        {
            List<ResponseWithContinuation<JObject[]>> cmResourceObjects = new List<ResponseWithContinuation<JObject[]>>();
            var tenantId = DefaultContext.Tenant.Id;
            return ListAllSubscriptionsForTenant(tenantId);
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<ConnectionMonitorResourceDetail>> GetConnectionMonitorBySubscriptions(IEnumerable<AzureSubscription> subscriptionsList, string workSpaceId)
        {
            List<ResponseWithContinuation<JObject[]>> cmResourceObjects = new List<ResponseWithContinuation<JObject[]>>();

            foreach (var subs in subscriptionsList)
            {
                cmResourceObjects.Add(await ListResourcesInSubscription(new Guid(subs.Id), "Microsoft.Network/networkWatchers/connectionMonitors", ""));
            }
            return ExtractCmResourceDetails(cmResourceObjects);
        }

        /// <summary>
        /// Get All the CMs which has MMAWorkspaceMachine as endpoint
        /// </summary>
        /// <param name="connectionMonitors">Basic details of CM like id, name , location, type</param>
        /// <param name="endpointType">endpointType = MMAWorkspaceMachine</param>
        public List<ConnectionMonitorResult> GetConnectionMonitorHasMMAWorkspaceMachineEndpoint(IEnumerable<ConnectionMonitorResourceDetail> connectionMonitors, string endpointType)
        {
            List<ConnectionMonitorResult> listCM = new List<ConnectionMonitorResult>();

            foreach (var cm in connectionMonitors)
            {
                var cmBasicDetails = this.GetConnectionMonitorDetails(cm.Id);
                listCM.Add(this.ConnectionMonitors.Get(cmBasicDetails.ResourceGroupName, cmBasicDetails.NetworkWatcherName, cmBasicDetails.ConnectionMonitorName));
            }

            return listCM.Where(w => w.Endpoints.Any(a => a.Type == endpointType)).ToList();
        }

        private IAccessToken AcquireAccessToken(
           IAzureAccount account,
           IAzureEnvironment environment,
           string tenantId,
           SecureString password,
           string promptBehavior,
           Action<string> promptAction,
           string resourceId = AzureEnvironment.Endpoint.ActiveDirectoryServiceEndpointResourceId)
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
                _cache,
                resourceId);
        }


        private IEnumerable<AzureSubscription> ListAllSubscriptionsForTenant(
          string tenantId)
        {
            IAzureAccount account = _profile.DefaultContext.Account;
            IAzureEnvironment environment = _profile.DefaultContext.Environment;
            SecureString password = null;
            string promptBehavior = ShowDialog.Never;
            IAccessToken accessToken = null;
            try
            {
                accessToken = AcquireAccessToken(account, environment, tenantId, password, promptBehavior, null);
            }
            catch (Exception e)
            {
                WriteWarningMessage(e.Message);
                //WriteDebugMessage(string.Format(ProfileMessages.UnableToAqcuireToken, tenantId, e.ToString()));
                return new List<AzureSubscription>();
            }

            return SubscriptionAndTenantClient?.ListAllSubscriptionsForTenant(accessToken, account, environment);
        }

        private void WriteWarningMessage(string message)
        {
            if (WarningLog != null)
            {
                WarningLog(message);
            }
        }

        //public List<AzureTenant> ListTenants(string tenant = "")
        //{
        //    IList<AzureTenant> tenants = ListAccountTenants(DefaultContext.Account, DefaultContext.Environment, null, ShowDialog.Never, null);
        //    return tenants.Where(t => string.IsNullOrEmpty(tenant) ||
        //                                 tenant.Equals(t.Id.ToString(), StringComparison.OrdinalIgnoreCase) ||
        //                                 Array.Exists(t.GetPropertyAsArray(AzureTenant.Property.Domains), e => tenant.Equals(e, StringComparison.OrdinalIgnoreCase)))
        //                         .ToList();
        //}

        //private List<AzureTenant> ListAccountTenants(
        //  IAzureAccount account,
        //  IAzureEnvironment environment,
        //  SecureString password,
        //  string promptBehavior,
        //  Action<string> promptAction)
        //{
        //    IList<AzureTenant> result = new List<AzureTenant>();
        //    var commonTenant = account.GetCommonTenant();
        //    try
        //    {
        //        var commonTenantToken = AcquireAccessToken(
        //            account,
        //            environment,
        //            commonTenant,
        //            password,
        //            promptBehavior,
        //            promptAction);

        //        result =  SubscriptionAndTenantClient?.ListAccountTenants(commonTenantToken, environment);
        //    }
        //    catch (Exception e)
        //    {
        //        WriteWarningMessage(string.Format(ProfileMessages.UnableToAqcuireToken, commonTenant, e.Message));
        //        WriteDebugMessage(string.Format(ProfileMessages.UnableToAqcuireToken, commonTenant, e.ToString()));
        //        if (account.IsPropertySet(AzureAccount.Property.Tenants))
        //        {
        //            result =
        //                account.GetPropertyAsArray(AzureAccount.Property.Tenants)
        //                    .Select(ti =>
        //                    {
        //                        var tenant = new AzureTenant();

        //                        Guid guid;
        //                        if (Guid.TryParse(ti, out guid))
        //                        {
        //                            tenant.Id = ti;
        //                        }
        //                        else
        //                        {
        //                            tenant.Directory = ti;
        //                        }

        //                        return tenant;
        //                    }).ToList();
        //        }
        //        if (!result.Any())
        //        {
        //            throw;
        //        }

        //    }

        //    return result.ToList();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmResourceObjects"></param>
        /// <returns></returns>
        private static List<ConnectionMonitorResourceDetail> ExtractCmResourceDetails(List<ResponseWithContinuation<JObject[]>> cmResourceObjects)
        {
            List<ConnectionMonitorResourceDetail> connectionMonitors = new List<ConnectionMonitorResourceDetail>();
            cmResourceObjects.ForEach(sub =>
            {
                if (sub?.Value?.Length > 0)
                {
                    foreach (JObject cm in sub.Value)
                    {
                        connectionMonitors.Add(new ConnectionMonitorResourceDetail
                        {
                            Id = cm["id"].Value<string>(),
                            Name = cm["name"].Value<string>(),
                            Location = cm["location"].Value<string>(),
                            Type = cm["type"].Value<string>()
                        });
                    }
                }
            });

            return connectionMonitors;
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

            return await this
                .GetResourcesClient()
                .ListResources<JObject>(
                    subscriptionId: SubscriptionId,
                    apiVersion: "2016-09-01",
                    filter: filterQuery,
                    cancellationToken: this.CancellationToken.Value)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Gets a new instance of the <see cref="ResourceManagerRestRestClient"/>.
        /// </summary>
        public ResourceManagerRestRestClient GetResourcesClient()
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
                        cmdletHeaderValues: this.GetCmdletHeaders()));
        }

        private Dictionary<string, string> GetCmdletHeaders()
        {
            return new Dictionary<string, string>
            {
                {"ParameterSetName", this.ParameterSetName },
                {"CommandName", this.CommandRuntime.ToString() }
            };
        }
    }

    /// <summary>
    /// Connection Monitor Resource Details
    /// </summary>
    public class ConnectionMonitorResourceDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }
    }

}
