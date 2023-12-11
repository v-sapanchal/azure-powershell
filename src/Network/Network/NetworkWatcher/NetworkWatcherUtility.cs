using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Hyak.Common;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using Formatting = Newtonsoft.Json.Formatting;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Rest.Serialization;
using System.Management.Automation;
using static Microsoft.Azure.Commands.Network.PSQueryResponse;

namespace Microsoft.Azure.Commands.Network
{
    public class NetworkWatcherUtility
    {

        /// <summary>
        /// Processes the parameters to return a valid resource Id.
        /// </summary>
        /// <param name="subscriptionId">The subscription.</param>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="resourceType">The resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="resourceName">The resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        /// <param name="extensionResourceType">The extension resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="extensionResourceName">The extension resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        public static string GetResourceId(Guid? subscriptionId, string resourceGroupName, string resourceType, string resourceName, string extensionResourceType = null, string extensionResourceName = null)
        {
            if (subscriptionId == null && !string.IsNullOrWhiteSpace(resourceGroupName))
            {
                throw new InvalidOperationException("A resource group cannot be specified without a subscription.");
            }

            var resourceId = new StringBuilder();

            if (subscriptionId != null)
            {
                resourceId.AppendFormat("/subscriptions/{0}", Uri.EscapeDataString(subscriptionId.Value.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(resourceGroupName))
            {
                resourceId.AppendFormat("/resourceGroups/{0}", Uri.EscapeDataString(resourceGroupName));
            }

            if (!string.IsNullOrWhiteSpace(resourceType))
            {
                resourceId.Append(ProcessResourceTypeAndName(resourceType: resourceType, resourceName: resourceName));
            }

            if (!string.IsNullOrWhiteSpace(extensionResourceType))
            {
                resourceId.Append(ProcessResourceTypeAndName(resourceType: extensionResourceType, resourceName: extensionResourceName));
            }

            return resourceId.ToString();
        }

        /// <summary>
        /// Processes a resource type string and a resource name string and 
        /// </summary>
        /// <param name="resourceType">The resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="resourceName">The resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        private static string ProcessResourceTypeAndName(string resourceType, string resourceName)
        {
            var resourceId = new StringBuilder();
            var resourceTypeTokens = resourceType.SplitRemoveEmpty('/');
            var resourceNameTokens = resourceName.CoalesceString().SplitRemoveEmpty('/');

            resourceId.AppendFormat("/providers/{0}", Uri.EscapeDataString(resourceTypeTokens.First()));

            for (int i = 1; i < resourceTypeTokens.Length; ++i)
            {
                resourceId.AppendFormat("/{0}", Uri.EscapeDataString(resourceTypeTokens[i]));

                if (resourceNameTokens.Length > i - 1)
                {
                    resourceId.AppendFormat("/{0}", Uri.EscapeDataString(resourceNameTokens[i - 1]));
                }
            }

            return resourceId.ToString();
        }

    }

    public static class OperationalInsightsDataClientExtensions
    {
        public static OperationalInsightsQueryResults Query(this OperationalInsightsDataClient operations, string query, TimeSpan? timespan = null, IList<string> workspaces = null)
        {
            return operations.QueryAsync(query, timespan, workspaces).GetAwaiter().GetResult();
        }

        public static async Task<OperationalInsightsQueryResults> QueryAsync(this OperationalInsightsDataClient operations, string query, TimeSpan? timespan = null, IList<string> workspaces = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpOperationResponse<OperationalInsightsQueryResults> httpOperationResponse = await operations.QueryWithHttpMessagesAsync(query, timespan, workspaces, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
            {
                return httpOperationResponse.Body;
            }
        }
    }

    public class OperationalInsightsDataClient : Rest.ServiceClient<OperationalInsightsDataClient>
    {
        public Uri BaseUri { get; set; }
        public JsonSerializerSettings SerializationSettings { get; private set; }
        public JsonSerializerSettings DeserializationSettings { get; private set; }
        public ServiceClientCredentials Credentials { get; private set; }

        public string WorkspaceId { get; set; }

        public ApiPreferences Preferences { get; set; } = new ApiPreferences();

        public string NameHeader { get; set; }

        public string RequestId { get; set; }

        public OperationalInsightsDataClient(ServiceClientCredentials credentials)
            : this(credentials, (DelegatingHandler[])null)
        {
        }

        public OperationalInsightsDataClient(ServiceClientCredentials credentials, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }

            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        protected OperationalInsightsDataClient(params DelegatingHandler[] handlers)
          : base(handlers)
        {
            Initialize();
        }

        private void CustomInitialize()
        {
            DelegatingHandler delegatingHandler = base.FirstMessageHandler as DelegatingHandler;
            if (delegatingHandler != null)
            {
                CustomDelegatingHandler customDelegatingHandler = (CustomDelegatingHandler)(delegatingHandler.InnerHandler = new CustomDelegatingHandler
                {
                    InnerHandler = delegatingHandler.InnerHandler,
                    Client = this
                });
            }
        }

        private void Initialize()
        {
            BaseUri = new Uri("https://api.loganalytics.io/v1");
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new Iso8601TimeSpanConverter()
                }
            };
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new Iso8601TimeSpanConverter()
                }
            };
            CustomInitialize();
        }

