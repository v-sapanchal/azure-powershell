// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.ApiManagement.Models
{
    using System.Linq;

    /// <summary>
    /// User update parameters.
    /// </summary>
    [Microsoft.Rest.Serialization.JsonTransformation]
    public partial class UserUpdateParameters
    {
        /// <summary>
        /// Initializes a new instance of the UserUpdateParameters class.
        /// </summary>
        public UserUpdateParameters()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UserUpdateParameters class.
        /// </summary>

        /// <param name="state">Account state. Specifies whether the user is active or not. Blocked users
        /// are unable to sign into the developer portal or call any APIs of subscribed
        /// products. Default state is Active.
        /// Possible values include: &#39;active&#39;, &#39;blocked&#39;, &#39;pending&#39;, &#39;deleted&#39;</param>

        /// <param name="note">Optional note about a user set by the administrator.
        /// </param>

        /// <param name="identities">Collection of user identities.
        /// </param>

        /// <param name="email">Email address. Must not be empty and must be unique within the service
        /// instance.
        /// </param>

        /// <param name="password">User Password.
        /// </param>

        /// <param name="firstName">First name.
        /// </param>

        /// <param name="lastName">Last name.
        /// </param>
        public UserUpdateParameters(string state = default(string), string note = default(string), System.Collections.Generic.IList<UserIdentityContract> identities = default(System.Collections.Generic.IList<UserIdentityContract>), string email = default(string), string password = default(string), string firstName = default(string), string lastName = default(string))

        {
            this.State = state;
            this.Note = note;
            this.Identities = identities;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets account state. Specifies whether the user is active or not.
        /// Blocked users are unable to sign into the developer portal or call any APIs
        /// of subscribed products. Default state is Active. Possible values include: &#39;active&#39;, &#39;blocked&#39;, &#39;pending&#39;, &#39;deleted&#39;
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.state")]
        public string State {get; set; }

        /// <summary>
        /// Gets or sets optional note about a user set by the administrator.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.note")]
        public string Note {get; set; }

        /// <summary>
        /// Gets or sets collection of user identities.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.identities")]
        public System.Collections.Generic.IList<UserIdentityContract> Identities {get; set; }

        /// <summary>
        /// Gets or sets email address. Must not be empty and must be unique within the
        /// service instance.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.email")]
        public string Email {get; set; }

        /// <summary>
        /// Gets or sets user Password.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.password")]
        public string Password {get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.firstName")]
        public string FirstName {get; set; }

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties.lastName")]
        public string LastName {get; set; }
        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {



            if (this.Email != null)
            {
                if (this.Email.Length > 254)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MaxLength, "Email", 254);
                }
                if (this.Email.Length < 1)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MinLength, "Email", 1);
                }
            }

            if (this.FirstName != null)
            {
                if (this.FirstName.Length > 100)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MaxLength, "FirstName", 100);
                }
                if (this.FirstName.Length < 1)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MinLength, "FirstName", 1);
                }
            }
            if (this.LastName != null)
            {
                if (this.LastName.Length > 100)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MaxLength, "LastName", 100);
                }
                if (this.LastName.Length < 1)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MinLength, "LastName", 1);
                }
            }
        }
    }
}