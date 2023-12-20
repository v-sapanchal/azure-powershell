using Microsoft.WindowsAzure.Commands.Common.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.Models
{
    public class PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor : PSConnectionMonitorResult
    {
        [JsonProperty(Order = 1)]
        public new string Id { get; set; }

        [Ps1Xml(Target = ViewControl.List)]
        public List<PSNetworkWatcherConnectionMonitorEndpointObject> Endpoints { get; set; }
    }
}
