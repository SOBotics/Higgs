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
import { ReportAllowedFeedbackResponse } from './reportAllowedFeedbackResponse';
import { ReportContentFragmentResponse } from './reportContentFragmentResponse';
import { ReportFeedback } from './reportFeedback';
import { ReportReasonResponse } from './reportReasonResponse';


export interface ReportResponse {
    title?: string;
    contentUrl?: string;
    contentSite?: string;
    contentType?: string;
    contentId?: number;
    detectionScore?: number;
    contentFragments?: Array<ReportContentFragmentResponse>;
    authorName?: string;
    authorReputation?: number;
    contentCreationDate?: Date;
    detectedDate?: Date;
    reasons?: Array<ReportReasonResponse>;
    allowedFeedback?: Array<ReportAllowedFeedbackResponse>;
    feedback?: Array<ReportFeedback>;
}
