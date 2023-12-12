using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.LAWorkSpace
{
    public class QueryResponseError
    {
        public string Code { get; set; }

        public List<QueryResponseError> Details { get; set; }

        public string Message { get; set; }

        public QueryResponseInnerError InnerError { get; set; }
    }
}
