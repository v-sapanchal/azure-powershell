using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
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
