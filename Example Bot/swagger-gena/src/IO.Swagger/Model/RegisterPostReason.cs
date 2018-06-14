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
    /// RegisterPostReason
    /// </summary>
    [DataContract]
    public partial class RegisterPostReason :  IEquatable<RegisterPostReason>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPostReason" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected RegisterPostReason() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPostReason" /> class.
        /// </summary>
        /// <param name="ReasonName">The name of the reason (required).</param>
        /// <param name="Confidence">Confidence of the reason, between 0 and 1.</param>
        /// <param name="Tripped">Whether or not this reason tripped the report threshold.</param>
        public RegisterPostReason(string ReasonName = default(string), double? Confidence = default(double?), bool? Tripped = default(bool?))
        {
            // to ensure "ReasonName" is required (not null)
            if (ReasonName == null)
            {
                throw new InvalidDataException("ReasonName is a required property for RegisterPostReason and cannot be null");
            }
            else
            {
                this.ReasonName = ReasonName;
            }
            this.Confidence = Confidence;
            this.Tripped = Tripped;
        }
        
        /// <summary>
        /// The name of the reason
        /// </summary>
        /// <value>The name of the reason</value>
        [DataMember(Name="reasonName", EmitDefaultValue=false)]
        public string ReasonName { get; set; }

        /// <summary>
        /// Confidence of the reason, between 0 and 1
        /// </summary>
        /// <value>Confidence of the reason, between 0 and 1</value>
        [DataMember(Name="confidence", EmitDefaultValue=false)]
        public double? Confidence { get; set; }

        /// <summary>
        /// Whether or not this reason tripped the report threshold
        /// </summary>
        /// <value>Whether or not this reason tripped the report threshold</value>
        [DataMember(Name="tripped", EmitDefaultValue=false)]
        public bool? Tripped { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RegisterPostReason {\n");
            sb.Append("  ReasonName: ").Append(ReasonName).Append("\n");
            sb.Append("  Confidence: ").Append(Confidence).Append("\n");
            sb.Append("  Tripped: ").Append(Tripped).Append("\n");
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
            return this.Equals(input as RegisterPostReason);
        }

        /// <summary>
        /// Returns true if RegisterPostReason instances are equal
        /// </summary>
        /// <param name="input">Instance of RegisterPostReason to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RegisterPostReason input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ReasonName == input.ReasonName ||
                    (this.ReasonName != null &&
                    this.ReasonName.Equals(input.ReasonName))
                ) && 
                (
                    this.Confidence == input.Confidence ||
                    (this.Confidence != null &&
                    this.Confidence.Equals(input.Confidence))
                ) && 
                (
                    this.Tripped == input.Tripped ||
                    (this.Tripped != null &&
                    this.Tripped.Equals(input.Tripped))
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
                if (this.ReasonName != null)
                    hashCode = hashCode * 59 + this.ReasonName.GetHashCode();
                if (this.Confidence != null)
                    hashCode = hashCode * 59 + this.Confidence.GetHashCode();
                if (this.Tripped != null)
                    hashCode = hashCode * 59 + this.Tripped.GetHashCode();
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
