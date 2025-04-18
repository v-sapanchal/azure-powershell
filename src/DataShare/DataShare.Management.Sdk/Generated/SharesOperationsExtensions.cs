// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.
namespace Microsoft.Azure.Management.DataShare
{
    using Microsoft.Rest.Azure;
    using Models;

    /// <summary>
    /// Extension methods for SharesOperations
    /// </summary>
    public static partial class SharesOperationsExtensions
    {
        /// <summary>
        /// Get a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share to retrieve.
        /// </param>
        public static Share Get(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName)
        {
                return ((ISharesOperations)operations).GetAsync(resourceGroupName, accountName, shareName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share to retrieve.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Share> GetAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.GetWithHttpMessagesAsync(resourceGroupName, accountName, shareName, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// Create a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        public static Share Create(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, Share share)
        {
                return ((ISharesOperations)operations).CreateAsync(resourceGroupName, accountName, shareName, share).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Create a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Share> CreateAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, Share share, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.CreateWithHttpMessagesAsync(resourceGroupName, accountName, shareName, share, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// Delete a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        public static OperationResponse Delete(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName)
        {
                return ((ISharesOperations)operations).DeleteAsync(resourceGroupName, accountName, shareName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Delete a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<OperationResponse> DeleteAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.DeleteWithHttpMessagesAsync(resourceGroupName, accountName, shareName, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// List shares in an account
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='skipToken'>
        /// Continuation Token
        /// </param>
        public static Microsoft.Rest.Azure.IPage<Share> ListByAccount(this ISharesOperations operations, string resourceGroupName, string accountName, string skipToken = default(string))
        {
                return ((ISharesOperations)operations).ListByAccountAsync(resourceGroupName, accountName, skipToken).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List shares in an account
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='skipToken'>
        /// Continuation Token
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Microsoft.Rest.Azure.IPage<Share>> ListByAccountAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string skipToken = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListByAccountWithHttpMessagesAsync(resourceGroupName, accountName, skipToken, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// List synchronizations of a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='skipToken'>
        /// Continuation token
        /// </param>
        public static Microsoft.Rest.Azure.IPage<ShareSynchronization> ListSynchronizations(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, string skipToken = default(string))
        {
                return ((ISharesOperations)operations).ListSynchronizationsAsync(resourceGroupName, accountName, shareName, skipToken).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List synchronizations of a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='skipToken'>
        /// Continuation token
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Microsoft.Rest.Azure.IPage<ShareSynchronization>> ListSynchronizationsAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, string skipToken = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListSynchronizationsWithHttpMessagesAsync(resourceGroupName, accountName, shareName, skipToken, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// List synchronization details
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='skipToken'>
        /// Continuation token
        /// </param>
        public static Microsoft.Rest.Azure.IPage<SynchronizationDetails> ListSynchronizationDetails(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, ShareSynchronization shareSynchronization, string skipToken = default(string))
        {
                return ((ISharesOperations)operations).ListSynchronizationDetailsAsync(resourceGroupName, accountName, shareName, shareSynchronization, skipToken).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List synchronization details
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='skipToken'>
        /// Continuation token
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Microsoft.Rest.Azure.IPage<SynchronizationDetails>> ListSynchronizationDetailsAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, ShareSynchronization shareSynchronization, string skipToken = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListSynchronizationDetailsWithHttpMessagesAsync(resourceGroupName, accountName, shareName, shareSynchronization, skipToken, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// Delete a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        public static OperationResponse BeginDelete(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName)
        {
                return ((ISharesOperations)operations).BeginDeleteAsync(resourceGroupName, accountName, shareName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Delete a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resourceGroupName'>
        /// The resource group name.
        /// </param>
        /// <param name='accountName'>
        /// The name of the share account.
        /// </param>
        /// <param name='shareName'>
        /// The name of the share.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<OperationResponse> BeginDeleteAsync(this ISharesOperations operations, string resourceGroupName, string accountName, string shareName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.BeginDeleteWithHttpMessagesAsync(resourceGroupName, accountName, shareName, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// List shares in an account
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        public static Microsoft.Rest.Azure.IPage<Share> ListByAccountNext(this ISharesOperations operations, string nextPageLink)
        {
                return ((ISharesOperations)operations).ListByAccountNextAsync(nextPageLink).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List shares in an account
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Microsoft.Rest.Azure.IPage<Share>> ListByAccountNextAsync(this ISharesOperations operations, string nextPageLink, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListByAccountNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// List synchronizations of a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        public static Microsoft.Rest.Azure.IPage<ShareSynchronization> ListSynchronizationsNext(this ISharesOperations operations, string nextPageLink)
        {
                return ((ISharesOperations)operations).ListSynchronizationsNextAsync(nextPageLink).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List synchronizations of a share
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Microsoft.Rest.Azure.IPage<ShareSynchronization>> ListSynchronizationsNextAsync(this ISharesOperations operations, string nextPageLink, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListSynchronizationsNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
        /// <summary>
        /// List synchronization details
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        public static Microsoft.Rest.Azure.IPage<SynchronizationDetails> ListSynchronizationDetailsNext(this ISharesOperations operations, string nextPageLink)
        {
                return ((ISharesOperations)operations).ListSynchronizationDetailsNextAsync(nextPageLink).GetAwaiter().GetResult();
        }

        /// <summary>
        /// List synchronization details
        /// </summary>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async System.Threading.Tasks.Task<Microsoft.Rest.Azure.IPage<SynchronizationDetails>> ListSynchronizationDetailsNextAsync(this ISharesOperations operations, string nextPageLink, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            using (var _result = await operations.ListSynchronizationDetailsNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
