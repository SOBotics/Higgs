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

export interface AquireTokenRequest {
    /**
     * The ID of the bot
     */
    botId: number;

    /**
     * A list of requested scopes
     */
    requiredScopes: Array<string>;

    /**
     * A randomly generated request ID. Must not be previously used
     */
    requestId: string;

}