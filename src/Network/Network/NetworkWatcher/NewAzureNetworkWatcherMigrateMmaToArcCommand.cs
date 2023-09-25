namespace Microsoft.Azure.Commands.Network
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
    using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
    using Microsoft.Azure.Commands.Common.Authentication;
    using Microsoft.Azure.Commands.Network.Models;
    using Microsoft.Azure.Management.Network;
    using Microsoft.Azure.Management.ResourceGraph;
    using Microsoft.Azure.Management.ResourceGraph.Models;
    using Newtonsoft.Json;

    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "AzureNetworkWatcherMigrateMmaToArc"), OutputType(typeof(PSAzureNetworkWatcherMigrateMmaToArc))]
    public class NewAzureNetworkWatcherMigrateMmaToArcCommand : NetworkWatcherBaseCmdlet
    {

        /// <summary>
        /// Gets or sets the query.
        /// </summary>s
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Resource Graph query")]
        [AllowEmptyString]
        public string Query
        {
            get;
            set;
        }

        public override void Execute()
        {
            base.Execute();
            this.QueryForArg(this.Query);
        }

        public void QueryForArg(string query)
        {
            ResourceGraphClient rgClient = AzureSession.Instance.ClientFactory.CreateArmClient<ResourceGraphClient>(DefaultContext, AzureEnvironment.Endpoint.ResourceManager);
            QueryRequest request = new QueryRequest
            {
                Query = query
            };
            QueryResponse response = rgClient.Resources(request);
            var data = JsonConvert.DeserializeObject<object>(response.Data.ToString());
            WriteObject(data);
        }
    }

}