        public async Task<HttpOperationResponse<OperationalInsightsQueryResults>> QueryWithHttpMessagesAsync(string query, TimeSpan? timespan = null, IList<string> workspaces = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (WorkspaceId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "this.WorkspaceId");
            }

            if (query == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "query");
            }

            QueryBody queryBody = new QueryBody();
            if (query != null || timespan.HasValue || workspaces != null)
            {
                queryBody.Query = query;
                queryBody.Timespan = timespan;
                queryBody.Workspaces = workspaces;
            }

            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("body", queryBody);
                dictionary.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "Query", dictionary);
            }

            string absoluteUri = BaseUri.AbsoluteUri;
            string text = new Uri(new Uri(absoluteUri + (absoluteUri.EndsWith("/") ? "" : "/")), "workspaces/{workspaceId}/query").ToString();
            text = text.Replace("{workspaceId}", Uri.EscapeDataString(WorkspaceId));
            HttpRequestMessage _httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(text)
            };
            if (customHeaders != null)
            {
                foreach (KeyValuePair<string, List<string>> customHeader in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(customHeader.Key))
                    {
                        _httpRequest.Headers.Remove(customHeader.Key);
                    }

                    _httpRequest.Headers.TryAddWithoutValidation(customHeader.Key, customHeader.Value);
                }
            }

            string _requestContent = null;
            if (queryBody != null)
            {
                _requestContent = SafeJsonConvert.SerializeObject(queryBody, SerializationSettings);
                _httpRequest.Content = new StringContent(_requestContent, Encoding.UTF8);
                _httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            }

            if (Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            }

            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }

            cancellationToken.ThrowIfCancellationRequested();
            HttpResponseMessage _httpResponse = await base.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }

            HttpStatusCode statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent2 = null;
            if (statusCode != HttpStatusCode.OK)
            {
                ErrorResponseException ex = new ErrorResponseException($"Operation returned an invalid status code '{statusCode}'");
                try
                {
                    _responseContent2 = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                    ErrorResponse errorResponse = SafeJsonConvert.DeserializeObject<ErrorResponse>(_responseContent2, DeserializationSettings);
                    if (errorResponse != null)
                    {
                        ex.Body = errorResponse;
                    }
                }
                catch (JsonException)
                {
                }

                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent2);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }

                _httpRequest.Dispose();
                _httpResponse?.Dispose();
                throw ex;
            }

            HttpOperationResponse<OperationalInsightsQueryResults> _result = new HttpOperationResponse<OperationalInsightsQueryResults>
            {
                Request = _httpRequest,
                Response = _httpResponse
            };
            if (statusCode == HttpStatusCode.OK)
            {
                _responseContent2 = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<OperationalInsightsQueryResults>(_responseContent2, DeserializationSettings);
                }
                catch (JsonException innerException)
                {
                    _httpRequest.Dispose();
                    _httpResponse?.Dispose();
                    throw new SerializationException("Unable to deserialize the response.", _responseContent2, innerException);
                }
            }

            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }

            return _result;
        }
    }

    public class ApiKeyClientCredentials : ServiceClientCredentials
    {
        private string token;

        public ApiKeyClientCredentials(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("token must not be null or empty");
            }

            this.token = token;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-api-key", token);
            return Task.FromResult(result: true);
        }
    }

    public class ApiPreferences
    {
        public bool IncludeRender { get; set; }

        public bool IncludeStatistics { get; set; }

        public int Wait { get; set; } = int.MinValue;


        public override string ToString()
        {
            string text = "response-v1=true";
            if (IncludeRender)
            {
                text += ",include-render=true";
            }

            if (IncludeStatistics)
            {
                text += ",include-statistics=true";
            }

            if (Wait != int.MinValue)
            {
                text += $",wait={Wait}";
            }

            return text;
        }
    }

    internal class CustomDelegatingHandler : DelegatingHandler
    {
        internal const string InternalNameHeader = "csharpsdk";

        internal OperationalInsightsDataClient Client { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string text = "csharpsdk";
            if (!string.IsNullOrWhiteSpace(Client.NameHeader))
            {
                text = text + "," + Client.NameHeader;
            }

            request.Headers.Add("prefer", Client.Preferences.ToString());
            request.Headers.Add("x-ms-app", text);
            request.Headers.Add("x-ms-client-request-id", Client.RequestId ?? Guid.NewGuid().ToString());
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        }
    }

    public class PSQueryResponse
    {
        // Private constructor so no one else can make one
        //private PSQueryResponse() { }

        public static PSQueryResponse Create(OperationalInsightsQueryResults response)
        {
            PSQueryResponse pSQueryResponse = new PSQueryResponse();
            pSQueryResponse.Results = GetResultEnumerable(response.Results);
            pSQueryResponse.Render = response.Render;
            pSQueryResponse.Statistics = response.Statistics;
            pSQueryResponse.Error = response.Error;
            return pSQueryResponse;
        }

        public IEnumerable<PSObject> Results { get; set; }
        public IDictionary<string, string> Render { get; set; }
        public IDictionary<string, object> Statistics { get; set; }
        public QueryResponseError Error { get; set; }

        private static IEnumerable<PSObject> GetResultEnumerable(IEnumerable<IDictionary<string, string>> rows)
        {
            foreach (var row in rows)
            {
                var psObject = new PSObject();
                foreach (var cell in row)
                {
                    psObject.Properties.Add(new PSNoteProperty(cell.Key, cell.Value));
                }
                yield return psObject;
            }
        }

        public class OperationalInsightsQueryResults
        {
            [JsonProperty(PropertyName = "results")]
            public IEnumerable<IDictionary<string, string>> Results
            {
                get
                {
                    foreach (Table table in Tables)
                    {
                        foreach (IList<string> row in table.Rows)
                        {
                            yield return table.Columns.Zip(row, (Column column, string cell) => new { column.Name, cell }).ToDictionary(entry => entry.Name, entry => entry.cell);
                        }
                    }
                }
            }

            public IDictionary<string, string> Render { get; set; }

            public IDictionary<string, object> Statistics { get; set; }

            public QueryResponseError Error { get; set; }

            [JsonProperty(PropertyName = "tables")]
            public IList<Table> Tables { get; set; }

            public OperationalInsightsQueryResults()
            {
            }

            public OperationalInsightsQueryResults(IList<Table> tables)
            {
                Tables = tables;
            }

            public virtual void Validate()
            {
                if (Tables == null)
                {
                    throw new ValidationException(ValidationRules.CannotBeNull, "Tables");
                }

                if (Tables == null)
                {
                    return;
                }

                foreach (Table table in Tables)
                {
                    table?.Validate();
                }
            }
        }

        public class Table
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "columns")]
            public IList<Column> Columns { get; set; }

            [JsonProperty(PropertyName = "rows")]
            public IList<IList<string>> Rows { get; set; }

            public Table()
            {
            }

            public Table(string name, IList<Column> columns, IList<IList<string>> rows)
            {
                Name = name;
                Columns = columns;
                Rows = rows;
            }

            public virtual void Validate()
            {
                if (Name == null)
                {
                    throw new ValidationException(ValidationRules.CannotBeNull, "Name");
                }

                if (Columns == null)
                {
                    throw new ValidationException(ValidationRules.CannotBeNull, "Columns");
                }

                if (Rows == null)
                {
                    throw new ValidationException(ValidationRules.CannotBeNull, "Rows");
                }
            }
        }

        public class Column
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }

            public Column()
            {
            }

            public Column(string name = null, string type = null)
            {
                Name = name;
                Type = type;
            }
        }

        public class QueryResponseError
        {
            public string Code { get; set; }

            public List<QueryResponseError> Details { get; set; }

            public string Message { get; set; }

            public QueryResponseInnerError InnerError { get; set; }
        }

        public class QueryResponseInnerError
        {
            public int Severity { get; set; }

            public string SeverityName { get; set; }

            public string Message { get; set; }

            public string Code { get; set; }
        }
    }


    /// <summary>
    /// The authentication handler.
    /// </summary>
    public class AuthenticationHandler : DelegatingHandler
    {
        /// <summary>
        /// The service client credentials.
        /// </summary>
        private readonly ServiceClientCredentials clientCredentials;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationHandler" /> class.
        /// </summary>
        /// <param name="cloudCredentials">The credentials.</param>
        public AuthenticationHandler(ServiceClientCredentials cloudCredentials)
        {
            this.clientCredentials = cloudCredentials;

        }

        /// <summary>
        /// Add the authentication token to the outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await this.clientCredentials
                .ProcessHttpRequestAsync(request: request, cancellationToken: cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);

            return await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

    }

    /// <summary>
    /// Factory class for creating <see cref="HttpClient"/> objects with custom headers.
    /// </summary>
    public abstract class HttpClientHelper
    {
        /// <summary>
        /// The service client credentials.
        /// </summary>
        private readonly ServiceClientCredentials credentials;

        /// <summary>
        /// The header values.
        /// </summary>
        private readonly IEnumerable<ProductInfoHeaderValue> headerValues;

        /// <summary>
        /// The cmdlet info header values.
        /// </summary>
        private readonly Dictionary<string, string> cmdletHeaderValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelper"/> class.
        /// </summary>
        /// <param name="credentials">The service client credentials.</param>
        /// <param name="headerValues">The header values.</param>
        /// <param name="cmdletHeaderValues">The cmdlet info header values.</param>
        protected HttpClientHelper(ServiceClientCredentials credentials, IEnumerable<ProductInfoHeaderValue> headerValues, Dictionary<string, string> cmdletHeaderValues)
        {
            this.credentials = credentials;
            this.headerValues = headerValues;
            this.cmdletHeaderValues = cmdletHeaderValues;
        }

        /// <summary>
        /// Creates an <see cref="HttpClient"/>
        /// </summary>
        /// <param name="primaryHandlers">The handlers that will be added to the top of the chain.</param>
        public virtual HttpClient CreateHttpClient(params DelegatingHandler[] primaryHandlers)
        {
            var delegateHandlers = new DelegatingHandler[]
            {
                new AuthenticationHandler(cloudCredentials: credentials),
                new UserAgentHandler(headerValues: headerValues),
                new CmdletInfoHandler(cmdletHeaderValues: cmdletHeaderValues),
                new TracingHandler(),
                new RetryHandler(),
            };

            var pipeline = (HttpMessageHandler)(new HttpClientHandler());
            var reversedHandlers = CoalesceEnumerable(primaryHandlers).Concat(delegateHandlers).ToArray().Reverse();
            foreach (var handler in reversedHandlers)
            {
                handler.InnerHandler = pipeline;
                pipeline = handler;
            }

            return new HttpClient(pipeline);
        }

        /// <summary>
        /// Coalesces the enumerable.
        /// </summary>
        /// <typeparam name="DelegatingHandler">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        public static IEnumerable<DelegatingHandler> CoalesceEnumerable<DelegatingHandler>(IEnumerable<DelegatingHandler> source)
        {
            return source ?? Enumerable.Empty<DelegatingHandler>();
        }
    }

    /// <summary>
    /// The user agent handler.
    /// </summary>
    public class UserAgentHandler : DelegatingHandler
    {
        /// <summary>
        /// The product info to add as headers.
        /// </summary>
        private readonly IEnumerable<ProductInfoHeaderValue> headerValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAgentHandler" /> class.
        /// </summary>
        /// <param name="headerValues">The product info to add as headers.</param>
        public UserAgentHandler(IEnumerable<ProductInfoHeaderValue> headerValues)
        {
            this.headerValues = headerValues;
        }

        ///// <summary>
        ///// Add the user agent to the outgoing request.
        ///// </summary>
        ///// <param name="request">The HTTP request message.</param>
        ///// <param name="cancellationToken">The cancellation token.</param>
        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    var currentRequestHeaders = request.Headers.UserAgent
        //        .ToInsensitiveDictionary(header => header.Product.Name + header.Product.Version);

        //    var infosToAdd = this.headerValues
        //        .Where(productInfo => !currentRequestHeaders.ContainsKey(productInfo.Product.Name + productInfo.Product.Version));

        //    foreach (var infoToAdd in infosToAdd)
        //    {
        //        request.Headers.UserAgent.Add(infoToAdd);
        //    }

        //    return await base
        //        .SendAsync(request, cancellationToken)
        //        .ConfigureAwait(continueOnCapturedContext: false);
        //}

        ///// <summary>
        ///// Creates an insensitive dictionary from an enumerable.
        ///// </summary>
        ///// <param name="source">The enumerable.</param>
        ///// <param name="keySelector">The key selector.</param>
        //public static InsensitiveDictionary<ProductInfoHeaderValue> ToInsensitiveDictionary<ProductInfoHeaderValue>(IEnumerable<ProductInfoHeaderValue> source, Func<TValue, string> keySelector)
        //{
        //    var dictionary = new InsensitiveDictionary<ProductInfoHeaderValue>();
        //    foreach (var value in source)
        //    {
        //        dictionary[keySelector(value)] = value;
        //    }

        //    return dictionary;
        //}
    }

    /// <summary>
    /// Tracing handler.
    /// </summary>
    public class TracingHandler : DelegatingHandler
    {
        /// <summary>
        /// Trace the outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!TracingAdapter.IsEnabled)
            {
                return await base.SendAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }

            var invocationId = TracingAdapter.NextInvocationId.ToString();
            try
            {
                TracingAdapter.SendRequest(invocationId: invocationId, request: request);
                var response = await base.SendAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
                TracingAdapter.ReceiveResponse(invocationId: invocationId, response: response);
                return response;
            }
            catch (Exception ex)
            {
                if (IsFatal(ex))
                {
                    throw;
                }

                TracingAdapter.Error(invocationId: invocationId, ex: ex);
                throw;
            }


        }

        /// <summary>
        /// Test if an exception is a fatal exception. 
        /// </summary>
        /// <param name="ex">Exception object.</param>
        public static bool IsFatal(Exception ex)
        {
            if (ex is AggregateException)
            {
                return ex.Cast<AggregateException>().Flatten().InnerExceptions.Any(exception => IsFatal(exception));
            }

            if (ex.InnerException != null && IsFatal(ex.InnerException))
            {
                return true;
            }

            return
                ex is TypeInitializationException ||
                ex is AppDomainUnloadedException ||
                ex is ThreadInterruptedException ||
                ex is AccessViolationException ||
                ex is InvalidProgramException ||
                ex is BadImageFormatException ||
                ex is StackOverflowException ||
                ex is ThreadAbortException ||
                ex is OutOfMemoryException ||
                ex is SecurityException ||
                ex is SEHException;
        }
    }

    /// <summary>
    /// The cmdlet info handler.
    /// </summary>
    public class CmdletInfoHandler : DelegatingHandler
    {
        /// <summary>
        /// The product info to add as headers.
        /// </summary>
        private readonly Dictionary<string, string> cmdletHeaderValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="CmdletInfoHandler" /> class.
        /// </summary>
        /// <param name="cmdletHeaderValues">The product info to add as headers.</param>
        public CmdletInfoHandler(Dictionary<string, string> cmdletHeaderValues)
        {
            this.cmdletHeaderValues = cmdletHeaderValues;
        }

        /// <summary>
        /// Add the custom headers to the outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            foreach (KeyValuePair<string, string> kvp in cmdletHeaderValues)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }

            return await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }

    /// <summary>
    /// Class containing HTTP message extension methods.
    /// </summary>
    public static class HttpMessageExtensions
    {
        /// <summary>
        /// Reads the JSON content from the http response message.
        /// </summary>
        /// <typeparam name="T">The type of object contained in the JSON.</typeparam>
        /// <param name="message">The response message to be read.</param>
        /// <param name="rewindContentStream">Rewind content stream if set to true.</param>
        /// <returns>An object of type T instantiated from the response message's body.</returns>
        public static async Task<T> ReadContentAsJsonAsync<T>(this HttpResponseMessage message, bool rewindContentStream = false)
        {
            using (var stream = await message.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var streamPosition = stream.Position;
                try
                {
                    return FromJson<T>(stream);
                }
                finally
                {
                    if (stream.CanSeek && streamPosition != stream.Position && rewindContentStream)
                    {
                        stream.Seek(streamPosition, SeekOrigin.Begin);
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize object from a JSON stream.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="stream">A <see cref="Stream"/> that contains a JSON representation of object</param>
        public static T FromJson<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JsonExtensions.JsonObjectTypeSerializer.Deserialize<T>(jsonReader);
            }
        }

        /// <summary>
        /// Reads the JSON content from the http response message.
        /// </summary>
        /// <param name="message">The response message to be read.</param>
        /// <param name="rewindContentStream">Rewind content stream if set to true.</param>
        /// <returns>An object of type T instantiated from the response message's body.</returns>
        public static async Task<string> ReadContentAsStringAsync(this HttpResponseMessage message, bool rewindContentStream = false)
        {
            using (var stream = await message.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(continueOnCapturedContext: false))
            using (var streamReader = new StreamReader(stream))
            {
                var streamPosition = stream.Position;
                try
                {

                    return streamReader.ReadToEnd();
                }
                finally
                {
                    if (stream.CanSeek && streamPosition != stream.Position && rewindContentStream)
                    {
                        stream.Seek(streamPosition, SeekOrigin.Begin);
                    }
                }
            }
        }
    }

    /// <summary>
    /// <c>JSON</c> extensions
    /// </summary>
    public static class JsonExtensions
    {
        public static readonly JsonSerializer JsonObjectTypeSerializer = JsonSerializer.Create(JsonExtensions.ObjectSerializationSettings);

        /// <summary>
        /// The JSON object serialization settings.
        /// </summary>
        public static readonly JsonSerializerSettings ObjectSerializationSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesWithOverridesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new TimeSpanConverter(),
                new StringEnumConverter(new DefaultNamingStrategy()),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal },
            },
        };
    }

    /// <summary>
    /// Overrides the default CamelCase resolver to respect property name set in the <c>JsonPropertyAttribute</c>.
    /// </summary>
    internal class CamelCasePropertyNamesWithOverridesContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// Creates dictionary contract
        /// </summary>
        /// <param name="objectType">The object type.</param>
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            var contract = base.CreateDictionaryContract(objectType);

            // TODO: Remove IfDef code
