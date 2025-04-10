
# ----------------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# Code generated by Microsoft (R) AutoRest Code Generator.Changes may cause incorrect behavior and will be lost if the code
# is regenerated.
# ----------------------------------------------------------------------------------

<#
.Synopsis
The operation to update a virtual machine instance.
.Description
The operation to update a virtual machine instance.
.Example
Update-AzScVmmVM -Name "test-vm" -ResourceGroupName "test-rg-01" -CpuCount 4

.Outputs
Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IVirtualMachineInstance
.Notes
COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.

AVAILABILITYSET <IAvailabilitySetListItem[]>: Availability Sets in vm.
  [Id <String>]: Gets the ARM Id of the microsoft.scvmm/availabilitySets resource.
  [Name <String>]: Gets or sets the name of the availability set.

NETWORKPROFILENETWORKINTERFACE <INetworkInterfaceUpdate[]>: Gets or sets the list of network interfaces associated with the virtual machine.
  [Ipv4AddressType <String>]: Gets or sets the ipv4 address type.
  [Ipv6AddressType <String>]: Gets or sets the ipv6 address type.
  [MacAddress <String>]: Gets or sets the nic MAC address.
  [MacAddressType <String>]: Gets or sets the mac address type.
  [Name <String>]: Gets or sets the name of the network interface.
  [NicId <String>]: Gets or sets the nic id.
  [VirtualNetworkId <String>]: Gets or sets the ARM Id of the Microsoft.ScVmm/virtualNetwork resource to connect the nic.

STORAGEPROFILEDISK <IVirtualDiskUpdate[]>: Gets or sets the list of virtual disks associated with the virtual machine.
  [Bus <Int32?>]: Gets or sets the disk bus.
  [BusType <String>]: Gets or sets the disk bus type.
  [DiskId <String>]: Gets or sets the disk id.
  [DiskSizeGb <Int32?>]: Gets or sets the disk total size.
  [Lun <Int32?>]: Gets or sets the disk lun.
  [Name <String>]: Gets or sets the name of the disk.
  [StorageQoSPolicyId <String>]: The ID of the QoS policy.
  [StorageQoSPolicyName <String>]: The name of the policy.
  [VhdType <String>]: Gets or sets the disk vhd type.
