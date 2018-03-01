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


export interface EditCreateBotRequest {
    /**
     * Id of bot to be edited
     */
    botId: number;
    /**
     * New name of the bot
     */
    name?: string;
    /**
     * New description for the bot
     */
    description?: string;
    /**
     * New public key for the bot
     */
    publicKey?: string;
}
