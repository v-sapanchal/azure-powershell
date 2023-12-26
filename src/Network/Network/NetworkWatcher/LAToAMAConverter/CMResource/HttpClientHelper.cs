using Hyak.Common;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{

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

            var pipeline = (HttpMessageHandler)new HttpClientHandler();
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
}