#if NETSTANDARD
            contract.DictionaryKeyResolver = keyName => keyName;
#else
            contract.PropertyNameResolver = propertyName => propertyName;
#endif
            return contract;
        }
    }

    /// <summary>
    /// The TimeSpan converter based on ISO 8601 format.
    /// </summary>
    internal class TimeSpanConverter : JsonConverter
    {
        /// <summary>
        /// Writes the <c>JSON</c>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, XmlConvert.ToString((TimeSpan)value));
        }

        /// <summary>
        /// Reads the <c>JSON</c>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.TokenType != JsonToken.Null ? (object)XmlConvert.ToTimeSpan(serializer.Deserialize<string>(reader)) : null;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }
    }

    /// <summary>
    /// The http utility.
    /// </summary>
    public static class HttpUtility
    {
        /// <summary>
        /// Returns true if the status code corresponds to a successful request.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public static bool IsSuccessfulRequest(this HttpStatusCode statusCode)
        {
            return HttpUtility.IsSuccessfulRequest((int)statusCode);
        }

        /// <summary>
        /// Returns true if the status code corresponds to a server failure request.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public static bool IsServerFailureRequest(this HttpStatusCode statusCode)
        {
            return HttpUtility.IsServerFailureRequest((int)statusCode);
        }

        /// <summary>
        /// Returns true if the status code corresponds to client failure.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public static bool IsClientFailureRequest(this HttpStatusCode statusCode)
        {
            return HttpUtility.IsClientFailureRequest((int)statusCode);
        }

        /// <summary>
        /// Returns true if the status code corresponds to a successful request.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        private static bool IsSuccessfulRequest(int statusCode)
        {
            return (statusCode >= 200 && statusCode <= 299) || statusCode == 304;
        }

        /// <summary>
        /// Returns true if the status code corresponds to client failure.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        private static bool IsClientFailureRequest(int statusCode)
        {
            return statusCode == 505 || statusCode == 501 || (statusCode >= 400 && statusCode < 500 && statusCode != 408);
        }

        /// <summary>
        /// Returns true if the status code corresponds to a server failure request.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        private static bool IsServerFailureRequest(int statusCode)
        {
            return (statusCode >= 500 && statusCode <= 599 && statusCode != 505 && statusCode != 501) || statusCode == 408;
        }
    }

    /// <summary>
    /// The object extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Casts the specified object to type T.
        /// </summary>
        /// <typeparam name="T">The type to cast to</typeparam>
        /// <param name="obj">The input object</param>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        /// <summary>
        /// Wraps the object in an array of length 1.
        /// </summary>
        /// <typeparam name="T">Type of object to wrap.</typeparam>
        /// <param name="obj">Object to wrap in array.</param>
        public static T[] AsArray<T>(this T obj)
        {
            return new T[] { obj };
        }
    }

    /// <summary>
    /// A class that builds query filters.
    /// </summary>
    public static class QueryFilterBuilder
    {
        /// <summary>
        /// Creates a filter from the given properties.
        /// </summary>
        /// <param name="subscriptionId">The subscription to query.</param>
        /// <param name="resourceGroup">The name of the resource group/</param>
        /// <param name="resourceType">The resource type.</param>
        /// <param name="resourceName">The resource name.</param>
        /// <param name="tagName">The tag name.</param>
        /// <param name="tagValue">The tag value.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="nameContains"></param>
        /// <param name="resourceGroupNameContains"></param>
        public static string CreateFilter(
            string subscriptionId,
            string resourceGroup,
            string resourceType,
            string resourceName,
            string tagName,
            string tagValue,
            string filter,
            string nameContains = null,
            string resourceGroupNameContains = null)
        {
            var filterStringBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(subscriptionId))
            {
                filterStringBuilder.AppendFormat("subscriptionId EQ '{0}'", subscriptionId);
            }

            if (!string.IsNullOrWhiteSpace(resourceGroup))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("resourceGroup EQ '{0}'", resourceGroup);
            }

            if (!string.IsNullOrWhiteSpace(resourceType))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("resourceType EQ '{0}'", resourceType);
            }

            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("name EQ '{0}'", resourceName);
            }

            if (!string.IsNullOrWhiteSpace(tagName))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("tagName EQ '{0}'", tagName);
            }

            if (!string.IsNullOrWhiteSpace(tagValue))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("tagValue EQ '{0}'", tagValue);
            }

            if (!string.IsNullOrWhiteSpace(resourceGroupNameContains))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("substringof('{0}', resourceGroup)", resourceGroupNameContains);
            }

            if (!string.IsNullOrWhiteSpace(nameContains))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("substringof('{0}', name)", nameContains);
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.Trim().TrimStart('?').TrimStart('&');

                if (filter.StartsWith("$filter", StringComparison.InvariantCultureIgnoreCase))
                {
                    var indexOfEqual = filter.IndexOf("=", StringComparison.Ordinal);

                    if (indexOfEqual > 0 && indexOfEqual < filter.Length - 2)
                    {

                        filter = filter.Substring(filter.IndexOf("=", StringComparison.Ordinal) + 1).Trim();
                    }
                    else
                    {
                        throw new ArgumentException(
                            "If $filter is specified, it cannot be empty and must be of the format '$filter = <filter_value>'. The filter: " + filter,
                            "filter");
                    }
                }
            }

            if (filterStringBuilder.Length > 0 && !string.IsNullOrWhiteSpace(filter))
            {
                return "(" + filterStringBuilder.ToString() + ") AND (" + filter.CoalesceString() + ")";
            }

            return filterStringBuilder.Length > 0
                ? filterStringBuilder.ToString()
                : filter.CoalesceString();
        }
    }

    /// <summary>
    /// Factory class for creating http client helpers.
    /// </summary>
    public class HttpClientHelperFactory
    {
        /// <summary>
        /// Gets an instance of the facotry.
        /// </summary>
        public static HttpClientHelperFactory Instance { get; internal set; }

        /// <summary>
        /// Initializes static members of the <see cref="HttpClientHelperFactory"/> class.
        /// </summary>
        static HttpClientHelperFactory()
        {
            HttpClientHelperFactory.Instance = new HttpClientHelperFactory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelperFactory"/> class.
        /// </summary>
        protected HttpClientHelperFactory()
        {
        }

        /// <summary>
        /// Creates new instances of the <see cref="HttpClientHelper"/> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="headerValues">The headers.</param>
        /// <param name="cmdletHeaderValues">The cmdlet info header.</param>
        public virtual HttpClientHelper CreateHttpClientHelper(ServiceClientCredentials credentials, IEnumerable<ProductInfoHeaderValue> headerValues, Dictionary<string, string> cmdletHeaderValues)
        {
            return new HttpClientHelperImpl(credentials: credentials, headerValues: headerValues, cmdletHeaderValues: cmdletHeaderValues);
        }

        /// <summary>
        /// An implementation of the <see cref="HttpClientHelper"/> abstract class.
        /// </summary>
        private class HttpClientHelperImpl : HttpClientHelper
        {
            /// <summary>
            /// Initializes new instances of the <see cref="HttpClientHelperImpl"/> class.
            /// </summary>
            /// <param name="credentials">The credentials.</param>
            /// <param name="headerValues">The headers.</param>
            /// <param name="cmdletHeaderValues">The cmdlet info header.</param>
            public HttpClientHelperImpl(ServiceClientCredentials credentials, IEnumerable<ProductInfoHeaderValue> headerValues, Dictionary<string, string> cmdletHeaderValues)
                : base(credentials: credentials, headerValues: headerValues, cmdletHeaderValues: cmdletHeaderValues)
            {
            }
        }
    }

    public class ResourceManagerRestRestClient : ResourceManagerRestClientBase
    {
        /// <summary>
        /// The endpoint that this client will communicate with.
        /// </summary>
        public Uri EndpointUri { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManagerRestRestClient"/> class.
        /// </summary>
        /// <param name="endpointUri">The endpoint that this client will communicate with.</param>
        /// <param name="httpClientHelper">The azure http client wrapper to use.</param>
        public ResourceManagerRestRestClient(Uri endpointUri, HttpClientHelper httpClientHelper)
            : base(httpClientHelper: httpClientHelper)
        {
            this.EndpointUri = endpointUri;
        }

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
            var resourceId = ResourceIdUtility.GetResourceId(
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
    }

    public class ResourceManagerRestClientBase
    {
        /// <summary>
        /// The azure http client wrapper to use.
        /// </summary>
        private readonly HttpClientHelper httpClientHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManagerRestClientBase"/> class.
        /// </summary>
        /// <param name="httpClientHelper">The azure http client wrapper to use.</param>
        public ResourceManagerRestClientBase(HttpClientHelper httpClientHelper)
        {
            this.httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// Sends an HTTP request message and returns the result from the content of the response message.
        /// </summary>
        /// <typeparam name="TResponseType">The type of the result of response from the server.</typeparam>
        /// <param name="httpMethod">The http method to use.</param>
        /// <param name="requestUri">The Uri of the operation.</param>
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<TResponseType> SendRequestAsync<TResponseType>(
            HttpMethod httpMethod,
            Uri requestUri,
            JObject content,
            CancellationToken cancellationToken)
        {
            using (var response = await this
                .SendRequestAsync(httpMethod: httpMethod, requestUri: requestUri, content: content, cancellationToken: cancellationToken)
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
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod httpMethod,
            Uri requestUri,
            JToken content,
            CancellationToken cancellationToken)
        {
            var contentString = content == null ? string.Empty : content.ToString();
            // minify JOSN payload to avoid payload too large error
            if (!string.IsNullOrEmpty(contentString))
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject(contentString);
                    contentString = JsonConvert.SerializeObject(obj, Formatting.None);
                }
                catch
                {
                    // leave contentString as it is
                }
            }
            using (var httpContent = new StringContent(content: contentString, encoding: Encoding.UTF8, mediaType: "application/json"))
            using (var request = new HttpRequestMessage(method: httpMethod, requestUri: requestUri) { Content = httpContent })
            {
                return await this.SendRequestAsync(request: request, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
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
                        throw new Exception("Not IsSuccessfulRequest");
                    }

                    return response;
                }
                catch (Exception exception)
                {
                    if (exception is OperationCanceledException && !cancellationToken.IsCancellationRequested)
                    {
                        throw new Exception("ProjectResources.OperationFailedWithTimeOut");
                    }

                    throw;
                }
            }
        }
    }

    public static class ResourceIdUtility
    {
        /// <summary>
        /// Processes the parameters to return a valid resource Id.
        /// </summary>
        /// <param name="subscriptionId">The subscription.</param>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="resourceType">The resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="resourceName">The resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        /// <param name="extensionResourceType">The extension resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="extensionResourceName">The extension resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        public static string GetResourceId(Guid? subscriptionId, string resourceGroupName, string resourceType, string resourceName, string extensionResourceType = null, string extensionResourceName = null)
        {
            if (subscriptionId == null && !string.IsNullOrWhiteSpace(resourceGroupName))
            {
                throw new InvalidOperationException("A resource group cannot be specified without a subscription.");
            }

            var resourceId = new StringBuilder();

            if (subscriptionId != null)
            {
                resourceId.AppendFormat("/subscriptions/{0}", Uri.EscapeDataString(subscriptionId.Value.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(resourceGroupName))
            {
                resourceId.AppendFormat("/resourceGroups/{0}", Uri.EscapeDataString(resourceGroupName));
            }

            if (!string.IsNullOrWhiteSpace(resourceType))
            {
                resourceId.Append(ResourceIdUtility.ProcessResourceTypeAndName(resourceType: resourceType, resourceName: resourceName));
            }

            if (!string.IsNullOrWhiteSpace(extensionResourceType))
            {
                resourceId.Append(ResourceIdUtility.ProcessResourceTypeAndName(resourceType: extensionResourceType, resourceName: extensionResourceName));
            }

            return resourceId.ToString();
        }

        /// <summary>
        /// Processes a resource type string and a resource name string and 
        /// </summary>
        /// <param name="resourceType">The resource type string in the format: '{providerName}/{typeName}/{nestedTypeName}'</param>
        /// <param name="resourceName">The resource name in the format: '{resourceName}[/{nestedResourceName}]'</param>
        private static string ProcessResourceTypeAndName(string resourceType, string resourceName)
        {
            var resourceId = new StringBuilder();
            var resourceTypeTokens = resourceType.SplitRemoveEmpty('/');
            var resourceNameTokens = resourceName.CoalesceString().SplitRemoveEmpty('/');

            resourceId.AppendFormat("/providers/{0}", Uri.EscapeDataString(resourceTypeTokens.First()));

            for (int i = 1; i < resourceTypeTokens.Length; ++i)
            {
                resourceId.AppendFormat("/{0}", Uri.EscapeDataString(resourceTypeTokens[i]));

                if (resourceNameTokens.Length > i - 1)
                {
                    resourceId.AppendFormat("/{0}", Uri.EscapeDataString(resourceNameTokens[i - 1]));
                }
            }

            return resourceId.ToString();
        }
    }

    public class QueryBody
    {
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        [JsonProperty(PropertyName = "timespan")]
        public TimeSpan? Timespan { get; set; }

        [JsonProperty(PropertyName = "workspaces")]
        public IList<string> Workspaces { get; set; }

        public QueryBody()
        {
        }

        public QueryBody(string query, TimeSpan? timespan = null, IList<string> workspaces = null)
        {
            Query = query;
            Timespan = timespan;
            Workspaces = workspaces;
        }

        public virtual void Validate()
        {
            if (Query == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Query");
            }
        }
    }
}
