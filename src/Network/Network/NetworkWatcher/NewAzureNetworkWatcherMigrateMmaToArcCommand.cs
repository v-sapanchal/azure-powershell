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

    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "AzureNetworkWatcherMigrateMmaToArc"), OutputType(typeof(PSAzureNetworkWatcherMigrateMmaToArc))]
    public class NewAzureNetworkWatcherMigrateMmaToArcCommand : NetworkWatcherBaseCmdlet
    {
        public Action<string> WarningLog;
        private IAzureTokenCache _cache;
        private IProfileOperations _profile;

        public ISubscriptionClientWrapper SubscriptionAndTenantClient = null;
        
        /// <summary>
        /// The endpoint that this client will communicate with.
        /// </summary>
        public Uri EndpointUri { get; set; }

        /// <summary>
        /// The azure http client wrapper to use.
        /// </summary>
        private readonly HttpClientHelper httpClientHelper;

        /// <summary>
        /// Gets or sets the query.
        /// </summary>s
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Resource Graph query")]
        [AllowEmptyString]
        public string Query
        {
            get;
            set;
        }

        ///// <summary>
        ///// Gets or sets the Work Space Id.
        ///// </summary>s
        //[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Work Space Id")]
        //[AllowEmptyString]
        //public string WorkSpaceId
        //{
        //    get;
        //    set;
        //}

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

            var endpointUri = new Uri(endpoint, UriKind.Absolute);
            EndpointUri = endpointUri;

            GetAllSubscriptionsByUserContext();
            this.QueryForArg(this.Query);
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
        public void GetAllSubscriptionsByUserContext()
        {
            var tenantId = DefaultContext.Tenant.Id;
            IEnumerable<AzureSubscription> subscriptionsList = ListAllSubscriptionsForTenant(tenantId);
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
        public void GetConnectionMonitorHasMMAMachineEndpoint(IEnumerable<AzureSubscription> subscriptions, string workSpaceId)
        {

        }

        /// <summary>
        /// Gets the resources in a subscription.
        /// </summary>
        private async Task<ResponseWithContinuation<JObject[]>> ListResourcesInSubscription()
        {
            var filterQuery = QueryFilterBuilder
                .CreateFilter(
                    subscriptionId: null,
                    resourceGroup: null,
                    resourceType: this.ResourceType,
                    resourceName: this.Name,
                    tagName: null,
                    tagValue: null,
                    filter: this.ODataQuery);

            return await this
                .GetResourcesClient()
                .ListResources<JObject>(
                    subscriptionId: this.SubscriptionId.Value,
                    apiVersion: this.DefaultApiVersion,
                    filter: filterQuery,
                    cancellationToken: this.CancellationToken.Value)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        ///// <summary>
        ///// Gets a new instance of the <see cref="ResourceManagerRestRestClient"/>.
        ///// </summary>
        //public ResourceManagerRestRestClient GetResourcesClient()
        //{
        //    var endpoint = DefaultContext.Environment.GetEndpoint(AzureEnvironment.Endpoint.ResourceManager);

        //    if (string.IsNullOrWhiteSpace(endpoint))
        //    {
        //        throw new ApplicationException(
        //            "The endpoint for the Azure Resource Manager service is not set. Please report this issue via GitHub or contact Microsoft customer support.");
        //    }

        //    var endpointUri = new Uri(endpoint, UriKind.Absolute);

        //    return new ResourceManagerRestRestClient(
        //        endpointUri: endpointUri,
        //        httpClientHelper: HttpClientHelperFactory.Instance
        //        .CreateHttpClientHelper(
        //                credentials: AzureSession.Instance.AuthenticationFactory
        //                                         .GetServiceClientCredentials(
        //                                            DefaultContext,
        //                                            AzureEnvironment.Endpoint.ResourceManager),
        //                headerValues: AzureSession.Instance.ClientFactory.UserAgents,
        //                cmdletHeaderValues: this.GetCmdletHeaders()));
        //}

        /// <summary>
        /// Lists all the resources under a single the subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription Id.</param>
        /// <param name="apiVersion">The API version to use.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="top">The number of resources to fetch.</param>
        /// <param name="filter">The filter query.</param>
        public Task<ResponseWithContinuation<TType[]>> ListResources<TType>(
            Guid subscriptionId,
            string apiVersion,
            CancellationToken cancellationToken,
            int? top = null,
            string filter = null)
        {
            var resourceId = NetworkWatcherUtility.GetResourceId(
                subscriptionId: subscriptionId,
                resourceGroupName: null,
                resourceType: null,
                resourceName: null);

            var requestUri = this.GetResourceManagementRequestUri(
                resourceId: resourceId,
                action: "resources",
                top: top == null ? null : string.Format("$top={0}", top.Value),
                odataQuery: string.IsNullOrWhiteSpace(filter) ? null : string.Format("$filter={0}", filter),
                apiVersion: apiVersion);

            return this.SendRequestAsync<ResponseWithContinuation<TType[]>>(
                httpMethod: HttpMethod.Get,
                requestUri: requestUri,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Generates a resource management request <see cref="Uri"/> based on the input parameters. Supports both subscription and tenant level resource and the condensed resource type and name format.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="apiVersion">The API version.</param>
        /// <param name="action">The action.</param>
        /// <param name="odataQuery">The OData query.</param>
        /// <param name="top">Top resources - used in list queries.</param>
        public Uri GetResourceManagementRequestUri(
            string resourceId,
            string apiVersion,
            string action = null,
            string odataQuery = null,
            string top = null)
        {
            var resourceIdStringBuilder = new StringBuilder(resourceId.CoalesceString().TrimEnd('/'));

            if (!string.IsNullOrWhiteSpace(action))
            {
                resourceIdStringBuilder.AppendFormat("/{0}", action);
            }

            var parts = new[]
            {
                top,
                odataQuery,
                string.Format("api-version={0}", apiVersion)
            };

            var queryString = parts.Where(part => !string.IsNullOrWhiteSpace(part)).ConcatStrings("&");

            resourceIdStringBuilder.AppendFormat("?{0}", queryString);

            var relativeUri = resourceIdStringBuilder.ToString()
                .Select(character => char.IsWhiteSpace(character) ? "%20" : character.ToString())
                .ConcatStrings();

            return new Uri(baseUri: this.EndpointUri, relativeUri: relativeUri);
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <typeparam name="TResponseType">The type of the result of response from the server.</typeparam>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<TResponseType> SendRequestAsync<TResponseType>(
            HttpMethod httpMethod,
            Uri requestUri,
            CancellationToken cancellationToken)
        {
            using (var response = await this
                .SendRequestAsync(httpMethod: httpMethod, requestUri: requestUri, cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                return await response
                    .ReadContentAsJsonAsync<TResponseType>()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod httpMethod,
            Uri requestUri,
            CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(method: httpMethod, requestUri: requestUri))
            {
                return await this.SendRequestAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result.
        /// </summary>
        /// <param name="request">The http request to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            using (var httpClient = this.httpClientHelper.CreateHttpClient())
            {
                try
                {
                    var response = await httpClient
                        .SendAsync(request: request, cancellationToken: cancellationToken)
                        .ConfigureAwait(continueOnCapturedContext: false);

                    if (!response.StatusCode.IsSuccessfulRequest())
                    {
                        var errorResponse = await ResourceManagerRestClientBase
                            .TryReadErrorResponseMessage(response, rewindContentStream: true)
                            .ConfigureAwait(continueOnCapturedContext: false);

                        var message = await ResourceManagerRestClientBase
                            .GetErrorMessage(request: request, response: response, errorResponse: errorResponse)
                            .ConfigureAwait(continueOnCapturedContext: false);

                        throw new ErrorResponseMessageException(
                            httpStatus: response.StatusCode,
                            errorResponseMessage: errorResponse,
                            errorMessage: message);
                    }

                    return response;
                }
                catch (Exception exception)
                {
                    if (exception is OperationCanceledException && !cancellationToken.IsCancellationRequested)
                    {
                        throw new Exception(ProjectResources.OperationFailedWithTimeOut);
                    }

                    throw;
                }
            }
        }
    }

}
