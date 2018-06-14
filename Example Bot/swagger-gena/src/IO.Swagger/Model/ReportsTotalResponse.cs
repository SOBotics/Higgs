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
    /// ReportsTotalResponse
    /// </summary>
    [DataContract]
    public partial class ReportsTotalResponse :  IEquatable<ReportsTotalResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsTotalResponse" /> class.
        /// </summary>
        /// <param name="BotId">BotId.</param>
        /// <param name="DashboardName">DashboardName.</param>
        /// <param name="Count">Count.</param>
        public ReportsTotalResponse(int? BotId = default(int?), string DashboardName = default(string), int? Count = default(int?))
        {
            this.BotId = BotId;
            this.DashboardName = DashboardName;
            this.Count = Count;
        }
        
        /// <summary>
        /// Gets or Sets BotId
        /// </summary>
        [DataMember(Name="botId", EmitDefaultValue=false)]
        public int? BotId { get; set; }

        /// <summary>
        /// Gets or Sets DashboardName
        /// </summary>
        [DataMember(Name="dashboardName", EmitDefaultValue=false)]
        public string DashboardName { get; set; }

        /// <summary>
        /// Gets or Sets Count
        /// </summary>
        [DataMember(Name="count", EmitDefaultValue=false)]
        public int? Count { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ReportsTotalResponse {\n");
            sb.Append("  BotId: ").Append(BotId).Append("\n");
            sb.Append("  DashboardName: ").Append(DashboardName).Append("\n");
            sb.Append("  Count: ").Append(Count).Append("\n");
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
            return this.Equals(input as ReportsTotalResponse);
        }

        /// <summary>
        /// Returns true if ReportsTotalResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of ReportsTotalResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReportsTotalResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.BotId == input.BotId ||
                    (this.BotId != null &&
                    this.BotId.Equals(input.BotId))
                ) && 
                (
                    this.DashboardName == input.DashboardName ||
                    (this.DashboardName != null &&
                    this.DashboardName.Equals(input.DashboardName))
                ) && 
                (
                    this.Count == input.Count ||
                    (this.Count != null &&
                    this.Count.Equals(input.Count))
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
                if (this.BotId != null)
                    hashCode = hashCode * 59 + this.BotId.GetHashCode();
                if (this.DashboardName != null)
                    hashCode = hashCode * 59 + this.DashboardName.GetHashCode();
                if (this.Count != null)
                    hashCode = hashCode * 59 + this.Count.GetHashCode();
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
