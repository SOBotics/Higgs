/**
 * Higgs API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { RegisterPostAttribute } from './registerPostAttribute';
import { RegisterPostContentFragment } from './registerPostContentFragment';
import { RegisterPostReason } from './registerPostReason';


export interface RegisterPostRequest {
    /**
     * Title of the report (for example, the question title)
     */
    title: string;
    /**
     * Link to detected content
     */
    contentUrl: string;
    /**
     * The site on which the content was detected
     */
    contentSite?: string;
    /**
     * The type of content (question, answer, comment, etc)
     */
    contentType?: string;
    /**
     * The Id of the content
     */
    contentId?: number;
    /**
     * The score of the report, between 0 and 1
     */
    detectionScore?: number;
    /**
     * The content of the report
     */
    content?: string;
    /**
     * Additional content fragments
     */
    contentFragments?: Array<RegisterPostContentFragment>;
    /**
     * The name of the author who created the content
     */
    authorName?: string;
    /**
     * The author's reputation
     */
    authorReputation?: number;
    /**
     * The UTC date the content was created
     */
    contentCreationDate?: Date;
    /**
     * The UTC date the content was detected
     */
    detectedDate?: Date;
    /**
     * A list of reasons the report was detected
     */
    reasons?: Array<RegisterPostReason>;
    /**
     * A list of feedback types
     */
    allowedFeedback?: Array<string>;
    /**
     * Any custom attributes to be associated with the report
     */
    attributes?: Array<RegisterPostAttribute>;
    requiredFeedback?: number;
    requiredFeedbackConflicted?: number;
}
