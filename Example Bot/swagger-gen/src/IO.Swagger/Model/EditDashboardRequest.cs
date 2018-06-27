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
    /// EditDashboardRequest
    /// </summary>
    [DataContract]
    public partial class EditDashboardRequest :  IEquatable<EditDashboardRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditDashboardRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected EditDashboardRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EditDashboardRequest" /> class.
        /// </summary>
        /// <param name="DashboardId">DashboardId (required).</param>
        /// <param name="OwnerAccountId">OwnerAccountId.</param>
        /// <param name="Secret">Secret.</param>
        /// <param name="BotName">Name of the bot (required).</param>
        /// <param name="DashboardName">Name of the dashboard (required).</param>
        /// <param name="Description">Description of the bot (required).</param>
        /// <param name="Homepage">Homepage.</param>
        /// <param name="LogoUrl">LogoUrl.</param>
        /// <param name="FavIcon">FavIcon.</param>
        /// <param name="TabTitle">TabTitle.</param>
        /// <param name="Feedbacks">Feedbacks.</param>
        /// <param name="ConflictExceptions">ConflictExceptions.</param>
        /// <param name="RequiredFeedback">RequiredFeedback (required).</param>
        /// <param name="RequiredFeedbackConflicted">RequiredFeedbackConflicted (required).</param>
        public EditDashboardRequest(int? DashboardId = default(int?), int? OwnerAccountId = default(int?), string Secret = default(string), string BotName = default(string), string DashboardName = default(string), string Description = default(string), string Homepage = default(string), string LogoUrl = default(string), string FavIcon = default(string), string TabTitle = default(string), List<CreateBotRequestFeedback> Feedbacks = default(List<CreateBotRequestFeedback>), List<CreateBotRequestExceptions> ConflictExceptions = default(List<CreateBotRequestExceptions>), int? RequiredFeedback = default(int?), int? RequiredFeedbackConflicted = default(int?))
        {
            // to ensure "DashboardId" is required (not null)
            if (DashboardId == null)
            {
                throw new InvalidDataException("DashboardId is a required property for EditDashboardRequest and cannot be null");
            }
            else
            {
                this.DashboardId = DashboardId;
            }
            // to ensure "BotName" is required (not null)
            if (BotName == null)
            {
                throw new InvalidDataException("BotName is a required property for EditDashboardRequest and cannot be null");
            }
            else
            {
                this.BotName = BotName;
            }
            // to ensure "DashboardName" is required (not null)
            if (DashboardName == null)
            {
                throw new InvalidDataException("DashboardName is a required property for EditDashboardRequest and cannot be null");
            }
            else
            {
                this.DashboardName = DashboardName;
            }
            // to ensure "Description" is required (not null)
            if (Description == null)
            {
                throw new InvalidDataException("Description is a required property for EditDashboardRequest and cannot be null");
            }
            else
            {
                this.Description = Description;
            }
            // to ensure "RequiredFeedback" is required (not null)
            if (RequiredFeedback == null)
            {
                throw new InvalidDataException("RequiredFeedback is a required property for EditDashboardRequest and cannot be null");
            }
            else
            {
                this.RequiredFeedback = RequiredFeedback;
            }
            // to ensure "RequiredFeedbackConflicted" is required (not null)
            if (RequiredFeedbackConflicted == null)
            {
                throw new InvalidDataException("RequiredFeedbackConflicted is a required property for EditDashboardRequest and cannot be null");
            }
            else
            {
                this.RequiredFeedbackConflicted = RequiredFeedbackConflicted;
            }
            this.OwnerAccountId = OwnerAccountId;
            this.Secret = Secret;
            this.Homepage = Homepage;
            this.LogoUrl = LogoUrl;
            this.FavIcon = FavIcon;
            this.TabTitle = TabTitle;
            this.Feedbacks = Feedbacks;
            this.ConflictExceptions = ConflictExceptions;
        }
        
        /// <summary>
        /// Gets or Sets DashboardId
        /// </summary>
        [DataMember(Name="dashboardId", EmitDefaultValue=false)]
        public int? DashboardId { get; set; }

        /// <summary>
        /// Gets or Sets OwnerAccountId
        /// </summary>
        [DataMember(Name="ownerAccountId", EmitDefaultValue=false)]
        public int? OwnerAccountId { get; set; }

        /// <summary>
        /// Gets or Sets Secret
        /// </summary>
        [DataMember(Name="secret", EmitDefaultValue=false)]
        public string Secret { get; set; }

        /// <summary>
        /// Name of the bot
        /// </summary>
        /// <value>Name of the bot</value>
        [DataMember(Name="botName", EmitDefaultValue=false)]
        public string BotName { get; set; }

        /// <summary>
        /// Name of the dashboard
        /// </summary>
        /// <value>Name of the dashboard</value>
        [DataMember(Name="dashboardName", EmitDefaultValue=false)]
        public string DashboardName { get; set; }

        /// <summary>
        /// Description of the bot
        /// </summary>
        /// <value>Description of the bot</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Homepage
        /// </summary>
        [DataMember(Name="homepage", EmitDefaultValue=false)]
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or Sets LogoUrl
        /// </summary>
        [DataMember(Name="logoUrl", EmitDefaultValue=false)]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or Sets FavIcon
        /// </summary>
        [DataMember(Name="favIcon", EmitDefaultValue=false)]
        public string FavIcon { get; set; }

        /// <summary>
        /// Gets or Sets TabTitle
        /// </summary>
        [DataMember(Name="tabTitle", EmitDefaultValue=false)]
        public string TabTitle { get; set; }

        /// <summary>
        /// Gets or Sets Feedbacks
        /// </summary>
        [DataMember(Name="feedbacks", EmitDefaultValue=false)]
        public List<CreateBotRequestFeedback> Feedbacks { get; set; }

        /// <summary>
        /// Gets or Sets ConflictExceptions
        /// </summary>
        [DataMember(Name="conflictExceptions", EmitDefaultValue=false)]
        public List<CreateBotRequestExceptions> ConflictExceptions { get; set; }

        /// <summary>
        /// Gets or Sets RequiredFeedback
        /// </summary>
        [DataMember(Name="requiredFeedback", EmitDefaultValue=false)]
        public int? RequiredFeedback { get; set; }

        /// <summary>
        /// Gets or Sets RequiredFeedbackConflicted
        /// </summary>
        [DataMember(Name="requiredFeedbackConflicted", EmitDefaultValue=false)]
        public int? RequiredFeedbackConflicted { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EditDashboardRequest {\n");
            sb.Append("  DashboardId: ").Append(DashboardId).Append("\n");
            sb.Append("  OwnerAccountId: ").Append(OwnerAccountId).Append("\n");
            sb.Append("  Secret: ").Append(Secret).Append("\n");
            sb.Append("  BotName: ").Append(BotName).Append("\n");
            sb.Append("  DashboardName: ").Append(DashboardName).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Homepage: ").Append(Homepage).Append("\n");
            sb.Append("  LogoUrl: ").Append(LogoUrl).Append("\n");
            sb.Append("  FavIcon: ").Append(FavIcon).Append("\n");
            sb.Append("  TabTitle: ").Append(TabTitle).Append("\n");
            sb.Append("  Feedbacks: ").Append(Feedbacks).Append("\n");
            sb.Append("  ConflictExceptions: ").Append(ConflictExceptions).Append("\n");
            sb.Append("  RequiredFeedback: ").Append(RequiredFeedback).Append("\n");
            sb.Append("  RequiredFeedbackConflicted: ").Append(RequiredFeedbackConflicted).Append("\n");
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
            return this.Equals(input as EditDashboardRequest);
        }

        /// <summary>
        /// Returns true if EditDashboardRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of EditDashboardRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EditDashboardRequest input)
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
                    this.OwnerAccountId == input.OwnerAccountId ||
                    (this.OwnerAccountId != null &&
                    this.OwnerAccountId.Equals(input.OwnerAccountId))
                ) && 
                (
                    this.Secret == input.Secret ||
                    (this.Secret != null &&
                    this.Secret.Equals(input.Secret))
                ) && 
                (
                    this.BotName == input.BotName ||
                    (this.BotName != null &&
                    this.BotName.Equals(input.BotName))
                ) && 
                (
                    this.DashboardName == input.DashboardName ||
                    (this.DashboardName != null &&
                    this.DashboardName.Equals(input.DashboardName))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Homepage == input.Homepage ||
                    (this.Homepage != null &&
                    this.Homepage.Equals(input.Homepage))
                ) && 
                (
                    this.LogoUrl == input.LogoUrl ||
                    (this.LogoUrl != null &&
                    this.LogoUrl.Equals(input.LogoUrl))
                ) && 
                (
                    this.FavIcon == input.FavIcon ||
                    (this.FavIcon != null &&
                    this.FavIcon.Equals(input.FavIcon))
                ) && 
                (
                    this.TabTitle == input.TabTitle ||
                    (this.TabTitle != null &&
                    this.TabTitle.Equals(input.TabTitle))
                ) && 
                (
                    this.Feedbacks == input.Feedbacks ||
                    this.Feedbacks != null &&
                    this.Feedbacks.SequenceEqual(input.Feedbacks)
                ) && 
                (
                    this.ConflictExceptions == input.ConflictExceptions ||
                    this.ConflictExceptions != null &&
                    this.ConflictExceptions.SequenceEqual(input.ConflictExceptions)
                ) && 
                (
                    this.RequiredFeedback == input.RequiredFeedback ||
                    (this.RequiredFeedback != null &&
                    this.RequiredFeedback.Equals(input.RequiredFeedback))
                ) && 
                (
                    this.RequiredFeedbackConflicted == input.RequiredFeedbackConflicted ||
                    (this.RequiredFeedbackConflicted != null &&
                    this.RequiredFeedbackConflicted.Equals(input.RequiredFeedbackConflicted))
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
                if (this.OwnerAccountId != null)
                    hashCode = hashCode * 59 + this.OwnerAccountId.GetHashCode();
                if (this.Secret != null)
                    hashCode = hashCode * 59 + this.Secret.GetHashCode();
                if (this.BotName != null)
                    hashCode = hashCode * 59 + this.BotName.GetHashCode();
                if (this.DashboardName != null)
                    hashCode = hashCode * 59 + this.DashboardName.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.Homepage != null)
                    hashCode = hashCode * 59 + this.Homepage.GetHashCode();
                if (this.LogoUrl != null)
                    hashCode = hashCode * 59 + this.LogoUrl.GetHashCode();
                if (this.FavIcon != null)
                    hashCode = hashCode * 59 + this.FavIcon.GetHashCode();
                if (this.TabTitle != null)
                    hashCode = hashCode * 59 + this.TabTitle.GetHashCode();
                if (this.Feedbacks != null)
                    hashCode = hashCode * 59 + this.Feedbacks.GetHashCode();
                if (this.ConflictExceptions != null)
                    hashCode = hashCode * 59 + this.ConflictExceptions.GetHashCode();
                if (this.RequiredFeedback != null)
                    hashCode = hashCode * 59 + this.RequiredFeedback.GetHashCode();
                if (this.RequiredFeedbackConflicted != null)
                    hashCode = hashCode * 59 + this.RequiredFeedbackConflicted.GetHashCode();
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