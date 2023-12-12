using Microsoft.Azure.Commands.Common.Authentication.Abstractions;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    /// <summary>
    /// Connection Monitor Resource Details
    /// </summary>
    public class ConnectionMonitorResourceDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public AzureSubscription SubscriptionDetail { get; set; }
    }
}
