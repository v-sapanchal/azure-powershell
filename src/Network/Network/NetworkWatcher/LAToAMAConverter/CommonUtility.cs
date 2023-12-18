using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    public static class CommonUtility
    {
        public const string ParamSetNameByWorkspaceId = "ByWorkspaceId";
        public const string ParamSetNameByWorkspaceObject = "ByWorkspaceObject";
        public const string Query = "NetworkMonitoring | join kind=inner (Heartbeat) on $left.AgentId== $right.SourceComputerId | where SubType == \"NetworkAgent\" | project TimeGenerated, SubType, SubnetId, AgentFqdn, AgentId, AgentIP, ResourceId";
        public const string QueryWithFilter = "NetworkMonitoring | join kind=inner (Heartbeat) on $left.AgentId== $right.SourceComputerId | where SubType == \"NetworkAgent\" {0} | project TimeGenerated, SubType, SubnetId, AgentFqdn, AgentId, AgentIP, ResourceId";
        public static TimeSpan TimeSpanForLAQuery { get; } = TimeSpan.FromDays(1);
    }
}
