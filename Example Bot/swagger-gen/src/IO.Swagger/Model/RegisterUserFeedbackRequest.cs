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
    /// RegisterUserFeedbackRequest
    /// </summary>
    [DataContract]
    public partial class RegisterUserFeedbackRequest :  IEquatable<RegisterUserFeedbackRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserFeedbackRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected RegisterUserFeedbackRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserFeedbackRequest" /> class.
        /// </summary>
        /// <param name="ReportId">ReportId (required).</param>
        /// <param name="UserId">UserId (required).</param>
        /// <param name="Feedback">Feedback (required).</param>
        public RegisterUserFeedbackRequest(int? ReportId = default(int?), int? UserId = default(int?), string Feedback = default(string))
        {
            // to ensure "ReportId" is required (not null)
            if (ReportId == null)
            {
                throw new InvalidDataException("ReportId is a required property for RegisterUserFeedbackRequest and cannot be null");
            }
            else
            {
                this.ReportId = ReportId;
            }
            // to ensure "UserId" is required (not null)
            if (UserId == null)
            {
                throw new InvalidDataException("UserId is a required property for RegisterUserFeedbackRequest and cannot be null");
            }
            else
            {
                this.UserId = UserId;
            }
            // to ensure "Feedback" is required (not null)
            if (Feedback == null)
            {
                throw new InvalidDataException("Feedback is a required property for RegisterUserFeedbackRequest and cannot be null");
            }
            else
            {
                this.Feedback = Feedback;
            }
        }
        
        /// <summary>
        /// Gets or Sets ReportId
        /// </summary>
        [DataMember(Name="reportId", EmitDefaultValue=false)]
        public int? ReportId { get; set; }

        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        [DataMember(Name="userId", EmitDefaultValue=false)]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or Sets Feedback
        /// </summary>
        [DataMember(Name="feedback", EmitDefaultValue=false)]
        public string Feedback { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RegisterUserFeedbackRequest {\n");
            sb.Append("  ReportId: ").Append(ReportId).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  Feedback: ").Append(Feedback).Append("\n");
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
            return this.Equals(input as RegisterUserFeedbackRequest);
        }

        /// <summary>
        /// Returns true if RegisterUserFeedbackRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of RegisterUserFeedbackRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RegisterUserFeedbackRequest input)
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
                    this.UserId == input.UserId ||
                    (this.UserId != null &&
                    this.UserId.Equals(input.UserId))
                ) && 
                (
                    this.Feedback == input.Feedback ||
                    (this.Feedback != null &&
                    this.Feedback.Equals(input.Feedback))
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
                if (this.UserId != null)
                    hashCode = hashCode * 59 + this.UserId.GetHashCode();
                if (this.Feedback != null)
                    hashCode = hashCode * 59 + this.Feedback.GetHashCode();
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