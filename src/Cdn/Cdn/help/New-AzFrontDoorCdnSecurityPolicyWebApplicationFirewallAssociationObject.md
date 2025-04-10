---
external help file: Az.Cdn-help.xml
Module Name: Az.Cdn
online version: https://learn.microsoft.com/powershell/module/az.Cdn/new-AzFrontDoorCdnSecurityPolicyWebApplicationFirewallAssociationObject
schema: 2.0.0
---

# New-AzFrontDoorCdnSecurityPolicyWebApplicationFirewallAssociationObject

## SYNOPSIS
Create an in-memory object for SecurityPolicyWebApplicationFirewallAssociation.

## SYNTAX

```
New-AzFrontDoorCdnSecurityPolicyWebApplicationFirewallAssociationObject
 [-Domain <IActivatedResourceReference[]>] [-PatternsToMatch <String[]>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
Create an in-memory object for SecurityPolicyWebApplicationFirewallAssociation.

## EXAMPLES

### Example 1: Create an in-memory object for AzureFrontDoor SecurityPolicyWebApplicationFirewallAssociation
```powershell
$endpoint = Get-AzFrontDoorCdnEndpoint -ResourceGroupName testps-rg-da16jm -ProfileName fdp-v542q6 -EndpointName end001
New-AzFrontDoorCdnSecurityPolicyWebApplicationFirewallAssociationObject -PatternsToMatch @("/*") -Domain @(@{"Id"=$($endpoint.Id)})
```

```output
PatternsToMatch
---------------
{/*}
```

Create an in-memory object for AzureFrontDoor SecurityPolicyWebApplicationFirewallAssociation

## PARAMETERS

### -Domain
List of domains.
To construct, see NOTES section for DOMAIN properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Cdn.Models.Api20240201.IActivatedResourceReference[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PatternsToMatch
List of paths.

```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases:

Required: False
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.Cdn.Models.Api20240201.SecurityPolicyWebApplicationFirewallAssociation

## NOTES

## RELATED LINKS
