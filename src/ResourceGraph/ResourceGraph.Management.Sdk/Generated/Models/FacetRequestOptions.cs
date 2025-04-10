// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.ResourceGraph.Models
{
    using System.Linq;

    /// <summary>
    /// The options for facet evaluation
    /// </summary>
    public partial class FacetRequestOptions
    {
        /// <summary>
        /// Initializes a new instance of the FacetRequestOptions class.
        /// </summary>
        public FacetRequestOptions()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the FacetRequestOptions class.
        /// </summary>

        /// <param name="sortBy">The column name or query expression to sort on. Defaults to count if not
        /// present.
        /// </param>

        /// <param name="sortOrder">The sorting order by the selected column (count by default).
        /// Possible values include: &#39;asc&#39;, &#39;desc&#39;</param>

        /// <param name="filter">Specifies the filter condition for the &#39;where&#39; clause which will be run on
        /// main query&#39;s result, just before the actual faceting.
        /// </param>

        /// <param name="top">The maximum number of facet rows that should be returned.
        /// </param>
        public FacetRequestOptions(string sortBy = default(string), FacetSortOrder? sortOrder = default(FacetSortOrder?), string filter = default(string), int? top = default(int?))

        {
            this.SortBy = sortBy;
            this.SortOrder = sortOrder;
            this.Filter = filter;
            this.Top = top;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets the column name or query expression to sort on. Defaults to
        /// count if not present.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "sortBy")]
        public string SortBy {get; set; }

        /// <summary>
        /// Gets or sets the sorting order by the selected column (count by default). Possible values include: &#39;asc&#39;, &#39;desc&#39;
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "sortOrder")]
        public FacetSortOrder? SortOrder {get; set; }

        /// <summary>
        /// Gets or sets specifies the filter condition for the &#39;where&#39; clause which
        /// will be run on main query&#39;s result, just before the actual faceting.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "filter")]
        public string Filter {get; set; }

        /// <summary>
        /// Gets or sets the maximum number of facet rows that should be returned.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "$top")]
        public int? Top {get; set; }
        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {



            if (this.Top != null)
            {
                if (this.Top > 1000)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.InclusiveMaximum, "Top", 1000);
                }
                if (this.Top < 1)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.InclusiveMinimum, "Top", 1);
                }
            }
        }
    }
}