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

import * as models from './models';

export interface RegisterPostRequest {
    /**
     * The ID of the bot, returned by the corresponding register call
     */
    botId: number;

    /**
     * Link to detected post
     */
    postUrl: string;

    /**
     * The confidence of the report, between 0 and 100
     */
    confidence?: number;

    /**
     * A list of reasons the report was detected
     */
    reasons?: Array<string>;

    /**
     * Any custom attributes to be associated with the report
     */
    attributes?: Array<models.RegsiterPostAttribute>;

}
