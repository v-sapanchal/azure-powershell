// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support
{

    /// <summary>An ability the user has to perform an action on the project as a developer.</summary>
    public partial struct ProjectAbilityAsDeveloper :
        System.IEquatable<ProjectAbilityAsDeveloper>
    {
        /// <summary>User can customize their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper CustomizeDevBoxes = @"CustomizeDevBoxes";

        /// <summary>User can delete their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper DeleteDevBoxes = @"DeleteDevBoxes";

        /// <summary>User can delete environments.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper DeleteEnvironments = @"DeleteEnvironments";

        /// <summary>User can delay and skip actions on their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ManageDevBoxActions = @"ManageDevBoxActions";

        /// <summary>User can delay and skip actions on environments.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ManageEnvironmentActions = @"ManageEnvironmentActions";

        /// <summary>User can read actions on their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ReadDevBoxActions = @"ReadDevBoxActions";

        /// <summary>User can read their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ReadDevBoxes = @"ReadDevBoxes";

        /// <summary>User can read actions on environments.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ReadEnvironmentActions = @"ReadEnvironmentActions";

        /// <summary>User can read outputs on environments.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ReadEnvironmentOutputs = @"ReadEnvironmentOutputs";

        /// <summary>User can read environments.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ReadEnvironments = @"ReadEnvironments";

        /// <summary>User can read remote connections on their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper ReadRemoteConnections = @"ReadRemoteConnections";

        /// <summary>User can start their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper StartDevBoxes = @"StartDevBoxes";

        /// <summary>User can stop their own dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper StopDevBoxes = @"StopDevBoxes";

        /// <summary>User can create dev boxes.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper WriteDevBoxes = @"WriteDevBoxes";

        /// <summary>User can create new environments or replace and update existing ones.</summary>
        public static Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper WriteEnvironments = @"WriteEnvironments";

        /// <summary>the value for an instance of the <see cref="ProjectAbilityAsDeveloper" /> Enum.</summary>
        private string _value { get; set; }

        /// <summary>Conversion from arbitrary object to ProjectAbilityAsDeveloper</summary>
        /// <param name="value">the value to convert to an instance of <see cref="ProjectAbilityAsDeveloper" />.</param>
        internal static object CreateFrom(object value)
        {
            return new ProjectAbilityAsDeveloper(global::System.Convert.ToString(value));
        }

        /// <summary>Compares values of enum type ProjectAbilityAsDeveloper</summary>
        /// <param name="e">the value to compare against this instance.</param>
        /// <returns><c>true</c> if the two instances are equal to the same value</returns>
        public bool Equals(Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper e)
        {
            return _value.Equals(e._value);
        }

        /// <summary>Compares values of enum type ProjectAbilityAsDeveloper (override for Object)</summary>
        /// <param name="obj">the value to compare against this instance.</param>
        /// <returns><c>true</c> if the two instances are equal to the same value</returns>
        public override bool Equals(object obj)
        {
            return obj is ProjectAbilityAsDeveloper && Equals((ProjectAbilityAsDeveloper)obj);
        }

        /// <summary>Returns hashCode for enum ProjectAbilityAsDeveloper</summary>
        /// <returns>The hashCode of the value</returns>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        /// <summary>Creates an instance of the <see cref="ProjectAbilityAsDeveloper"/> Enum class.</summary>
        /// <param name="underlyingValue">the value to create an instance for.</param>
        private ProjectAbilityAsDeveloper(string underlyingValue)
        {
            this._value = underlyingValue;
        }

        /// <summary>Returns string representation for ProjectAbilityAsDeveloper</summary>
        /// <returns>A string for this value.</returns>
        public override string ToString()
        {
            return this._value;
        }

        /// <summary>Implicit operator to convert string to ProjectAbilityAsDeveloper</summary>
        /// <param name="value">the value to convert to an instance of <see cref="ProjectAbilityAsDeveloper" />.</param>

        public static implicit operator ProjectAbilityAsDeveloper(string value)
        {
            return new ProjectAbilityAsDeveloper(value);
        }

        /// <summary>Implicit operator to convert ProjectAbilityAsDeveloper to string</summary>
        /// <param name="e">the value to convert to an instance of <see cref="ProjectAbilityAsDeveloper" />.</param>

        public static implicit operator string(Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper e)
        {
            return e._value;
        }

        /// <summary>Overriding != operator for enum ProjectAbilityAsDeveloper</summary>
        /// <param name="e1">the value to compare against <paramref name="e2" /></param>
        /// <param name="e2">the value to compare against <paramref name="e1" /></param>
        /// <returns><c>true</c> if the two instances are not equal to the same value</returns>
        public static bool operator !=(Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper e1, Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper e2)
        {
            return !e2.Equals(e1);
        }

        /// <summary>Overriding == operator for enum ProjectAbilityAsDeveloper</summary>
        /// <param name="e1">the value to compare against <paramref name="e2" /></param>
        /// <param name="e2">the value to compare against <paramref name="e1" /></param>
        /// <returns><c>true</c> if the two instances are equal to the same value</returns>
        public static bool operator ==(Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper e1, Microsoft.Azure.PowerShell.Cmdlets.DevCenterdata.Support.ProjectAbilityAsDeveloper e2)
        {
            return e2.Equals(e1);
        }
    }
}