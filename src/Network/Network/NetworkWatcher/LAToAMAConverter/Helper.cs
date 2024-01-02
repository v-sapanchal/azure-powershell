using Microsoft.Azure.OperationalInsights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    public static class Helper
    {
        public static IEnumerable<string> GetRowsByColumnName(this IEnumerable<Table> allData, string columnName, int noOfTakeResult)
        {
            return allData?
                .SelectMany(s => s.Rows.Take(noOfTakeResult)
                .Select(row => $"'{row[s.Columns.IndexOf(s.Columns.First(c => c.Name == columnName))]}'"));
        }
    }
}
