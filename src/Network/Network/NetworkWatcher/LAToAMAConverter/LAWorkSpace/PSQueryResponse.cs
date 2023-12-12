using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
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
    }
}
