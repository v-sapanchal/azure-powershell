using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
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
        /// <param name="location">The location name.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="nameContains"></param>
        /// <param name="resourceGroupNameContains"></param>
        public static string CreateFilter(
            string subscriptionId,
            string resourceGroup,
            string resourceType,
            string resourceName,
            string location,
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

            if (!string.IsNullOrWhiteSpace(location))
            {
                if (filterStringBuilder.Length > 0)
                {
                    filterStringBuilder.Append(" AND ");
                }

                filterStringBuilder.AppendFormat("location EQ '{0}'", location);
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
}
