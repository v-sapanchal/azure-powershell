using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
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
    }
}
