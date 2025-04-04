// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.
namespace Microsoft.Azure.Management.DataBoxEdge
{
    using Microsoft.Rest.Azure;
    using Models;

    /// <summary>
    /// Extension methods for SkusOperations
    /// </summary>
    public static partial class SkusOperationsExtensions
    {
        /// <summary>
        /// List all the available Skus in the region and information related to them
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='filter'>
        /// Specify $filter=&#39;location eq &lt;location&gt;&#39; to filter on location.
        /// </param>
        public static System.Collections.Generic.IEnumerable<ResourceTypeSku> List(this ISkusOperations operations, string filter = default(string))
        {
                return ((ISkusOperations)operations).ListAsync(filter).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List all the available Skus in the region and information related to them
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='filter'>
        /// Specify $filter=&#39;location eq &lt;location&gt;&#39; to filter on location.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<ResourceTypeSku>> ListAsync(this ISkusOperations operations, string filter = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListWithHttpMessagesAsync(filter, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
