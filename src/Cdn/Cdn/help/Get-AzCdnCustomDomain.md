---
external help file: Az.Cdn-help.xml
Module Name: Az.Cdn
online version: https://learn.microsoft.com/powershell/module/az.cdn/get-azcdncustomdomain
schema: 2.0.0
---

# Get-AzCdnCustomDomain

## SYNOPSIS
Gets an existing custom domain within an endpoint.

## SYNTAX

### List1 (Default)
```
Get-AzCdnCustomDomain -EndpointName <String> -ProfileName <String> -ResourceGroupName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### Get1
```
Get-AzCdnCustomDomain -EndpointName <String> -Name <String> -ProfileName <String> -ResourceGroupName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### GetViaIdentity1
```
Get-AzCdnCustomDomain -InputObject <ICdnIdentity> [-DefaultProfile <PSObject>]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
Gets an existing custom domain within an endpoint.

## EXAMPLES

### Example 1: List AzureCDN custom domains under the AzureCDN endpoint
```powershell
Get-AzCdnCustomDomain -ResourceGroupName testps-rg-da16jm -ProfileName cdn001 -EndpointName endptest001
```

```output
Name            ResourceGroupName
----            -----------------
customdomain001 testps-rg-da16jm
customdomain002 testps-rg-da16jm
```

List AzureCDN custom domains under the AzureCDN endpoint

### Example 2: Get an AzureCDN custom domain under the AzureCDN endpoint
```powershell
Get-AzCdnCustomDomain -ResourceGroupName testps-rg-da16jm -ProfileName cdn001 -EndpointName endptest001 -Name customdomain001
```

```output
Name            ResourceGroupName
----            -----------------
customdomain001 testps-rg-da16jm
```

Get an AzureCDN custom domain under the AzureCDN endpoint

### Example 3: Get an AzureCDN custom domain under the AzureCDN endpoint via identity
```powershell
New-AzCdnCustomDomain -ResourceGroupName testps-rg-da16jm -ProfileName cdn001 -EndpointName endptest001  -Name customdomain010 -HostName 'testcm.dev.cdn.azure.cn' | Get-AzCdnCustomDomain
```

```output
Name            ResourceGroupName
----            -----------------
customdomain010 testps-rg-da16jm
```

Get an AzureCDN custom domain under the AzureCDN endpoint via identity

## PARAMETERS

### -DefaultProfile
The DefaultProfile parameter is not functional.
Use the SubscriptionId parameter when available if executing the cmdlet against a different subscription.

```yaml
Type: System.Management.Automation.PSObject
Parameter Sets: (All)
Aliases: AzureRMContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EndpointName
Name of the endpoint under the profile which is unique globally.

```yaml
Type: System.String
Parameter Sets: List1, Get1
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Identity Parameter
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Cdn.Models.ICdnIdentity
Parameter Sets: GetViaIdentity1
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
Name of the custom domain within an endpoint.

```yaml
Type: System.String
Parameter Sets: Get1
Aliases: CustomDomainName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProfileName
Name of the CDN profile which is unique within the resource group.

```yaml
Type: System.String
Parameter Sets: List1, Get1
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: System.Management.Automation.ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Name of the Resource group within the Azure subscription.

```yaml
Type: System.String
Parameter Sets: List1, Get1
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscriptionId
Azure Subscription ID.

```yaml
Type: System.String[]
Parameter Sets: List1, Get1
Aliases:

Required: False
Position: Named
Default value: (Get-AzContext).Subscription.Id
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.PowerShell.Cmdlets.Cdn.Models.ICdnIdentity

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.Cdn.Models.Api20240201.ICustomDomain

## NOTES

## RELATED LINKS
