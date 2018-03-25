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


export interface CreateBotRequest {
    /**
     * Public key of bot used to sign JWT payloads
     */
    secret: string;
    /**
     * Name of the bot
     */
    name: string;
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
}
