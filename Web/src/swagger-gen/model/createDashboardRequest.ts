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
import { CreateBotRequestExceptions } from './createBotRequestExceptions';
import { CreateBotRequestFeedback } from './createBotRequestFeedback';


export interface CreateDashboardRequest {
    ownerAccountId?: number;
    secret?: string;
    /**
     * Name of the bot
     */
    botName: string;
    /**
     * Name of the dashboard
     */
    dashboardName: string;
    /**
     * Description of the bot
     */
    description: string;
    homepage?: string;
    logoUrl?: string;
    favIcon?: string;
    tabTitle?: string;
    feedbacks?: Array<CreateBotRequestFeedback>;
    conflictExceptions?: Array<CreateBotRequestExceptions>;
    requiredFeedback: number;
    requiredFeedbackConflicted: number;
}
