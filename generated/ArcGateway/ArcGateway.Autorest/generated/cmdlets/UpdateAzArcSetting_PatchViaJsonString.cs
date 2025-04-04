// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Cmdlets
{
    using static Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Extensions;
    using Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.PowerShell;
    using Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Cmdlets;
    using System;

    /// <summary>patch the base Settings of the target resource.</summary>
    /// <remarks>
    /// [OpenAPI] Patch=>PATCH:"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/{baseProvider}/{baseResourceType}/{baseResourceName}/providers/Microsoft.HybridCompute/settings/{settingsResourceName}"
    /// </remarks>
    [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.InternalExport]
    [global::System.Management.Automation.Cmdlet(global::System.Management.Automation.VerbsData.Update, @"AzArcSetting_PatchViaJsonString", SupportsShouldProcess = true)]
    [global::System.Management.Automation.OutputType(typeof(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings))]
    [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Description(@"patch the base Settings of the target resource.")]
    [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Generated]
    [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.HttpPath(Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/{baseProvider}/{baseResourceType}/{baseResourceName}/providers/Microsoft.HybridCompute/settings/{settingsResourceName}", ApiVersion = "2024-07-31-preview")]
    [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.NotSuggestDefaultParameterSet]
    public partial class UpdateAzArcSetting_PatchViaJsonString : global::System.Management.Automation.PSCmdlet,
        Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener,
        Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IContext
    {
        /// <summary>A unique id generatd for the this cmdlet when it is instantiated.</summary>
        private string __correlationId = System.Guid.NewGuid().ToString();

        /// <summary>A copy of the Invocation Info (necessary to allow asJob to clone this cmdlet)</summary>
        private global::System.Management.Automation.InvocationInfo __invocationInfo;

        /// <summary>A unique id generatd for the this cmdlet when ProcessRecord() is called.</summary>
        private string __processRecordId;

        /// <summary>
        /// The <see cref="global::System.Threading.CancellationTokenSource" /> for this operation.
        /// </summary>
        private global::System.Threading.CancellationTokenSource _cancellationTokenSource = new global::System.Threading.CancellationTokenSource();

        /// <summary>A dictionary to carry over additional data for pipeline.</summary>
        private global::System.Collections.Generic.Dictionary<global::System.String,global::System.Object> _extensibleParameters = new System.Collections.Generic.Dictionary<string, object>();

        /// <summary>A buffer to record first returned object in response.</summary>
        private object _firstResponse = null;

        /// <summary>
        /// A flag to tell whether it is the first returned object in a call. Zero means no response yet. One means 1 returned object.
        /// Two means multiple returned objects in response.
        /// </summary>
        private int _responseSize = 0;

        /// <summary>Backing field for <see cref="BaseProvider" /> property.</summary>
        private string _baseProvider;

        /// <summary>The name of the base Resource Provider.</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "The name of the base Resource Provider.")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"The name of the base Resource Provider.",
        SerializedName = @"baseProvider",
        PossibleTypes = new [] { typeof(string) })]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Path)]
        public string BaseProvider { get => this._baseProvider; set => this._baseProvider = value; }

        /// <summary>Backing field for <see cref="BaseResourceName" /> property.</summary>
        private string _baseResourceName;

        /// <summary>The name of the base resource.</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "The name of the base resource.")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"The name of the base resource.",
        SerializedName = @"baseResourceName",
        PossibleTypes = new [] { typeof(string) })]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Path)]
        public string BaseResourceName { get => this._baseResourceName; set => this._baseResourceName = value; }

        /// <summary>Backing field for <see cref="BaseResourceType" /> property.</summary>
        private string _baseResourceType;

        /// <summary>The name of the base Resource Type.</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "The name of the base Resource Type.")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"The name of the base Resource Type.",
        SerializedName = @"baseResourceType",
        PossibleTypes = new [] { typeof(string) })]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Path)]
        public string BaseResourceType { get => this._baseResourceType; set => this._baseResourceType = value; }

        /// <summary>Wait for .NET debugger to attach</summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, DontShow = true, HelpMessage = "Wait for .NET debugger to attach")]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Runtime)]
        public global::System.Management.Automation.SwitchParameter Break { get; set; }

        /// <summary>Accessor for cancellationTokenSource.</summary>
        public global::System.Threading.CancellationTokenSource CancellationTokenSource { get => _cancellationTokenSource ; set { _cancellationTokenSource = value; } }

        /// <summary>The reference to the client API class.</summary>
        public Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ArcGateway Client => Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.ClientAPI;

        /// <summary>
        /// The DefaultProfile parameter is not functional. Use the SubscriptionId parameter when available if executing the cmdlet
        /// against a different subscription
        /// </summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, HelpMessage = "The DefaultProfile parameter is not functional. Use the SubscriptionId parameter when available if executing the cmdlet against a different subscription.")]
        [global::System.Management.Automation.ValidateNotNull]
        [global::System.Management.Automation.Alias("AzureRMContext", "AzureCredential")]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Azure)]
        public global::System.Management.Automation.PSObject DefaultProfile { get; set; }

        /// <summary>Accessor for extensibleParameters.</summary>
        public global::System.Collections.Generic.IDictionary<global::System.String,global::System.Object> ExtensibleParameters { get => _extensibleParameters ; }

        /// <summary>SendAsync Pipeline Steps to be appended to the front of the pipeline</summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, DontShow = true, HelpMessage = "SendAsync Pipeline Steps to be appended to the front of the pipeline")]
        [global::System.Management.Automation.ValidateNotNull]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Runtime)]
        public Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.SendAsyncStep[] HttpPipelineAppend { get; set; }

        /// <summary>SendAsync Pipeline Steps to be prepended to the front of the pipeline</summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, DontShow = true, HelpMessage = "SendAsync Pipeline Steps to be prepended to the front of the pipeline")]
        [global::System.Management.Automation.ValidateNotNull]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Runtime)]
        public Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.SendAsyncStep[] HttpPipelinePrepend { get; set; }

        /// <summary>Accessor for our copy of the InvocationInfo.</summary>
        public global::System.Management.Automation.InvocationInfo InvocationInformation { get => __invocationInfo = __invocationInfo ?? this.MyInvocation ; set { __invocationInfo = value; } }

        /// <summary>Backing field for <see cref="JsonString" /> property.</summary>
        private string _jsonString;

        /// <summary>Json string supplied to the Patch operation</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "Json string supplied to the Patch operation")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"Json string supplied to the Patch operation",
        SerializedName = @"JsonString",
        PossibleTypes = new [] { typeof(string) })]
        public string JsonString { get => this._jsonString; set => this._jsonString = value; }

        /// <summary>
        /// <see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener" /> cancellation delegate. Stops the cmdlet when called.
        /// </summary>
        global::System.Action Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener.Cancel => _cancellationTokenSource.Cancel;

        /// <summary><see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener" /> cancellation token.</summary>
        global::System.Threading.CancellationToken Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener.Token => _cancellationTokenSource.Token;

        /// <summary>
        /// The instance of the <see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.HttpPipeline" /> that the remote call will use.
        /// </summary>
        public Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.HttpPipeline Pipeline { get; set; }

        /// <summary>The URI for the proxy server to use</summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, DontShow = true, HelpMessage = "The URI for the proxy server to use")]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Runtime)]
        public global::System.Uri Proxy { get; set; }

        /// <summary>Credentials for a proxy server to use for the remote call</summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, DontShow = true, HelpMessage = "Credentials for a proxy server to use for the remote call")]
        [global::System.Management.Automation.ValidateNotNull]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Runtime)]
        public global::System.Management.Automation.PSCredential ProxyCredential { get; set; }

        /// <summary>Use the default credentials for the proxy</summary>
        [global::System.Management.Automation.Parameter(Mandatory = false, DontShow = true, HelpMessage = "Use the default credentials for the proxy")]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Runtime)]
        public global::System.Management.Automation.SwitchParameter ProxyUseDefaultCredentials { get; set; }

        /// <summary>Backing field for <see cref="ResourceGroupName" /> property.</summary>
        private string _resourceGroupName;

        /// <summary>The name of the resource group. The name is case insensitive.</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "The name of the resource group. The name is case insensitive.")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"The name of the resource group. The name is case insensitive.",
        SerializedName = @"resourceGroupName",
        PossibleTypes = new [] { typeof(string) })]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Path)]
        public string ResourceGroupName { get => this._resourceGroupName; set => this._resourceGroupName = value; }

        /// <summary>Backing field for <see cref="SResourceName" /> property.</summary>
        private string _sResourceName;

        /// <summary>The name of the settings resource.</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "The name of the settings resource.")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"The name of the settings resource.",
        SerializedName = @"settingsResourceName",
        PossibleTypes = new [] { typeof(string) })]
        [global::System.Management.Automation.Alias("SettingsResourceName")]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Path)]
        public string SResourceName { get => this._sResourceName; set => this._sResourceName = value; }

        /// <summary>Backing field for <see cref="SubscriptionId" /> property.</summary>
        private string _subscriptionId;

        /// <summary>The ID of the target subscription.</summary>
        [global::System.Management.Automation.Parameter(Mandatory = true, HelpMessage = "The ID of the target subscription.")]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = true,
        ReadOnly = false,
        Description = @"The ID of the target subscription.",
        SerializedName = @"subscriptionId",
        PossibleTypes = new [] { typeof(string) })]
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.DefaultInfo(
        Name = @"",
        Description =@"",
        Script = @"(Get-AzContext).Subscription.Id",
        SetCondition = @"")]
        [global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Category(global::Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.ParameterCategory.Path)]
        public string SubscriptionId { get => this._subscriptionId; set => this._subscriptionId = value; }

        /// <summary>
        /// <c>overrideOnDefault</c> will be called before the regular onDefault has been processed, allowing customization of what
        /// happens on that response. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="responseMessage">the raw response message as an global::System.Net.Http.HttpResponseMessage.</param>
        /// <param name="response">the body result as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse">Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse</see>
        /// from the remote call</param>
        /// <param name="returnNow">/// Determines if the rest of the onDefault method should be processed, or if the method should
        /// return immediately (set to true to skip further processing )</param>

        partial void overrideOnDefault(global::System.Net.Http.HttpResponseMessage responseMessage, global::System.Threading.Tasks.Task<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse> response, ref global::System.Threading.Tasks.Task<bool> returnNow);

        /// <summary>
        /// <c>overrideOnOk</c> will be called before the regular onOk has been processed, allowing customization of what happens
        /// on that response. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="responseMessage">the raw response message as an global::System.Net.Http.HttpResponseMessage.</param>
        /// <param name="response">the body result as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings">Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings</see>
        /// from the remote call</param>
        /// <param name="returnNow">/// Determines if the rest of the onOk method should be processed, or if the method should return
        /// immediately (set to true to skip further processing )</param>

        partial void overrideOnOk(global::System.Net.Http.HttpResponseMessage responseMessage, global::System.Threading.Tasks.Task<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings> response, ref global::System.Threading.Tasks.Task<bool> returnNow);

        /// <summary>
        /// (overrides the default BeginProcessing method in global::System.Management.Automation.PSCmdlet)
        /// </summary>
        protected override void BeginProcessing()
        {
            var telemetryId = Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.GetTelemetryId.Invoke();
            if (telemetryId != "" && telemetryId != "internal")
            {
                __correlationId = telemetryId;
            }
            Module.Instance.SetProxyConfiguration(Proxy, ProxyCredential, ProxyUseDefaultCredentials);
            if (Break)
            {
                Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.AttachDebugger.Break();
            }
            ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletBeginProcessing).Wait(); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
        }

        /// <summary>Performs clean-up after the command execution</summary>
        protected override void EndProcessing()
        {
            if (1 ==_responseSize)
            {
                // Flush buffer
                WriteObject(_firstResponse);
            }
            var telemetryInfo = Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.GetTelemetryInfo?.Invoke(__correlationId);
            if (telemetryInfo != null)
            {
                telemetryInfo.TryGetValue("ShowSecretsWarning", out var showSecretsWarning);
                telemetryInfo.TryGetValue("SanitizedProperties", out var sanitizedProperties);
                telemetryInfo.TryGetValue("InvocationName", out var invocationName);
                if (showSecretsWarning == "true")
                {
                    if (string.IsNullOrEmpty(sanitizedProperties))
                    {
                        WriteWarning($"The output of cmdlet {invocationName} may compromise security by showing secrets. Learn more at https://go.microsoft.com/fwlink/?linkid=2258844");
                    }
                    else
                    {
                        WriteWarning($"The output of cmdlet {invocationName} may compromise security by showing the following secrets: {sanitizedProperties}. Learn more at https://go.microsoft.com/fwlink/?linkid=2258844");
                    }
                }
            }
        }

        /// <summary>Handles/Dispatches events during the call to the REST service.</summary>
        /// <param name="id">The message id</param>
        /// <param name="token">The message cancellation token. When this call is cancelled, this should be <c>true</c></param>
        /// <param name="messageData">Detailed message data for the message event.</param>
        /// <returns>
        /// A <see cref="global::System.Threading.Tasks.Task" /> that will be complete when handling of the message is completed.
        /// </returns>
         async global::System.Threading.Tasks.Task Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener.Signal(string id, global::System.Threading.CancellationToken token, global::System.Func<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.EventData> messageData)
        {
            using( NoSynchronizationContext )
            {
                if (token.IsCancellationRequested)
                {
                    return ;
                }

                switch ( id )
                {
                    case Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.Verbose:
                    {
                        WriteVerbose($"{(messageData().Message ?? global::System.String.Empty)}");
                        return ;
                    }
                    case Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.Warning:
                    {
                        WriteWarning($"{(messageData().Message ?? global::System.String.Empty)}");
                        return ;
                    }
                    case Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.Information:
                    {
                        var data = messageData();
                        WriteInformation(data.Message, new string[]{});
                        return ;
                    }
                    case Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.Debug:
                    {
                        WriteDebug($"{(messageData().Message ?? global::System.String.Empty)}");
                        return ;
                    }
                    case Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.Error:
                    {
                        WriteError(new global::System.Management.Automation.ErrorRecord( new global::System.Exception(messageData().Message), string.Empty, global::System.Management.Automation.ErrorCategory.NotSpecified, null ) );
                        return ;
                    }
                    case Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.Progress:
                    {
                        var data = messageData();
                        int progress = (int)data.Value;
                        string activityMessage, statusDescription;
                        global::System.Management.Automation.ProgressRecordType recordType;
                        if (progress < 100)
                        {
                            activityMessage = "In progress";
                            statusDescription = "Checking operation status";
                            recordType = System.Management.Automation.ProgressRecordType.Processing;
                        }
                        else
                        {
                            activityMessage = "Completed";
                            statusDescription = "Completed";
                            recordType = System.Management.Automation.ProgressRecordType.Completed;
                        }
                        WriteProgress(new global::System.Management.Automation.ProgressRecord(1, activityMessage, statusDescription)
                        {
                            PercentComplete = progress,
                        RecordType = recordType
                        });
                        return ;
                    }
                }
                await Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.Signal(id, token, messageData, (i, t, m) => ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(i, t, () => Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.EventDataConverter.ConvertFrom(m()) as Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.EventData), InvocationInformation, this.ParameterSetName, __correlationId, __processRecordId, null );
                if (token.IsCancellationRequested)
                {
                    return ;
                }
                WriteDebug($"{id}: {(messageData().Message ?? global::System.String.Empty)}");
            }
        }

        /// <summary>Performs execution of the command.</summary>
        protected override void ProcessRecord()
        {
            ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletProcessRecordStart).Wait(); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
            __processRecordId = System.Guid.NewGuid().ToString();
            try
            {
                // work
                if (ShouldProcess($"Call remote 'SettingsPatch' operation"))
                {
                    using( var asyncCommandRuntime = new Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.PowerShell.AsyncCommandRuntime(this, ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token) )
                    {
                        asyncCommandRuntime.Wait( ProcessRecordAsync(),((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token);
                    }
                }
            }
            catch (global::System.AggregateException aggregateException)
            {
                // unroll the inner exceptions to get the root cause
                foreach( var innerException in aggregateException.Flatten().InnerExceptions )
                {
                    ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletException, $"{innerException.GetType().Name} - {innerException.Message} : {innerException.StackTrace}").Wait(); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
                    // Write exception out to error channel.
                    WriteError( new global::System.Management.Automation.ErrorRecord(innerException,string.Empty, global::System.Management.Automation.ErrorCategory.NotSpecified, null) );
                }
            }
            catch (global::System.Exception exception) when ((exception as System.Management.Automation.PipelineStoppedException)== null || (exception as System.Management.Automation.PipelineStoppedException).InnerException != null)
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletException, $"{exception.GetType().Name} - {exception.Message} : {exception.StackTrace}").Wait(); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
                // Write exception out to error channel.
                WriteError( new global::System.Management.Automation.ErrorRecord(exception,string.Empty, global::System.Management.Automation.ErrorCategory.NotSpecified, null) );
            }
            finally
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletProcessRecordEnd).Wait();
            }
        }

        /// <summary>Performs execution of the command, working asynchronously if required.</summary>
        /// <returns>
        /// A <see cref="global::System.Threading.Tasks.Task" /> that will be complete when handling of the method is completed.
        /// </returns>
        protected async global::System.Threading.Tasks.Task ProcessRecordAsync()
        {
            using( NoSynchronizationContext )
            {
                await ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletGetPipeline); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
                Pipeline = Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.CreatePipeline(InvocationInformation, __correlationId, __processRecordId, this.ParameterSetName, this.ExtensibleParameters);
                if (null != HttpPipelinePrepend)
                {
                    Pipeline.Prepend((this.CommandRuntime as Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.PowerShell.IAsyncCommandRuntimeExtensions)?.Wrap(HttpPipelinePrepend) ?? HttpPipelinePrepend);
                }
                if (null != HttpPipelineAppend)
                {
                    Pipeline.Append((this.CommandRuntime as Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.PowerShell.IAsyncCommandRuntimeExtensions)?.Wrap(HttpPipelineAppend) ?? HttpPipelineAppend);
                }
                // get the client instance
                try
                {
                    await ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletBeforeAPICall); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
                    await this.Client.SettingsPatchViaJsonString(SubscriptionId, ResourceGroupName, BaseProvider, BaseResourceType, BaseResourceName, SResourceName, _jsonString, onOk, onDefault, this, Pipeline);
                    await ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletAfterAPICall); if( ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Token.IsCancellationRequested ) { return; }
                }
                catch (Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.UndeclaredResponseException urexception)
                {
                    WriteError(new global::System.Management.Automation.ErrorRecord(urexception, urexception.StatusCode.ToString(), global::System.Management.Automation.ErrorCategory.InvalidOperation, new { SubscriptionId=SubscriptionId,ResourceGroupName=ResourceGroupName,BaseProvider=BaseProvider,BaseResourceType=BaseResourceType,BaseResourceName=BaseResourceName,SResourceName=SResourceName})
                    {
                      ErrorDetails = new global::System.Management.Automation.ErrorDetails(urexception.Message) { RecommendedAction = urexception.Action }
                    });
                }
                finally
                {
                    await ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Signal(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Events.CmdletProcessRecordAsyncEnd);
                }
            }
        }

        /// <summary>Interrupts currently running code within the command.</summary>
        protected override void StopProcessing()
        {
            ((Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IEventListener)this).Cancel();
            base.StopProcessing();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAzArcSetting_PatchViaJsonString" /> cmdlet class.
        /// </summary>
        public UpdateAzArcSetting_PatchViaJsonString()
        {

        }

        /// <param name="sendToPipeline"></param>
        new protected void WriteObject(object sendToPipeline)
        {
            Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.SanitizeOutput?.Invoke(sendToPipeline, __correlationId);
            base.WriteObject(sendToPipeline);
        }

        /// <param name="sendToPipeline"></param>
        /// <param name="enumerateCollection"></param>
        new protected void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
            Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Module.Instance.SanitizeOutput?.Invoke(sendToPipeline, __correlationId);
            base.WriteObject(sendToPipeline, enumerateCollection);
        }

        /// <summary>
        /// a delegate that is called when the remote service returns default (any response code not handled elsewhere).
        /// </summary>
        /// <param name="responseMessage">the raw response message as an global::System.Net.Http.HttpResponseMessage.</param>
        /// <param name="response">the body result as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse">Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse</see>
        /// from the remote call</param>
        /// <returns>
        /// A <see cref="global::System.Threading.Tasks.Task" /> that will be complete when handling of the method is completed.
        /// </returns>
        private async global::System.Threading.Tasks.Task onDefault(global::System.Net.Http.HttpResponseMessage responseMessage, global::System.Threading.Tasks.Task<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse> response)
        {
            using( NoSynchronizationContext )
            {
                var _returnNow = global::System.Threading.Tasks.Task<bool>.FromResult(false);
                overrideOnDefault(responseMessage, response, ref _returnNow);
                // if overrideOnDefault has returned true, then return right away.
                if ((null != _returnNow && await _returnNow))
                {
                    return ;
                }
                // Error Response : default
                var code = (await response)?.Code;
                var message = (await response)?.Message;
                if ((null == code || null == message))
                {
                    // Unrecognized Response. Create an error record based on what we have.
                    var ex = new Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.RestException<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IErrorResponse>(responseMessage, await response);
                    WriteError( new global::System.Management.Automation.ErrorRecord(ex, ex.Code, global::System.Management.Automation.ErrorCategory.InvalidOperation, new {  })
                    {
                      ErrorDetails = new global::System.Management.Automation.ErrorDetails(ex.Message) { RecommendedAction = ex.Action }
                    });
                }
                else
                {
                    WriteError( new global::System.Management.Automation.ErrorRecord(new global::System.Exception($"[{code}] : {message}"), code?.ToString(), global::System.Management.Automation.ErrorCategory.InvalidOperation, new {  })
                    {
                      ErrorDetails = new global::System.Management.Automation.ErrorDetails(message) { RecommendedAction = global::System.String.Empty }
                    });
                }
            }
        }

        /// <summary>a delegate that is called when the remote service returns 200 (OK).</summary>
        /// <param name="responseMessage">the raw response message as an global::System.Net.Http.HttpResponseMessage.</param>
        /// <param name="response">the body result as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings">Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings</see>
        /// from the remote call</param>
        /// <returns>
        /// A <see cref="global::System.Threading.Tasks.Task" /> that will be complete when handling of the method is completed.
        /// </returns>
        private async global::System.Threading.Tasks.Task onOk(global::System.Net.Http.HttpResponseMessage responseMessage, global::System.Threading.Tasks.Task<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings> response)
        {
            using( NoSynchronizationContext )
            {
                var _returnNow = global::System.Threading.Tasks.Task<bool>.FromResult(false);
                overrideOnOk(responseMessage, response, ref _returnNow);
                // if overrideOnOk has returned true, then return right away.
                if ((null != _returnNow && await _returnNow))
                {
                    return ;
                }
                // onOk - response for 200 / application/json
                // (await response) // should be Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.ISettings
                var result = (await response);
                if (null != result)
                {
                    if (0 == _responseSize)
                    {
                        _firstResponse = result;
                        _responseSize = 1;
                    }
                    else
                    {
                        if (1 ==_responseSize)
                        {
                            // Flush buffer
                            WriteObject(_firstResponse.AddMultipleTypeNameIntoPSObject());
                        }
                        WriteObject(result.AddMultipleTypeNameIntoPSObject());
                        _responseSize = 2;
                    }
                }
            }
        }
    }
}