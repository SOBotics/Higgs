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


export interface RegisterPostContentFragment {
    /**
     * The order in which the content will be displayed to the user
     */
    order?: number;
    /**
     * The name of the content type
     */
    name?: string;
    /**
     * The content itself
     */
    content?: string;
    /**
     * Required scopes the user must have to view the content.
     */
    requiredScope?: string;
}