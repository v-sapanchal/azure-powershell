using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public class ApiPreferences
    {
        public bool IncludeRender { get; set; }

        public bool IncludeStatistics { get; set; }

        public int Wait { get; set; } = int.MinValue;

        public override string ToString()
        {
            string text = "response-v1=true";
            if (IncludeRender)
            {
                text += ",include-render=true";
            }

            if (IncludeStatistics)
            {
                text += ",include-statistics=true";
            }

            if (Wait != int.MinValue)
            {
                text += $",wait={Wait}";
            }

            return text;
        }
    }
}
