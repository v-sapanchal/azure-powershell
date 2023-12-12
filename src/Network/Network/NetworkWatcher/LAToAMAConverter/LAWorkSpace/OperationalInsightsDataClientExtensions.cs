using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public static class OperationalInsightsDataClientExtensions
    {
        public static OperationalInsightsQueryResults Query(this OperationalInsightsDataClient operations, string query, TimeSpan? timespan = null, IList<string> workspaces = null)
        {
            return operations.QueryAsync(query, timespan, workspaces).GetAwaiter().GetResult();
        }

        public static async Task<OperationalInsightsQueryResults> QueryAsync(this OperationalInsightsDataClient operations, string query, TimeSpan? timespan = null, IList<string> workspaces = null, CancellationToken cancellationToken = default)
        {
            using (HttpOperationResponse<OperationalInsightsQueryResults> httpOperationResponse = await operations.QueryWithHttpMessagesAsync(query, timespan, workspaces, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
            {
                return httpOperationResponse.Body;
            }
        }
    }
}
