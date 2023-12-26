using Microsoft.Rest;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
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
            Instance = new HttpClientHelperFactory();
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
}
