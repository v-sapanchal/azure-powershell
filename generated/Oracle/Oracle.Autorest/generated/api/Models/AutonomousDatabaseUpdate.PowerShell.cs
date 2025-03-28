// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models
{
    using Microsoft.Azure.PowerShell.Cmdlets.Oracle.Runtime.PowerShell;

    /// <summary>The type used for update operations of the AutonomousDatabase.</summary>
    [System.ComponentModel.TypeConverter(typeof(AutonomousDatabaseUpdateTypeConverter))]
    public partial class AutonomousDatabaseUpdate
    {

        /// <summary>
        /// <c>AfterDeserializeDictionary</c> will be called after the deserialization has finished, allowing customization of the
        /// object before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="content">The global::System.Collections.IDictionary content that should be used.</param>

        partial void AfterDeserializeDictionary(global::System.Collections.IDictionary content);

        /// <summary>
        /// <c>AfterDeserializePSObject</c> will be called after the deserialization has finished, allowing customization of the object
        /// before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="content">The global::System.Management.Automation.PSObject content that should be used.</param>

        partial void AfterDeserializePSObject(global::System.Management.Automation.PSObject content);

        /// <summary>
        /// <c>BeforeDeserializeDictionary</c> will be called before the deserialization has commenced, allowing complete customization
        /// of the object before it is deserialized.
        /// If you wish to disable the default deserialization entirely, return <c>true</c> in the <paramref name="returnNow" /> output
        /// parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="content">The global::System.Collections.IDictionary content that should be used.</param>
        /// <param name="returnNow">Determines if the rest of the serialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeDeserializeDictionary(global::System.Collections.IDictionary content, ref bool returnNow);

        /// <summary>
        /// <c>BeforeDeserializePSObject</c> will be called before the deserialization has commenced, allowing complete customization
        /// of the object before it is deserialized.
        /// If you wish to disable the default deserialization entirely, return <c>true</c> in the <paramref name="returnNow" /> output
        /// parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="content">The global::System.Management.Automation.PSObject content that should be used.</param>
        /// <param name="returnNow">Determines if the rest of the serialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeDeserializePSObject(global::System.Management.Automation.PSObject content, ref bool returnNow);

        /// <summary>
        /// <c>OverrideToString</c> will be called if it is implemented. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="stringResult">/// instance serialized to a string, normally it is a Json</param>
        /// <param name="returnNow">/// set returnNow to true if you provide a customized OverrideToString function</param>

        partial void OverrideToString(ref string stringResult, ref bool returnNow);

        /// <summary>
        /// Deserializes a <see cref="global::System.Collections.IDictionary" /> into a new instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdate"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Collections.IDictionary content that should be used.</param>
        internal AutonomousDatabaseUpdate(global::System.Collections.IDictionary content)
        {
            bool returnNow = false;
            BeforeDeserializeDictionary(content, ref returnNow);
            if (returnNow)
            {
                return;
            }
            // actually deserialize
            if (content.Contains("Property"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Property = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateProperties) content.GetValueForProperty("Property",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Property, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdatePropertiesTypeConverter.ConvertFrom);
            }
            if (content.Contains("Tag"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Tag = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateTags) content.GetValueForProperty("Tag",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Tag, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdateTagsTypeConverter.ConvertFrom);
            }
            if (content.Contains("LicenseModel"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LicenseModel = (string) content.GetValueForProperty("LicenseModel",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LicenseModel, global::System.Convert.ToString);
            }
            if (content.Contains("ScheduledOperation"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperation = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IScheduledOperationsTypeUpdate) content.GetValueForProperty("ScheduledOperation",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperation, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ScheduledOperationsTypeUpdateTypeConverter.ConvertFrom);
            }
            if (content.Contains("LongTermBackupSchedule"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupSchedule = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ILongTermBackUpScheduleDetails) content.GetValueForProperty("LongTermBackupSchedule",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupSchedule, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.LongTermBackUpScheduleDetailsTypeConverter.ConvertFrom);
            }
            if (content.Contains("AdminPassword"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AdminPassword = (System.Security.SecureString) content.GetValueForProperty("AdminPassword",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AdminPassword, (object ss) => (System.Security.SecureString)ss);
            }
            if (content.Contains("AutonomousMaintenanceScheduleType"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AutonomousMaintenanceScheduleType = (string) content.GetValueForProperty("AutonomousMaintenanceScheduleType",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AutonomousMaintenanceScheduleType, global::System.Convert.ToString);
            }
            if (content.Contains("ComputeCount"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ComputeCount = (float?) content.GetValueForProperty("ComputeCount",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ComputeCount, (__y)=> (float) global::System.Convert.ChangeType(__y, typeof(float)));
            }
            if (content.Contains("CpuCoreCount"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CpuCoreCount = (int?) content.GetValueForProperty("CpuCoreCount",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CpuCoreCount, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("CustomerContact"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CustomerContact = (System.Collections.Generic.List<Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ICustomerContact>) content.GetValueForProperty("CustomerContact",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CustomerContact, __y => TypeConverterExtensions.SelectToList<Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ICustomerContact>(__y, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.CustomerContactTypeConverter.ConvertFrom));
            }
            if (content.Contains("DataStorageSizeInTb"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInTb = (int?) content.GetValueForProperty("DataStorageSizeInTb",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInTb, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("DataStorageSizeInGb"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInGb = (int?) content.GetValueForProperty("DataStorageSizeInGb",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInGb, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("DisplayName"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DisplayName = (string) content.GetValueForProperty("DisplayName",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DisplayName, global::System.Convert.ToString);
            }
            if (content.Contains("IsAutoScalingEnabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingEnabled = (bool?) content.GetValueForProperty("IsAutoScalingEnabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingEnabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("IsAutoScalingForStorageEnabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingForStorageEnabled = (bool?) content.GetValueForProperty("IsAutoScalingForStorageEnabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingForStorageEnabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("PeerDbId"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PeerDbId = (string) content.GetValueForProperty("PeerDbId",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PeerDbId, global::System.Convert.ToString);
            }
            if (content.Contains("IsLocalDataGuardEnabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsLocalDataGuardEnabled = (bool?) content.GetValueForProperty("IsLocalDataGuardEnabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsLocalDataGuardEnabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("IsMtlsConnectionRequired"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsMtlsConnectionRequired = (bool?) content.GetValueForProperty("IsMtlsConnectionRequired",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsMtlsConnectionRequired, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("DatabaseEdition"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DatabaseEdition = (string) content.GetValueForProperty("DatabaseEdition",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DatabaseEdition, global::System.Convert.ToString);
            }
            if (content.Contains("LocalAdgAutoFailoverMaxDataLossLimit"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LocalAdgAutoFailoverMaxDataLossLimit = (int?) content.GetValueForProperty("LocalAdgAutoFailoverMaxDataLossLimit",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LocalAdgAutoFailoverMaxDataLossLimit, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("OpenMode"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).OpenMode = (string) content.GetValueForProperty("OpenMode",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).OpenMode, global::System.Convert.ToString);
            }
            if (content.Contains("PermissionLevel"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PermissionLevel = (string) content.GetValueForProperty("PermissionLevel",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PermissionLevel, global::System.Convert.ToString);
            }
            if (content.Contains("Role"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Role = (string) content.GetValueForProperty("Role",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Role, global::System.Convert.ToString);
            }
            if (content.Contains("BackupRetentionPeriodInDay"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).BackupRetentionPeriodInDay = (int?) content.GetValueForProperty("BackupRetentionPeriodInDay",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).BackupRetentionPeriodInDay, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("WhitelistedIP"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).WhitelistedIP = (System.Collections.Generic.List<string>) content.GetValueForProperty("WhitelistedIP",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).WhitelistedIP, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("LongTermBackupScheduleRetentionPeriodInDay"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRetentionPeriodInDay = (int?) content.GetValueForProperty("LongTermBackupScheduleRetentionPeriodInDay",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRetentionPeriodInDay, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("ScheduledOperationDayOfWeek"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationDayOfWeek = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IDayOfWeekUpdate) content.GetValueForProperty("ScheduledOperationDayOfWeek",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationDayOfWeek, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.DayOfWeekUpdateTypeConverter.ConvertFrom);
            }
            if (content.Contains("ScheduledOperationScheduledStartTime"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStartTime = (string) content.GetValueForProperty("ScheduledOperationScheduledStartTime",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStartTime, global::System.Convert.ToString);
            }
            if (content.Contains("ScheduledOperationScheduledStopTime"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStopTime = (string) content.GetValueForProperty("ScheduledOperationScheduledStopTime",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStopTime, global::System.Convert.ToString);
            }
            if (content.Contains("DayOfWeekName"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DayOfWeekName = (string) content.GetValueForProperty("DayOfWeekName",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DayOfWeekName, global::System.Convert.ToString);
            }
            if (content.Contains("LongTermBackupScheduleRepeatCadence"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRepeatCadence = (string) content.GetValueForProperty("LongTermBackupScheduleRepeatCadence",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRepeatCadence, global::System.Convert.ToString);
            }
            if (content.Contains("LongTermBackupScheduleTimeOfBackup"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleTimeOfBackup = (global::System.DateTime?) content.GetValueForProperty("LongTermBackupScheduleTimeOfBackup",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleTimeOfBackup, (v) => v is global::System.DateTime _v ? _v : global::System.Xml.XmlConvert.ToDateTime( v.ToString() , global::System.Xml.XmlDateTimeSerializationMode.Unspecified));
            }
            if (content.Contains("LongTermBackupScheduleIsDisabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleIsDisabled = (bool?) content.GetValueForProperty("LongTermBackupScheduleIsDisabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleIsDisabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            AfterDeserializeDictionary(content);
        }

        /// <summary>
        /// Deserializes a <see cref="global::System.Management.Automation.PSObject" /> into a new instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdate"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Management.Automation.PSObject content that should be used.</param>
        internal AutonomousDatabaseUpdate(global::System.Management.Automation.PSObject content)
        {
            bool returnNow = false;
            BeforeDeserializePSObject(content, ref returnNow);
            if (returnNow)
            {
                return;
            }
            // actually deserialize
            if (content.Contains("Property"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Property = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateProperties) content.GetValueForProperty("Property",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Property, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdatePropertiesTypeConverter.ConvertFrom);
            }
            if (content.Contains("Tag"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Tag = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateTags) content.GetValueForProperty("Tag",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Tag, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdateTagsTypeConverter.ConvertFrom);
            }
            if (content.Contains("LicenseModel"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LicenseModel = (string) content.GetValueForProperty("LicenseModel",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LicenseModel, global::System.Convert.ToString);
            }
            if (content.Contains("ScheduledOperation"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperation = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IScheduledOperationsTypeUpdate) content.GetValueForProperty("ScheduledOperation",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperation, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ScheduledOperationsTypeUpdateTypeConverter.ConvertFrom);
            }
            if (content.Contains("LongTermBackupSchedule"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupSchedule = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ILongTermBackUpScheduleDetails) content.GetValueForProperty("LongTermBackupSchedule",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupSchedule, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.LongTermBackUpScheduleDetailsTypeConverter.ConvertFrom);
            }
            if (content.Contains("AdminPassword"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AdminPassword = (System.Security.SecureString) content.GetValueForProperty("AdminPassword",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AdminPassword, (object ss) => (System.Security.SecureString)ss);
            }
            if (content.Contains("AutonomousMaintenanceScheduleType"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AutonomousMaintenanceScheduleType = (string) content.GetValueForProperty("AutonomousMaintenanceScheduleType",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).AutonomousMaintenanceScheduleType, global::System.Convert.ToString);
            }
            if (content.Contains("ComputeCount"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ComputeCount = (float?) content.GetValueForProperty("ComputeCount",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ComputeCount, (__y)=> (float) global::System.Convert.ChangeType(__y, typeof(float)));
            }
            if (content.Contains("CpuCoreCount"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CpuCoreCount = (int?) content.GetValueForProperty("CpuCoreCount",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CpuCoreCount, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("CustomerContact"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CustomerContact = (System.Collections.Generic.List<Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ICustomerContact>) content.GetValueForProperty("CustomerContact",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).CustomerContact, __y => TypeConverterExtensions.SelectToList<Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.ICustomerContact>(__y, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.CustomerContactTypeConverter.ConvertFrom));
            }
            if (content.Contains("DataStorageSizeInTb"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInTb = (int?) content.GetValueForProperty("DataStorageSizeInTb",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInTb, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("DataStorageSizeInGb"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInGb = (int?) content.GetValueForProperty("DataStorageSizeInGb",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DataStorageSizeInGb, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("DisplayName"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DisplayName = (string) content.GetValueForProperty("DisplayName",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DisplayName, global::System.Convert.ToString);
            }
            if (content.Contains("IsAutoScalingEnabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingEnabled = (bool?) content.GetValueForProperty("IsAutoScalingEnabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingEnabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("IsAutoScalingForStorageEnabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingForStorageEnabled = (bool?) content.GetValueForProperty("IsAutoScalingForStorageEnabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsAutoScalingForStorageEnabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("PeerDbId"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PeerDbId = (string) content.GetValueForProperty("PeerDbId",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PeerDbId, global::System.Convert.ToString);
            }
            if (content.Contains("IsLocalDataGuardEnabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsLocalDataGuardEnabled = (bool?) content.GetValueForProperty("IsLocalDataGuardEnabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsLocalDataGuardEnabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("IsMtlsConnectionRequired"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsMtlsConnectionRequired = (bool?) content.GetValueForProperty("IsMtlsConnectionRequired",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).IsMtlsConnectionRequired, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("DatabaseEdition"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DatabaseEdition = (string) content.GetValueForProperty("DatabaseEdition",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DatabaseEdition, global::System.Convert.ToString);
            }
            if (content.Contains("LocalAdgAutoFailoverMaxDataLossLimit"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LocalAdgAutoFailoverMaxDataLossLimit = (int?) content.GetValueForProperty("LocalAdgAutoFailoverMaxDataLossLimit",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LocalAdgAutoFailoverMaxDataLossLimit, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("OpenMode"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).OpenMode = (string) content.GetValueForProperty("OpenMode",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).OpenMode, global::System.Convert.ToString);
            }
            if (content.Contains("PermissionLevel"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PermissionLevel = (string) content.GetValueForProperty("PermissionLevel",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).PermissionLevel, global::System.Convert.ToString);
            }
            if (content.Contains("Role"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Role = (string) content.GetValueForProperty("Role",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).Role, global::System.Convert.ToString);
            }
            if (content.Contains("BackupRetentionPeriodInDay"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).BackupRetentionPeriodInDay = (int?) content.GetValueForProperty("BackupRetentionPeriodInDay",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).BackupRetentionPeriodInDay, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("WhitelistedIP"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).WhitelistedIP = (System.Collections.Generic.List<string>) content.GetValueForProperty("WhitelistedIP",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).WhitelistedIP, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("LongTermBackupScheduleRetentionPeriodInDay"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRetentionPeriodInDay = (int?) content.GetValueForProperty("LongTermBackupScheduleRetentionPeriodInDay",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRetentionPeriodInDay, (__y)=> (int) global::System.Convert.ChangeType(__y, typeof(int)));
            }
            if (content.Contains("ScheduledOperationDayOfWeek"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationDayOfWeek = (Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IDayOfWeekUpdate) content.GetValueForProperty("ScheduledOperationDayOfWeek",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationDayOfWeek, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.DayOfWeekUpdateTypeConverter.ConvertFrom);
            }
            if (content.Contains("ScheduledOperationScheduledStartTime"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStartTime = (string) content.GetValueForProperty("ScheduledOperationScheduledStartTime",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStartTime, global::System.Convert.ToString);
            }
            if (content.Contains("ScheduledOperationScheduledStopTime"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStopTime = (string) content.GetValueForProperty("ScheduledOperationScheduledStopTime",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).ScheduledOperationScheduledStopTime, global::System.Convert.ToString);
            }
            if (content.Contains("DayOfWeekName"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DayOfWeekName = (string) content.GetValueForProperty("DayOfWeekName",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).DayOfWeekName, global::System.Convert.ToString);
            }
            if (content.Contains("LongTermBackupScheduleRepeatCadence"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRepeatCadence = (string) content.GetValueForProperty("LongTermBackupScheduleRepeatCadence",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleRepeatCadence, global::System.Convert.ToString);
            }
            if (content.Contains("LongTermBackupScheduleTimeOfBackup"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleTimeOfBackup = (global::System.DateTime?) content.GetValueForProperty("LongTermBackupScheduleTimeOfBackup",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleTimeOfBackup, (v) => v is global::System.DateTime _v ? _v : global::System.Xml.XmlConvert.ToDateTime( v.ToString() , global::System.Xml.XmlDateTimeSerializationMode.Unspecified));
            }
            if (content.Contains("LongTermBackupScheduleIsDisabled"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleIsDisabled = (bool?) content.GetValueForProperty("LongTermBackupScheduleIsDisabled",((Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdateInternal)this).LongTermBackupScheduleIsDisabled, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            AfterDeserializePSObject(content);
        }

        /// <summary>
        /// Deserializes a <see cref="global::System.Collections.IDictionary" /> into an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdate"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Collections.IDictionary content that should be used.</param>
        /// <returns>
        /// an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdate" />.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdate DeserializeFromDictionary(global::System.Collections.IDictionary content)
        {
            return new AutonomousDatabaseUpdate(content);
        }

        /// <summary>
        /// Deserializes a <see cref="global::System.Management.Automation.PSObject" /> into an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.AutonomousDatabaseUpdate"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Management.Automation.PSObject content that should be used.</param>
        /// <returns>
        /// an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdate" />.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdate DeserializeFromPSObject(global::System.Management.Automation.PSObject content)
        {
            return new AutonomousDatabaseUpdate(content);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AutonomousDatabaseUpdate" />, deserializing the content from a json string.
        /// </summary>
        /// <param name="jsonText">a string containing a JSON serialized instance of this model.</param>
        /// <returns>an instance of the <see cref="AutonomousDatabaseUpdate" /> model class.</returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.Oracle.Models.IAutonomousDatabaseUpdate FromJsonString(string jsonText) => FromJson(Microsoft.Azure.PowerShell.Cmdlets.Oracle.Runtime.Json.JsonNode.Parse(jsonText));

        /// <summary>Serializes this instance to a json string.</summary>

        /// <returns>a <see cref="System.String" /> containing this model serialized to JSON text.</returns>
        public string ToJsonString() => ToJson(null, Microsoft.Azure.PowerShell.Cmdlets.Oracle.Runtime.SerializationMode.IncludeAll)?.ToString();

        public override string ToString()
        {
            var returnNow = false;
            var result = global::System.String.Empty;
            OverrideToString(ref result, ref returnNow);
            if (returnNow)
            {
                return result;
            }
            return ToJsonString();
        }
    }
    /// The type used for update operations of the AutonomousDatabase.
    [System.ComponentModel.TypeConverter(typeof(AutonomousDatabaseUpdateTypeConverter))]
    public partial interface IAutonomousDatabaseUpdate

    {

    }
}