.Link
https://learn.microsoft.com/powershell/module/az.scvmm/update-azscvmmvm
#>
function Update-AzScVmmVM {
[OutputType([Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IVirtualMachineInstance])]
[CmdletBinding(DefaultParameterSetName='UpdateExpanded', PositionalBinding=$false, SupportsShouldProcess, ConfirmImpact='Medium')]
param(
    [Parameter(Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Path')]
    [System.String]
    # The fully qualified Azure Resource manager identifier of the resource.
    ${MachineId},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [AllowEmptyCollection()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IAvailabilitySetListItem[]]
    # Availability Sets in vm.
    ${AvailabilitySet},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.Int32]
    # Gets or sets the number of vCPUs for the vm.
    ${HardwareProfileCpuCount},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.PSArgumentCompleterAttribute("true", "false")]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.String]
    # Gets or sets a value indicating whether to enable dynamic memory or not.
    ${HardwareProfileDynamicMemoryEnabled},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.Int32]
    # Gets or sets the max dynamic memory for the vm.
    ${HardwareProfileDynamicMemoryMaxMb},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.Int32]
    # Gets or sets the min dynamic memory for the vm.
    ${HardwareProfileDynamicMemoryMinMb},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.PSArgumentCompleterAttribute("true", "false")]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.String]
    # Gets or sets a value indicating whether to enable processor compatibility mode for live migration of VMs.
    ${HardwareProfileLimitCpuForMigration},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.Int32]
    # MemoryMB is the size of a virtual machine's memory, in MB.
    ${HardwareProfileMemoryMb},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.String]
    # Type of checkpoint supported for the vm.
    ${InfrastructureProfileCheckpointType},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [AllowEmptyCollection()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.INetworkInterfaceUpdate[]]
    # Gets or sets the list of network interfaces associated with the virtual machine.
    ${NetworkProfileNetworkInterface},

    [Parameter(ParameterSetName='UpdateExpanded')]
    [AllowEmptyCollection()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IVirtualDiskUpdate[]]
    # Gets or sets the list of virtual disks associated with the virtual machine.
    ${StorageProfileDisk},

    [Parameter(ParameterSetName='UpdateViaJsonFilePath', Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.String]
    # Path of Json file supplied to the Update operation
    ${JsonFilePath},

    [Parameter(ParameterSetName='UpdateViaJsonString', Mandatory)]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Body')]
    [System.String]
    # Json string supplied to the Update operation
    ${JsonString},

    [Parameter()]
    [Alias('AzureRMContext', 'AzureCredential')]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Azure')]
    [System.Management.Automation.PSObject]
    # The DefaultProfile parameter is not functional.
    # Use the SubscriptionId parameter when available if executing the cmdlet against a different subscription.
    ${DefaultProfile},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [System.Management.Automation.SwitchParameter]
    # Run the command as a job
    ${AsJob},

    [Parameter(DontShow)]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [System.Management.Automation.SwitchParameter]
    # Wait for .NET debugger to attach
    ${Break},

    [Parameter(DontShow)]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Runtime.SendAsyncStep[]]
    # SendAsync Pipeline Steps to be appended to the front of the pipeline
    ${HttpPipelineAppend},

    [Parameter(DontShow)]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Runtime.SendAsyncStep[]]
    # SendAsync Pipeline Steps to be prepended to the front of the pipeline
    ${HttpPipelinePrepend},

    [Parameter()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [System.Management.Automation.SwitchParameter]
    # Run the command asynchronously
    ${NoWait},

    [Parameter(DontShow)]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [System.Uri]
    # The URI for the proxy server to use
    ${Proxy},

    [Parameter(DontShow)]
    [ValidateNotNull()]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [System.Management.Automation.PSCredential]
    # Credentials for a proxy server to use for the remote call
    ${ProxyCredential},

    [Parameter(DontShow)]
    [Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Category('Runtime')]
    [System.Management.Automation.SwitchParameter]
    # Use the default credentials for the proxy
    ${ProxyUseDefaultCredentials}
)

begin {
    try {
        $outBuffer = $null
        if ($PSBoundParameters.TryGetValue('OutBuffer', [ref]$outBuffer)) {
            $PSBoundParameters['OutBuffer'] = 1
        }
        $parameterSet = $PSCmdlet.ParameterSetName
        
        $testPlayback = $false
        $PSBoundParameters['HttpPipelinePrepend'] | Foreach-Object { if ($_) { $testPlayback = $testPlayback -or ('Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Runtime.PipelineMock' -eq $_.Target.GetType().FullName -and 'Playback' -eq $_.Target.Mode) } }

        $mapping = @{
            UpdateExpanded = 'Az.ScVmm.private\Update-AzScVmmVM_UpdateExpanded';
            UpdateViaJsonFilePath = 'Az.ScVmm.private\Update-AzScVmmVM_UpdateViaJsonFilePath';
            UpdateViaJsonString = 'Az.ScVmm.private\Update-AzScVmmVM_UpdateViaJsonString';
        }

        $wrappedCmd = $ExecutionContext.InvokeCommand.GetCommand(($mapping[$parameterSet]), [System.Management.Automation.CommandTypes]::Cmdlet)
        if ($wrappedCmd -eq $null) {
            $wrappedCmd = $ExecutionContext.InvokeCommand.GetCommand(($mapping[$parameterSet]), [System.Management.Automation.CommandTypes]::Function)
        }
        $scriptCmd = {& $wrappedCmd @PSBoundParameters}
        $steppablePipeline = $scriptCmd.GetSteppablePipeline($MyInvocation.CommandOrigin)
        $steppablePipeline.Begin($PSCmdlet)
    } catch {

        throw
    }
}

process {
    try {
        $steppablePipeline.Process($_)
    } catch {

        throw
    }

}
end {
    try {
        $steppablePipeline.End()

    } catch {

        throw
    }
} 
}
