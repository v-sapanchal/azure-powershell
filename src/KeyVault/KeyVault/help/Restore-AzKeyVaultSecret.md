---
external help file: Microsoft.Azure.PowerShell.Cmdlets.KeyVault.dll-Help.xml
Module Name: Az.KeyVault
ms.assetid: 70DB088D-4AF5-406B-8D66-118A0F766041
online version: https://learn.microsoft.com/powershell/module/az.keyvault/restore-azkeyvaultsecret
schema: 2.0.0
---

# Restore-AzKeyVaultSecret

## SYNOPSIS
Creates a secret in a key vault from a backed-up secret.

## SYNTAX

### ByVaultName (Default)
```
Restore-AzKeyVaultSecret [-VaultName] <String> [-InputFile] <String> [-DefaultProfile <IAzureContextContainer>]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### BySecretUri
```
Restore-AzKeyVaultSecret [-Id] <String> [-InputFile] <String> [-DefaultProfile <IAzureContextContainer>]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ByInputObject
```
Restore-AzKeyVaultSecret [-InputObject] <PSKeyVault> [-InputFile] <String>
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### ByParentResourceId
```
Restore-AzKeyVaultSecret [-ParentResourceId] <String> [-InputFile] <String>
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

## DESCRIPTION
The **Restore-AzKeyVaultSecret** cmdlet creates a secret in the specified key vault.
This secret is a replica of the backed-up secret in the input file and has the same name as the original secret.
If the key vault already has a secret by the same name, this cmdlet fails instead of overwriting the original secret.
If the backup contains multiple versions of a secret, all versions are restored.
The key vault that you restore the secret into can be different from the key vault that you backed up the secret from.
However, the key vault must use the same subscription and be in an Azure region in the same geography (for example, North America).
See the Microsoft Azure Trust Center (https://azure.microsoft.com/support/trust-center/) for the mapping of Azure regions to geographies.

## EXAMPLES

### Example 1: Restore a backed-up secret
```powershell
Restore-AzKeyVaultSecret -VaultName 'contoso' -InputFile "C:\Backup.blob"
```

```output
Vault Name   : contoso
Name         : secret1
Version      : 7128133570f84a71b48d7d0550deb74c
Id           : https://contoso.vault.azure.net:443/secrets/secret1/7128133570f84a71b48d7d0550deb74c
Enabled      : True
Expires      : 4/6/2018 3:59:43 PM
Not Before   :
Created      : 4/5/2018 11:46:28 PM
Updated      : 4/6/2018 11:30:17 PM
Content Type :
Tags         :
```

This command restores a secret, including all of its versions, from the backup file named Backup.blob into the key vault named contoso.

### Example 2: Restore a backed-up secret (using Uri)
```powershell
Restore-AzKeyVaultSecret -Id "https://contoso.vault.azure.net:443/secrets/" -InputFile "C:\Backup.blob"
```

```output
Vault Name   : contoso
Name         : secret1
Version      : 7128133570f84a71b48d7d0550deb74c
Id           : https://contoso.vault.azure.net:443/secrets/secret1/7128133570f84a71b48d7d0550deb74c
Enabled      : True
Expires      : 4/6/2018 3:59:43 PM
Not Before   :
Created      : 4/5/2018 11:46:28 PM
Updated      : 4/6/2018 11:30:17 PM
Content Type :
Tags         :
```

This command restores a secret, including all of its versions, from the backup file named Backup.blob into the key vault named contoso.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with azure

```yaml
Type: IAzureContextContainer
Parameter Sets: (All)
Aliases: AzContext, AzureRmContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The URI of the KeyVault Secret.
Please ensure it follows the format: `https://<vault-name>.vault.azure.net/secrets/<secret-name>/<version>`

```yaml
Type: String
Parameter Sets: BySecretUri
Aliases: SecretId

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputFile
Specifies the input file that contains the backup of the secret to restore.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
KeyVault object

```yaml
Type: PSKeyVault
Parameter Sets: ByInputObject
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ParentResourceId
KeyVault Resource Id

```yaml
Type: String
Parameter Sets: ByParentResourceId
Aliases: ResourceId

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -VaultName
Specifies the name of the key vault into which to restore the secret.

```yaml
Type: String
Parameter Sets: ByVaultName
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.Commands.KeyVault.Models.PSKeyVault

### System.String

## OUTPUTS

### Microsoft.Azure.Commands.KeyVault.Models.PSKeyVaultSecret

## NOTES

## RELATED LINKS

[Set-AzKeyVaultSecret](./Set-AzKeyVaultSecret.md)

[Backup-AzKeyVaultSecret](./Backup-AzKeyVaultSecret.md)

[Get-AzKeyVaultSecret](./Get-AzKeyVaultSecret.md)

[Remove-AzKeyVaultSecret](./Remove-AzKeyVaultSecret.md)
