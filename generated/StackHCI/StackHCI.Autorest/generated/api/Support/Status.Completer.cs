// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.StackHCI.Support
{

    /// <summary>Status of the cluster agent.</summary>
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Azure.PowerShell.Cmdlets.StackHCI.Support.StatusTypeConverter))]
    public partial struct Status :
        System.Management.Automation.IArgumentCompleter
    {

        /// <summary>
        /// Implementations of this function are called by PowerShell to complete arguments.
        /// </summary>
        /// <param name="commandName">The name of the command that needs argument completion.</param>
        /// <param name="parameterName">The name of the parameter that needs argument completion.</param>
        /// <param name="wordToComplete">The (possibly empty) word being completed.</param>
        /// <param name="commandAst">The command ast in case it is needed for completion.</param>
        /// <param name="fakeBoundParameters">This parameter is similar to $PSBoundParameters, except that sometimes PowerShell cannot
        /// or will not attempt to evaluate an argument, in which case you may need to use commandAst.</param>
        /// <returns>
        /// A collection of completion results, most like with ResultType set to ParameterValue.
        /// </returns>
        public global::System.Collections.Generic.IEnumerable<global::System.Management.Automation.CompletionResult> CompleteArgument(global::System.String commandName, global::System.String parameterName, global::System.String wordToComplete, global::System.Management.Automation.Language.CommandAst commandAst, global::System.Collections.IDictionary fakeBoundParameters)
        {
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "NotYetRegistered".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'NotYetRegistered'", "NotYetRegistered", global::System.Management.Automation.CompletionResultType.ParameterValue, "NotYetRegistered");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "ConnectedRecently".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'ConnectedRecently'", "ConnectedRecently", global::System.Management.Automation.CompletionResultType.ParameterValue, "ConnectedRecently");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "NotConnectedRecently".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'NotConnectedRecently'", "NotConnectedRecently", global::System.Management.Automation.CompletionResultType.ParameterValue, "NotConnectedRecently");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "Disconnected".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'Disconnected'", "Disconnected", global::System.Management.Automation.CompletionResultType.ParameterValue, "Disconnected");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "Error".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'Error'", "Error", global::System.Management.Automation.CompletionResultType.ParameterValue, "Error");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "NotSpecified".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'NotSpecified'", "NotSpecified", global::System.Management.Automation.CompletionResultType.ParameterValue, "NotSpecified");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "ValidationInProgress".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'ValidationInProgress'", "ValidationInProgress", global::System.Management.Automation.CompletionResultType.ParameterValue, "ValidationInProgress");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "ValidationSuccess".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'ValidationSuccess'", "ValidationSuccess", global::System.Management.Automation.CompletionResultType.ParameterValue, "ValidationSuccess");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "ValidationFailed".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'ValidationFailed'", "ValidationFailed", global::System.Management.Automation.CompletionResultType.ParameterValue, "ValidationFailed");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "DeploymentInProgress".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'DeploymentInProgress'", "DeploymentInProgress", global::System.Management.Automation.CompletionResultType.ParameterValue, "DeploymentInProgress");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "DeploymentFailed".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'DeploymentFailed'", "DeploymentFailed", global::System.Management.Automation.CompletionResultType.ParameterValue, "DeploymentFailed");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "DeploymentSuccess".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'DeploymentSuccess'", "DeploymentSuccess", global::System.Management.Automation.CompletionResultType.ParameterValue, "DeploymentSuccess");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "Succeeded".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'Succeeded'", "Succeeded", global::System.Management.Automation.CompletionResultType.ParameterValue, "Succeeded");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "Failed".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'Failed'", "Failed", global::System.Management.Automation.CompletionResultType.ParameterValue, "Failed");
            }
            if (global::System.String.IsNullOrEmpty(wordToComplete) || "InProgress".StartsWith(wordToComplete, global::System.StringComparison.InvariantCultureIgnoreCase))
            {
                yield return new global::System.Management.Automation.CompletionResult("'InProgress'", "InProgress", global::System.Management.Automation.CompletionResultType.ParameterValue, "InProgress");
            }
        }
    }
}