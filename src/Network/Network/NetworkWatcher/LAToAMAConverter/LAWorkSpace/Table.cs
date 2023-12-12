using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public class Table
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public IList<Column> Columns { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public IList<IList<string>> Rows { get; set; }

        public Table()
        {
        }

        public Table(string name, IList<Column> columns, IList<IList<string>> rows)
        {
            Name = name;
            Columns = columns;
            Rows = rows;
        }

        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }

            if (Columns == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Columns");
            }

            if (Rows == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Rows");
            }
        }
    }
}
