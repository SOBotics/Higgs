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
    /// RegisterPostRequest
    /// </summary>
    [DataContract]
    public partial class RegisterPostRequest :  IEquatable<RegisterPostRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPostRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected RegisterPostRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPostRequest" /> class.
        /// </summary>
        /// <param name="Title">Title of the report (for example, the question title) (required).</param>
        /// <param name="ContentUrl">Link to detected content (required).</param>
        /// <param name="ContentSite">The site on which the content was detected.</param>
        /// <param name="ContentType">The type of content (question, answer, comment, etc).</param>
        /// <param name="ContentId">The Id of the content.</param>
        /// <param name="DetectionScore">The score of the report, between 0 and 1.</param>
        /// <param name="Content">The content of the report.</param>
        /// <param name="ContentFragments">Additional content fragments.</param>
        /// <param name="AuthorName">The name of the author who created the content.</param>
        /// <param name="AuthorReputation">The author&#39;s reputation.</param>
        /// <param name="ContentCreationDate">The UTC date the content was created.</param>
        /// <param name="DetectedDate">The UTC date the content was detected.</param>
        /// <param name="Reasons">A list of reasons the report was detected.</param>
        /// <param name="AllowedFeedback">A list of feedback types.</param>
        /// <param name="Attributes">Any custom attributes to be associated with the report.</param>
        /// <param name="RequiredFeedback">RequiredFeedback.</param>
        /// <param name="RequiredFeedbackConflicted">RequiredFeedbackConflicted.</param>
        public RegisterPostRequest(string Title = default(string), string ContentUrl = default(string), string ContentSite = default(string), string ContentType = default(string), int? ContentId = default(int?), double? DetectionScore = default(double?), string Content = default(string), List<RegisterPostContentFragment> ContentFragments = default(List<RegisterPostContentFragment>), string AuthorName = default(string), int? AuthorReputation = default(int?), DateTime? ContentCreationDate = default(DateTime?), DateTime? DetectedDate = default(DateTime?), List<RegisterPostReason> Reasons = default(List<RegisterPostReason>), List<string> AllowedFeedback = default(List<string>), List<RegisterPostAttribute> Attributes = default(List<RegisterPostAttribute>), int? RequiredFeedback = default(int?), int? RequiredFeedbackConflicted = default(int?))
        {
            // to ensure "Title" is required (not null)
            if (Title == null)
            {
                throw new InvalidDataException("Title is a required property for RegisterPostRequest and cannot be null");
            }
            else
            {
                this.Title = Title;
            }
            // to ensure "ContentUrl" is required (not null)
            if (ContentUrl == null)
            {
                throw new InvalidDataException("ContentUrl is a required property for RegisterPostRequest and cannot be null");
            }
            else
            {
                this.ContentUrl = ContentUrl;
            }
            this.ContentSite = ContentSite;
            this.ContentType = ContentType;
            this.ContentId = ContentId;
            this.DetectionScore = DetectionScore;
            this.Content = Content;
            this.ContentFragments = ContentFragments;
            this.AuthorName = AuthorName;
            this.AuthorReputation = AuthorReputation;
            this.ContentCreationDate = ContentCreationDate;
            this.DetectedDate = DetectedDate;
            this.Reasons = Reasons;
            this.AllowedFeedback = AllowedFeedback;
            this.Attributes = Attributes;
            this.RequiredFeedback = RequiredFeedback;
            this.RequiredFeedbackConflicted = RequiredFeedbackConflicted;
        }
        
        /// <summary>
        /// Title of the report (for example, the question title)
        /// </summary>
        /// <value>Title of the report (for example, the question title)</value>
        [DataMember(Name="title", EmitDefaultValue=false)]
        public string Title { get; set; }

        /// <summary>
        /// Link to detected content
        /// </summary>
        /// <value>Link to detected content</value>
        [DataMember(Name="contentUrl", EmitDefaultValue=false)]
        public string ContentUrl { get; set; }

        /// <summary>
        /// The site on which the content was detected
        /// </summary>
        /// <value>The site on which the content was detected</value>
        [DataMember(Name="contentSite", EmitDefaultValue=false)]
        public string ContentSite { get; set; }

        /// <summary>
        /// The type of content (question, answer, comment, etc)
        /// </summary>
        /// <value>The type of content (question, answer, comment, etc)</value>
        [DataMember(Name="contentType", EmitDefaultValue=false)]
        public string ContentType { get; set; }

        /// <summary>
        /// The Id of the content
        /// </summary>
        /// <value>The Id of the content</value>
        [DataMember(Name="contentId", EmitDefaultValue=false)]
        public int? ContentId { get; set; }

        /// <summary>
        /// The score of the report, between 0 and 1
        /// </summary>
        /// <value>The score of the report, between 0 and 1</value>
        [DataMember(Name="detectionScore", EmitDefaultValue=false)]
        public double? DetectionScore { get; set; }

        /// <summary>
        /// The content of the report
        /// </summary>
        /// <value>The content of the report</value>
        [DataMember(Name="content", EmitDefaultValue=false)]
        public string Content { get; set; }

        /// <summary>
        /// Additional content fragments
        /// </summary>
        /// <value>Additional content fragments</value>
        [DataMember(Name="contentFragments", EmitDefaultValue=false)]
        public List<RegisterPostContentFragment> ContentFragments { get; set; }

        /// <summary>
        /// The name of the author who created the content
        /// </summary>
        /// <value>The name of the author who created the content</value>
        [DataMember(Name="authorName", EmitDefaultValue=false)]
        public string AuthorName { get; set; }

        /// <summary>
        /// The author&#39;s reputation
        /// </summary>
        /// <value>The author&#39;s reputation</value>
        [DataMember(Name="authorReputation", EmitDefaultValue=false)]
        public int? AuthorReputation { get; set; }

        /// <summary>
        /// The UTC date the content was created
        /// </summary>
        /// <value>The UTC date the content was created</value>
        [DataMember(Name="contentCreationDate", EmitDefaultValue=false)]
        public DateTime? ContentCreationDate { get; set; }

        /// <summary>
        /// The UTC date the content was detected
        /// </summary>
        /// <value>The UTC date the content was detected</value>
        [DataMember(Name="detectedDate", EmitDefaultValue=false)]
        public DateTime? DetectedDate { get; set; }

        /// <summary>
        /// A list of reasons the report was detected
        /// </summary>
        /// <value>A list of reasons the report was detected</value>
        [DataMember(Name="reasons", EmitDefaultValue=false)]
        public List<RegisterPostReason> Reasons { get; set; }

        /// <summary>
        /// A list of feedback types
        /// </summary>
        /// <value>A list of feedback types</value>
        [DataMember(Name="allowedFeedback", EmitDefaultValue=false)]
        public List<string> AllowedFeedback { get; set; }

        /// <summary>
        /// Any custom attributes to be associated with the report
        /// </summary>
        /// <value>Any custom attributes to be associated with the report</value>
        [DataMember(Name="attributes", EmitDefaultValue=false)]
        public List<RegisterPostAttribute> Attributes { get; set; }

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
            sb.Append("class RegisterPostRequest {\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  ContentUrl: ").Append(ContentUrl).Append("\n");
            sb.Append("  ContentSite: ").Append(ContentSite).Append("\n");
            sb.Append("  ContentType: ").Append(ContentType).Append("\n");
            sb.Append("  ContentId: ").Append(ContentId).Append("\n");
            sb.Append("  DetectionScore: ").Append(DetectionScore).Append("\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  ContentFragments: ").Append(ContentFragments).Append("\n");
            sb.Append("  AuthorName: ").Append(AuthorName).Append("\n");
            sb.Append("  AuthorReputation: ").Append(AuthorReputation).Append("\n");
            sb.Append("  ContentCreationDate: ").Append(ContentCreationDate).Append("\n");
            sb.Append("  DetectedDate: ").Append(DetectedDate).Append("\n");
            sb.Append("  Reasons: ").Append(Reasons).Append("\n");
            sb.Append("  AllowedFeedback: ").Append(AllowedFeedback).Append("\n");
            sb.Append("  Attributes: ").Append(Attributes).Append("\n");
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
            return this.Equals(input as RegisterPostRequest);
        }

        /// <summary>
        /// Returns true if RegisterPostRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of RegisterPostRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RegisterPostRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Title == input.Title ||
                    (this.Title != null &&
                    this.Title.Equals(input.Title))
                ) && 
                (
                    this.ContentUrl == input.ContentUrl ||
                    (this.ContentUrl != null &&
                    this.ContentUrl.Equals(input.ContentUrl))
                ) && 
                (
                    this.ContentSite == input.ContentSite ||
                    (this.ContentSite != null &&
                    this.ContentSite.Equals(input.ContentSite))
                ) && 
                (
                    this.ContentType == input.ContentType ||
                    (this.ContentType != null &&
                    this.ContentType.Equals(input.ContentType))
                ) && 
                (
                    this.ContentId == input.ContentId ||
                    (this.ContentId != null &&
                    this.ContentId.Equals(input.ContentId))
                ) && 
                (
                    this.DetectionScore == input.DetectionScore ||
                    (this.DetectionScore != null &&
                    this.DetectionScore.Equals(input.DetectionScore))
                ) && 
                (
                    this.Content == input.Content ||
                    (this.Content != null &&
                    this.Content.Equals(input.Content))
                ) && 
                (
                    this.ContentFragments == input.ContentFragments ||
                    this.ContentFragments != null &&
                    this.ContentFragments.SequenceEqual(input.ContentFragments)
                ) && 
                (
                    this.AuthorName == input.AuthorName ||
                    (this.AuthorName != null &&
                    this.AuthorName.Equals(input.AuthorName))
                ) && 
                (
                    this.AuthorReputation == input.AuthorReputation ||
                    (this.AuthorReputation != null &&
                    this.AuthorReputation.Equals(input.AuthorReputation))
                ) && 
                (
                    this.ContentCreationDate == input.ContentCreationDate ||
                    (this.ContentCreationDate != null &&
                    this.ContentCreationDate.Equals(input.ContentCreationDate))
                ) && 
                (
                    this.DetectedDate == input.DetectedDate ||
                    (this.DetectedDate != null &&
                    this.DetectedDate.Equals(input.DetectedDate))
                ) && 
                (
                    this.Reasons == input.Reasons ||
                    this.Reasons != null &&
                    this.Reasons.SequenceEqual(input.Reasons)
                ) && 
                (
                    this.AllowedFeedback == input.AllowedFeedback ||
                    this.AllowedFeedback != null &&
                    this.AllowedFeedback.SequenceEqual(input.AllowedFeedback)
                ) && 
                (
                    this.Attributes == input.Attributes ||
                    this.Attributes != null &&
                    this.Attributes.SequenceEqual(input.Attributes)
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
                if (this.Title != null)
                    hashCode = hashCode * 59 + this.Title.GetHashCode();
                if (this.ContentUrl != null)
                    hashCode = hashCode * 59 + this.ContentUrl.GetHashCode();
                if (this.ContentSite != null)
                    hashCode = hashCode * 59 + this.ContentSite.GetHashCode();
                if (this.ContentType != null)
                    hashCode = hashCode * 59 + this.ContentType.GetHashCode();
                if (this.ContentId != null)
                    hashCode = hashCode * 59 + this.ContentId.GetHashCode();
                if (this.DetectionScore != null)
                    hashCode = hashCode * 59 + this.DetectionScore.GetHashCode();
                if (this.Content != null)
                    hashCode = hashCode * 59 + this.Content.GetHashCode();
                if (this.ContentFragments != null)
                    hashCode = hashCode * 59 + this.ContentFragments.GetHashCode();
                if (this.AuthorName != null)
                    hashCode = hashCode * 59 + this.AuthorName.GetHashCode();
                if (this.AuthorReputation != null)
                    hashCode = hashCode * 59 + this.AuthorReputation.GetHashCode();
                if (this.ContentCreationDate != null)
                    hashCode = hashCode * 59 + this.ContentCreationDate.GetHashCode();
                if (this.DetectedDate != null)
                    hashCode = hashCode * 59 + this.DetectedDate.GetHashCode();
                if (this.Reasons != null)
                    hashCode = hashCode * 59 + this.Reasons.GetHashCode();
                if (this.AllowedFeedback != null)
                    hashCode = hashCode * 59 + this.AllowedFeedback.GetHashCode();
                if (this.Attributes != null)
                    hashCode = hashCode * 59 + this.Attributes.GetHashCode();
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
