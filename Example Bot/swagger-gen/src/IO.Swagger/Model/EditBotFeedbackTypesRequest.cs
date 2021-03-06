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
    /// EditBotFeedbackTypesRequest
    /// </summary>
    [DataContract]
    public partial class EditBotFeedbackTypesRequest :  IEquatable<EditBotFeedbackTypesRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditBotFeedbackTypesRequest" /> class.
        /// </summary>
        /// <param name="BotId">BotId.</param>
        /// <param name="FeedbackTypes">FeedbackTypes.</param>
        public EditBotFeedbackTypesRequest(int? BotId = default(int?), List<ViewBotFeedbackTypesResponse> FeedbackTypes = default(List<ViewBotFeedbackTypesResponse>))
        {
            this.BotId = BotId;
            this.FeedbackTypes = FeedbackTypes;
        }
        
        /// <summary>
        /// Gets or Sets BotId
        /// </summary>
        [DataMember(Name="botId", EmitDefaultValue=false)]
        public int? BotId { get; set; }

        /// <summary>
        /// Gets or Sets FeedbackTypes
        /// </summary>
        [DataMember(Name="feedbackTypes", EmitDefaultValue=false)]
        public List<ViewBotFeedbackTypesResponse> FeedbackTypes { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EditBotFeedbackTypesRequest {\n");
            sb.Append("  BotId: ").Append(BotId).Append("\n");
            sb.Append("  FeedbackTypes: ").Append(FeedbackTypes).Append("\n");
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
            return this.Equals(input as EditBotFeedbackTypesRequest);
        }

        /// <summary>
        /// Returns true if EditBotFeedbackTypesRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of EditBotFeedbackTypesRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EditBotFeedbackTypesRequest input)
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
                    this.FeedbackTypes == input.FeedbackTypes ||
                    this.FeedbackTypes != null &&
                    this.FeedbackTypes.SequenceEqual(input.FeedbackTypes)
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
                if (this.FeedbackTypes != null)
                    hashCode = hashCode * 59 + this.FeedbackTypes.GetHashCode();
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
