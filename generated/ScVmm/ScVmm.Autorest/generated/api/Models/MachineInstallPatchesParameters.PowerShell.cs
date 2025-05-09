// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models
{
    using Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Runtime.PowerShell;

    /// <summary>Input for InstallPatches as directly received by the API</summary>
    [System.ComponentModel.TypeConverter(typeof(MachineInstallPatchesParametersTypeConverter))]
    public partial class MachineInstallPatchesParameters
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
        /// Deserializes a <see cref="global::System.Collections.IDictionary" /> into an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.MachineInstallPatchesParameters"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Collections.IDictionary content that should be used.</param>
        /// <returns>
        /// an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParameters" />.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParameters DeserializeFromDictionary(global::System.Collections.IDictionary content)
        {
            return new MachineInstallPatchesParameters(content);
        }

        /// <summary>
        /// Deserializes a <see cref="global::System.Management.Automation.PSObject" /> into an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.MachineInstallPatchesParameters"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Management.Automation.PSObject content that should be used.</param>
        /// <returns>
        /// an instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParameters" />.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParameters DeserializeFromPSObject(global::System.Management.Automation.PSObject content)
        {
            return new MachineInstallPatchesParameters(content);
        }

        /// <summary>
        /// Creates a new instance of <see cref="MachineInstallPatchesParameters" />, deserializing the content from a json string.
        /// </summary>
        /// <param name="jsonText">a string containing a JSON serialized instance of this model.</param>
        /// <returns>an instance of the <see cref="MachineInstallPatchesParameters" /> model class.</returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParameters FromJsonString(string jsonText) => FromJson(Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Runtime.Json.JsonNode.Parse(jsonText));

        /// <summary>
        /// Deserializes a <see cref="global::System.Collections.IDictionary" /> into a new instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.MachineInstallPatchesParameters"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Collections.IDictionary content that should be used.</param>
        internal MachineInstallPatchesParameters(global::System.Collections.IDictionary content)
        {
            bool returnNow = false;
            BeforeDeserializeDictionary(content, ref returnNow);
            if (returnNow)
            {
                return;
            }
            // actually deserialize
            if (content.Contains("WindowsParameter"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowsParameter = (Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IWindowsParameters) content.GetValueForProperty("WindowsParameter",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowsParameter, Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.WindowsParametersTypeConverter.ConvertFrom);
            }
            if (content.Contains("LinuxParameter"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameter = (Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.ILinuxParameters) content.GetValueForProperty("LinuxParameter",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameter, Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.LinuxParametersTypeConverter.ConvertFrom);
            }
            if (content.Contains("MaximumDuration"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).MaximumDuration = (string) content.GetValueForProperty("MaximumDuration",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).MaximumDuration, global::System.Convert.ToString);
            }
            if (content.Contains("RebootSetting"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).RebootSetting = (string) content.GetValueForProperty("RebootSetting",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).RebootSetting, global::System.Convert.ToString);
            }
            if (content.Contains("WindowParameterClassificationsToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterClassificationsToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("WindowParameterClassificationsToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterClassificationsToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("WindowParameterKbNumbersToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("WindowParameterKbNumbersToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("WindowParameterKbNumbersToExclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToExclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("WindowParameterKbNumbersToExclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToExclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("WindowParameterExcludeKbsRequiringReboot"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterExcludeKbsRequiringReboot = (bool?) content.GetValueForProperty("WindowParameterExcludeKbsRequiringReboot",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterExcludeKbsRequiringReboot, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("WindowParameterMaxPatchPublishDate"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterMaxPatchPublishDate = (global::System.DateTime?) content.GetValueForProperty("WindowParameterMaxPatchPublishDate",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterMaxPatchPublishDate, (v) => v is global::System.DateTime _v ? _v : global::System.Xml.XmlConvert.ToDateTime( v.ToString() , global::System.Xml.XmlDateTimeSerializationMode.Unspecified));
            }
            if (content.Contains("LinuxParameterClassificationsToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterClassificationsToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("LinuxParameterClassificationsToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterClassificationsToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("LinuxParameterPackageNameMasksToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("LinuxParameterPackageNameMasksToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("LinuxParameterPackageNameMasksToExclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToExclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("LinuxParameterPackageNameMasksToExclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToExclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            AfterDeserializeDictionary(content);
        }

        /// <summary>
        /// Deserializes a <see cref="global::System.Management.Automation.PSObject" /> into a new instance of <see cref="Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.MachineInstallPatchesParameters"
        /// />.
        /// </summary>
        /// <param name="content">The global::System.Management.Automation.PSObject content that should be used.</param>
        internal MachineInstallPatchesParameters(global::System.Management.Automation.PSObject content)
        {
            bool returnNow = false;
            BeforeDeserializePSObject(content, ref returnNow);
            if (returnNow)
            {
                return;
            }
            // actually deserialize
            if (content.Contains("WindowsParameter"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowsParameter = (Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IWindowsParameters) content.GetValueForProperty("WindowsParameter",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowsParameter, Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.WindowsParametersTypeConverter.ConvertFrom);
            }
            if (content.Contains("LinuxParameter"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameter = (Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.ILinuxParameters) content.GetValueForProperty("LinuxParameter",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameter, Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.LinuxParametersTypeConverter.ConvertFrom);
            }
            if (content.Contains("MaximumDuration"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).MaximumDuration = (string) content.GetValueForProperty("MaximumDuration",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).MaximumDuration, global::System.Convert.ToString);
            }
            if (content.Contains("RebootSetting"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).RebootSetting = (string) content.GetValueForProperty("RebootSetting",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).RebootSetting, global::System.Convert.ToString);
            }
            if (content.Contains("WindowParameterClassificationsToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterClassificationsToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("WindowParameterClassificationsToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterClassificationsToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("WindowParameterKbNumbersToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("WindowParameterKbNumbersToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("WindowParameterKbNumbersToExclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToExclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("WindowParameterKbNumbersToExclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterKbNumbersToExclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("WindowParameterExcludeKbsRequiringReboot"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterExcludeKbsRequiringReboot = (bool?) content.GetValueForProperty("WindowParameterExcludeKbsRequiringReboot",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterExcludeKbsRequiringReboot, (__y)=> (bool) global::System.Convert.ChangeType(__y, typeof(bool)));
            }
            if (content.Contains("WindowParameterMaxPatchPublishDate"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterMaxPatchPublishDate = (global::System.DateTime?) content.GetValueForProperty("WindowParameterMaxPatchPublishDate",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).WindowParameterMaxPatchPublishDate, (v) => v is global::System.DateTime _v ? _v : global::System.Xml.XmlConvert.ToDateTime( v.ToString() , global::System.Xml.XmlDateTimeSerializationMode.Unspecified));
            }
            if (content.Contains("LinuxParameterClassificationsToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterClassificationsToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("LinuxParameterClassificationsToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterClassificationsToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("LinuxParameterPackageNameMasksToInclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToInclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("LinuxParameterPackageNameMasksToInclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToInclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            if (content.Contains("LinuxParameterPackageNameMasksToExclude"))
            {
                ((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToExclude = (System.Collections.Generic.List<string>) content.GetValueForProperty("LinuxParameterPackageNameMasksToExclude",((Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Models.IMachineInstallPatchesParametersInternal)this).LinuxParameterPackageNameMasksToExclude, __y => TypeConverterExtensions.SelectToList<string>(__y, global::System.Convert.ToString));
            }
            AfterDeserializePSObject(content);
        }

        /// <summary>Serializes this instance to a json string.</summary>

        /// <returns>a <see cref="System.String" /> containing this model serialized to JSON text.</returns>
        public string ToJsonString() => ToJson(null, Microsoft.Azure.PowerShell.Cmdlets.ScVmm.Runtime.SerializationMode.IncludeAll)?.ToString();

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
    /// Input for InstallPatches as directly received by the API
    [System.ComponentModel.TypeConverter(typeof(MachineInstallPatchesParametersTypeConverter))]
    public partial interface IMachineInstallPatchesParameters

    {

    }
}