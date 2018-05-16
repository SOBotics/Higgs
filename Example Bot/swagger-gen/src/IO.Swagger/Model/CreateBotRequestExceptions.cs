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
    /// CreateBotRequestExceptions
    /// </summary>
    [DataContract]
    public partial class CreateBotRequestExceptions :  IEquatable<CreateBotRequestExceptions>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBotRequestExceptions" /> class.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <param name="IsConfict">IsConfict.</param>
        /// <param name="RequiresAdmin">RequiresAdmin.</param>
        /// <param name="BotResponseConflictFeedbacks">BotResponseConflictFeedbacks.</param>
        public CreateBotRequestExceptions(int? Id = default(int?), bool? IsConfict = default(bool?), bool? RequiresAdmin = default(bool?), List<int?> BotResponseConflictFeedbacks = default(List<int?>))
        {
            this.Id = Id;
            this.IsConfict = IsConfict;
            this.RequiresAdmin = RequiresAdmin;
            this.BotResponseConflictFeedbacks = BotResponseConflictFeedbacks;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets IsConfict
        /// </summary>
        [DataMember(Name="isConfict", EmitDefaultValue=false)]
        public bool? IsConfict { get; set; }

        /// <summary>
        /// Gets or Sets RequiresAdmin
        /// </summary>
        [DataMember(Name="requiresAdmin", EmitDefaultValue=false)]
        public bool? RequiresAdmin { get; set; }

        /// <summary>
        /// Gets or Sets BotResponseConflictFeedbacks
        /// </summary>
        [DataMember(Name="botResponseConflictFeedbacks", EmitDefaultValue=false)]
        public List<int?> BotResponseConflictFeedbacks { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateBotRequestExceptions {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IsConfict: ").Append(IsConfict).Append("\n");
            sb.Append("  RequiresAdmin: ").Append(RequiresAdmin).Append("\n");
            sb.Append("  BotResponseConflictFeedbacks: ").Append(BotResponseConflictFeedbacks).Append("\n");
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
            return this.Equals(input as CreateBotRequestExceptions);
        }

        /// <summary>
        /// Returns true if CreateBotRequestExceptions instances are equal
        /// </summary>
        /// <param name="input">Instance of CreateBotRequestExceptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateBotRequestExceptions input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.IsConfict == input.IsConfict ||
                    (this.IsConfict != null &&
                    this.IsConfict.Equals(input.IsConfict))
                ) && 
                (
                    this.RequiresAdmin == input.RequiresAdmin ||
                    (this.RequiresAdmin != null &&
                    this.RequiresAdmin.Equals(input.RequiresAdmin))
                ) && 
                (
                    this.BotResponseConflictFeedbacks == input.BotResponseConflictFeedbacks ||
                    this.BotResponseConflictFeedbacks != null &&
                    this.BotResponseConflictFeedbacks.SequenceEqual(input.BotResponseConflictFeedbacks)
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
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.IsConfict != null)
                    hashCode = hashCode * 59 + this.IsConfict.GetHashCode();
                if (this.RequiresAdmin != null)
                    hashCode = hashCode * 59 + this.RequiresAdmin.GetHashCode();
                if (this.BotResponseConflictFeedbacks != null)
                    hashCode = hashCode * 59 + this.BotResponseConflictFeedbacks.GetHashCode();
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
