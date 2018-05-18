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
    /// SendFeedbackRequest
    /// </summary>
    [DataContract]
    public partial class SendFeedbackRequest :  IEquatable<SendFeedbackRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendFeedbackRequest" /> class.
        /// </summary>
        /// <param name="ReportId">ReportId.</param>
        /// <param name="FeedbackId">FeedbackId.</param>
        public SendFeedbackRequest(int? ReportId = default(int?), int? FeedbackId = default(int?))
        {
            this.ReportId = ReportId;
            this.FeedbackId = FeedbackId;
        }
        
        /// <summary>
        /// Gets or Sets ReportId
        /// </summary>
        [DataMember(Name="reportId", EmitDefaultValue=false)]
        public int? ReportId { get; set; }

        /// <summary>
        /// Gets or Sets FeedbackId
        /// </summary>
        [DataMember(Name="feedbackId", EmitDefaultValue=false)]
        public int? FeedbackId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SendFeedbackRequest {\n");
            sb.Append("  ReportId: ").Append(ReportId).Append("\n");
            sb.Append("  FeedbackId: ").Append(FeedbackId).Append("\n");
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
            return this.Equals(input as SendFeedbackRequest);
        }

        /// <summary>
        /// Returns true if SendFeedbackRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of SendFeedbackRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SendFeedbackRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ReportId == input.ReportId ||
                    (this.ReportId != null &&
                    this.ReportId.Equals(input.ReportId))
                ) && 
                (
                    this.FeedbackId == input.FeedbackId ||
                    (this.FeedbackId != null &&
                    this.FeedbackId.Equals(input.FeedbackId))
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
                if (this.ReportId != null)
                    hashCode = hashCode * 59 + this.ReportId.GetHashCode();
                if (this.FeedbackId != null)
                    hashCode = hashCode * 59 + this.FeedbackId.GetHashCode();
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