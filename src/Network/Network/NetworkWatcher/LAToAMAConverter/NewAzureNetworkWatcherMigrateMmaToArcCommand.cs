using System;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Network.Models;
using System.Linq;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.Common.Authentication.ResourceManager;
using Microsoft.Azure.Management.Network.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json.Linq;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    [Cmdlet("New", AzureRMConstants.AzureRMPrefix + "AzureNetworkWatcherMigrateMmaToArc"), OutputType(typeof(PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor))]
    public class NewAzureNetworkWatcherMigrateMmaToArcCommand : LaToAmaConnectionMonitorBaseCmdlet
    {

        private IAzureTokenCache _cache;
        private IProfileOperations _profile;

        /// <summary>
        /// Gets or sets the query.
        /// </summary>sub
        [Parameter(Mandatory = false, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Resource Graph query")]
        [AllowEmptyString]
        public string Query
        {
            get;
            set;
        }

        [Parameter(Mandatory = false, ParameterSetName = CommonConstants.ParamSetNameByWorkspaceId, HelpMessage = "The workspace ID.")]
        [ValidateNotNullOrEmpty]
        public string WorkspaceId { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The timespan to bound the query by, pass any number in hours.")]
        public int TimespanInHrs { get; set; }


        [Parameter(Mandatory = true, HelpMessage = "List of MMA machine connection monitor.")]
        [ValidateNotNullOrEmpty]
        public PSNetworkWatcherMmaWorkspaceMachineConnectionMonitor[] MMAWorkspaceConnectionMonitors { get; set; }

        public override void Execute()
        {
            base.Execute();
            _cache = AzureSession.Instance.TokenCache;
            _profile = AzureRmProfileProvider.Instance.GetProfile<AzureRmProfile>();
            var profileClient = new RMProfileClient(_profile);
            SubscriptionAndTenantClient = profileClient.SubscriptionAndTenantClient;
            var endpoint = DefaultContext.Environment.GetEndpoint(AzureEnvironment.Endpoint.ResourceManager);
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ApplicationException(
                    "The endpoint for the Azure Resource Manager service is not set. Please report this issue via GitHub or contact Microsoft customer support.");
            }

            if (MMAWorkspaceConnectionMonitors?.Count() > 0)
            {
                var cmWithArmEndpoints = MigrateCMs(MMAWorkspaceConnectionMonitors).GetAwaiter().GetResult();
                WriteObject(cmWithArmEndpoints);

                List<ConnectionMonitorResult> outputCMs = cmWithArmEndpoints.Select(cm => MapPSMmaWorkspaceMachineConnectionMonitorToConnectionMonitorResult(cm)).ToList();

                string template = ARMTemplateForConnectionMonitors(outputCMs);
                WriteInformation($" Template -----------------------------------------------------------\n", new string[] { "PSHOST" });
                WriteInformation($" {template} \n", new string[] { "PSHOST" });
            }
        }

        private string ARMTemplateForConnectionMonitors(List<ConnectionMonitorResult> connectionMonitors)
        {
            List<string> cms = new List<string>();

            JObject jo = new JObject();
            jo.Add("$schema", "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#");
            jo.Add("contentVersion", "1.0.0.0");
            jo.Add("parameters", new JObject());

            JArray jarray = new JArray();

            connectionMonitors.ForEach(monitor =>
            {
                string s = CreateArmTemplateForCM(monitor);
                jarray.Add(s);
                cms.Add(s);
            });

            string cmTemplate = string.Join(",", cms);
            jo.Add("resources", jarray);
            string armTemplate = jo.ToString();
            armTemplate = armTemplate.Replace("\\\"", "\"");

            //Using JToken, it adds extra "" due to which template becomes invalid, hence trying to create template without JToken.

            string armt = "{\"$schema\":\"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#\",\"contentVersion\":\"1.0.0.0\",\"parameters\":{},\"resources\":[";
            armt += cmTemplate;
            armt += "]}";

            // this armt is final template which directly can be used to deploy.
            Console.WriteLine("writing without jtoken");
            Console.WriteLine(armt);
            Console.WriteLine("----------------------");

            return armt;
        }

        // Method to create ARM template for a ConnectionMonitor
        private string CreateArmTemplateForCM(ConnectionMonitorResult cm)
        {
            JsonSerializerSettings serializationSettings = new JsonSerializerSettings
            {
                //Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                // ContractResolver = new ReadOnlyJsonContractResolver(),
                ContractResolver = new IgnorePropertiesResolver(new List<string>() { "id", "etag", "properties.connectionMonitorType", "properties.startTime", "properties.provisioningState" }),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            serializationSettings.Converters.Add(new TransformationJsonConverter());

            string cmResultJson = JsonConvert.SerializeObject(cm, serializationSettings);
            JObject jo = JObject.Parse(cmResultJson);
            jo["apiVersion"] = "2020-11-01";
            jo["name"] = string.Concat("networkwatcher_", cm.Location, "/", cm.Name);
            cmResultJson = jo.ToString(Formatting.None);
            return cmResultJson;
        }
    }
}
