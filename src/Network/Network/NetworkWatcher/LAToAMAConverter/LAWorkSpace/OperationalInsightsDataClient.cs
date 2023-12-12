using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using Formatting = Newtonsoft.Json.Formatting;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Rest.Serialization;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
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
            : this(credentials, null)
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
            DelegatingHandler delegatingHandler = FirstMessageHandler as DelegatingHandler;
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

        public async Task<HttpOperationResponse<OperationalInsightsQueryResults>> QueryWithHttpMessagesAsync(string query, TimeSpan? timespan = null, IList<string> workspaces = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default)
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
            HttpResponseMessage _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
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
}
