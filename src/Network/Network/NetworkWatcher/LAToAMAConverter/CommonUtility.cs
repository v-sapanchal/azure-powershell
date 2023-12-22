using System;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    public static class CommonUtility
    {
        public const string ParamSetNameByWorkspaceId = "ByWorkspaceId";
        public const string ParamSetNameByWorkspaceObject = "ByWorkspaceObject";
        public const string Query = "NetworkMonitoring | join kind=inner (Heartbeat) on $left.AgentId== $right.SourceComputerId | where SubType == \"NetworkAgent\" | distinct SubType, SubnetId, AgentFqdn, AgentId, AgentIP, ResourceId";
        public const string QueryWithFilter = "NetworkMonitoring | join kind=inner (Heartbeat) on $left.AgentId== $right.SourceComputerId | where SubType == \"NetworkAgent\" {0} | distinct SubType, SubnetId, AgentFqdn, AgentId, AgentIP, ResourceId";
        public const string CustomQueryForArg = "resources | where ['type'] == 'microsoft.hybridcompute/machines' and ['id'] in ({0})";
        public const string ConnectionMonitorResourceType = "Microsoft.Network/networkWatchers/connectionMonitors";
        public const string EndpointResourceType = "MMAWorkspaceMachine";
        public const string MMAWorkspaceNetworkEndpointResourceType = "MMAWorkspaceNetwork";
        public static TimeSpan TimeSpanForLAQuery { get; } = TimeSpan.FromDays(1);
    }
}
