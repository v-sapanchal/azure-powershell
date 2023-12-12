using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public class QueryResponseInnerError
    {
        public int Severity { get; set; }

        public string SeverityName { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }
    }
}
