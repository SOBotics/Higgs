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


export interface RemoveUserScopeRequest {
    /**
     * User to remove the scope from
     */
    userId: number;
    /**
     * Scope to remove
     */
    scope: string;
}
