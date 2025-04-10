// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models
{
    using static Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Extensions;

    /// <summary>Describes the Run Commands List Result.</summary>
    public partial class MachineRunCommandsListResult :
        Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommandsListResult,
        Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommandsListResultInternal
    {

        /// <summary>Backing field for <see cref="NextLink" /> property.</summary>
        private string _nextLink;

        /// <summary>
        /// The uri to fetch the next page of run commands. Call ListNext() with this to fetch the next page of run commands.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Origin(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.PropertyOrigin.Owned)]
        public string NextLink { get => this._nextLink; set => this._nextLink = value; }

        /// <summary>Backing field for <see cref="Value" /> property.</summary>
        private System.Collections.Generic.List<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommand> _value;

        /// <summary>The list of run commands</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Origin(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.PropertyOrigin.Owned)]
        public System.Collections.Generic.List<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommand> Value { get => this._value; set => this._value = value; }

        /// <summary>Creates an new <see cref="MachineRunCommandsListResult" /> instance.</summary>
        public MachineRunCommandsListResult()
        {

        }
    }
    /// Describes the Run Commands List Result.
    public partial interface IMachineRunCommandsListResult :
        Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.IJsonSerializable
    {
        /// <summary>
        /// The uri to fetch the next page of run commands. Call ListNext() with this to fetch the next page of run commands.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = false,
        ReadOnly = false,
        Read = true,
        Create = true,
        Update = true,
        Description = @"The uri to fetch the next page of run commands. Call ListNext() with this to fetch the next page of run commands.",
        SerializedName = @"nextLink",
        PossibleTypes = new [] { typeof(string) })]
        string NextLink { get; set; }
        /// <summary>The list of run commands</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Runtime.Info(
        Required = false,
        ReadOnly = false,
        Read = true,
        Create = true,
        Update = true,
        Description = @"The list of run commands",
        SerializedName = @"value",
        PossibleTypes = new [] { typeof(Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommand) })]
        System.Collections.Generic.List<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommand> Value { get; set; }

    }
    /// Describes the Run Commands List Result.
    internal partial interface IMachineRunCommandsListResultInternal

    {
        /// <summary>
        /// The uri to fetch the next page of run commands. Call ListNext() with this to fetch the next page of run commands.
        /// </summary>
        string NextLink { get; set; }
        /// <summary>The list of run commands</summary>
        System.Collections.Generic.List<Microsoft.Azure.PowerShell.Cmdlets.ArcGateway.Models.IMachineRunCommand> Value { get; set; }

    }
}