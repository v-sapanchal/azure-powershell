using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public class OperationalInsightsQueryResults
    {
        [JsonProperty(PropertyName = "results")]
        public IEnumerable<IDictionary<string, string>> Results
        {
            get
            {
                foreach (Table table in Tables)
                {
                    foreach (IList<string> row in table.Rows)
                    {
                        yield return table.Columns.Zip(row, (column, cell) => new { column.Name, cell }).ToDictionary(entry => entry.Name, entry => entry.cell);
                    }
                }
            }
        }

        public IDictionary<string, string> Render { get; set; }

        public IDictionary<string, object> Statistics { get; set; }

        public QueryResponseError Error { get; set; }

        [JsonProperty(PropertyName = "tables")]
        public IList<Table> Tables { get; set; }

        public OperationalInsightsQueryResults()
        {
        }

        public OperationalInsightsQueryResults(IList<Table> tables)
        {
            Tables = tables;
        }

        public virtual void Validate()
        {
            if (Tables == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Tables");
            }

            if (Tables == null)
            {
                return;
            }

            foreach (Table table in Tables)
            {
                table?.Validate();
            }
        }
    }
}
