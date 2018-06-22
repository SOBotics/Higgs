/* 
 * Higgs API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// ReviewerDashboardResponse
    /// </summary>
    [DataContract]
    public partial class ReviewerDashboardResponse :  IEquatable<ReviewerDashboardResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerDashboardResponse" /> class.
        /// </summary>
        /// <param name="DashboardId">DashboardId.</param>
        /// <param name="DashboardLogo">DashboardLogo.</param>
        /// <param name="DashboardName">DashboardName.</param>
        /// <param name="DashboardDescription">DashboardDescription.</param>
        public ReviewerDashboardResponse(int? DashboardId = default(int?), string DashboardLogo = default(string), string DashboardName = default(string), string DashboardDescription = default(string))
        {
            this.DashboardId = DashboardId;
            this.DashboardLogo = DashboardLogo;
            this.DashboardName = DashboardName;
            this.DashboardDescription = DashboardDescription;
        }
        
        /// <summary>
        /// Gets or Sets DashboardId
        /// </summary>
        [DataMember(Name="dashboardId", EmitDefaultValue=false)]
        public int? DashboardId { get; set; }

        /// <summary>
        /// Gets or Sets DashboardLogo
        /// </summary>
        [DataMember(Name="dashboardLogo", EmitDefaultValue=false)]
        public string DashboardLogo { get; set; }

        /// <summary>
        /// Gets or Sets DashboardName
        /// </summary>
        [DataMember(Name="dashboardName", EmitDefaultValue=false)]
        public string DashboardName { get; set; }

        /// <summary>
        /// Gets or Sets DashboardDescription
        /// </summary>
        [DataMember(Name="dashboardDescription", EmitDefaultValue=false)]
        public string DashboardDescription { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReviewerDashboardResponse {\n");
            sb.Append("  DashboardId: ").Append(DashboardId).Append("\n");
            sb.Append("  DashboardLogo: ").Append(DashboardLogo).Append("\n");
            sb.Append("  DashboardName: ").Append(DashboardName).Append("\n");
            sb.Append("  DashboardDescription: ").Append(DashboardDescription).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ReviewerDashboardResponse);
        }

        /// <summary>
        /// Returns true if ReviewerDashboardResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of ReviewerDashboardResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReviewerDashboardResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DashboardId == input.DashboardId ||
                    (this.DashboardId != null &&
                    this.DashboardId.Equals(input.DashboardId))
                ) && 
                (
                    this.DashboardLogo == input.DashboardLogo ||
                    (this.DashboardLogo != null &&
                    this.DashboardLogo.Equals(input.DashboardLogo))
                ) && 
                (
                    this.DashboardName == input.DashboardName ||
                    (this.DashboardName != null &&
                    this.DashboardName.Equals(input.DashboardName))
                ) && 
                (
                    this.DashboardDescription == input.DashboardDescription ||
                    (this.DashboardDescription != null &&
                    this.DashboardDescription.Equals(input.DashboardDescription))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.DashboardId != null)
                    hashCode = hashCode * 59 + this.DashboardId.GetHashCode();
                if (this.DashboardLogo != null)
                    hashCode = hashCode * 59 + this.DashboardLogo.GetHashCode();
                if (this.DashboardName != null)
                    hashCode = hashCode * 59 + this.DashboardName.GetHashCode();
                if (this.DashboardDescription != null)
                    hashCode = hashCode * 59 + this.DashboardDescription.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
