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
/* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs/Observable';

import { AquireTokenResponse } from '../model/aquireTokenResponse';
import { ErrorResponse } from '../model/errorResponse';
import { RegisterFeedbackTypesRequest } from '../model/registerFeedbackTypesRequest';
import { RegisterPostRequest } from '../model/registerPostRequest';
import { RegisterUserFeedbackByContentRequest } from '../model/registerUserFeedbackByContentRequest';
import { RegisterUserFeedbackRequest } from '../model/registerUserFeedbackRequest';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class BotService {

    protected basePath = 'https://localhost';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (let consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param botId 
     * @param secret 
     * @param requestedScopes 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public botAquireTokenPost(botId: number, secret: string, requestedScopes?: Array<string>, observe?: 'body', reportProgress?: boolean): Observable<AquireTokenResponse>;
    public botAquireTokenPost(botId: number, secret: string, requestedScopes?: Array<string>, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AquireTokenResponse>>;
    public botAquireTokenPost(botId: number, secret: string, requestedScopes?: Array<string>, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AquireTokenResponse>>;
    public botAquireTokenPost(botId: number, secret: string, requestedScopes?: Array<string>, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {
        if (botId === null || botId === undefined) {
            throw new Error('Required parameter botId was null or undefined when calling botAquireTokenPost.');
        }
        if (secret === null || secret === undefined) {
            throw new Error('Required parameter secret was null or undefined when calling botAquireTokenPost.');
        }

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (botId !== undefined) {
            queryParameters = queryParameters.set('BotId', <any>botId);
        }
        if (secret !== undefined) {
            queryParameters = queryParameters.set('Secret', <any>secret);
        }
        if (requestedScopes) {
            requestedScopes.forEach((element) => {
                queryParameters = queryParameters.append('RequestedScopes', <any>element);
            })
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.post<AquireTokenResponse>(`${this.basePath}/Bot/AquireToken`,
            null,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * Used by bots to register feedback types
     * 
     * @param request 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public botRegisterFeedbackTypesPost(request?: RegisterFeedbackTypesRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public botRegisterFeedbackTypesPost(request?: RegisterFeedbackTypesRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public botRegisterFeedbackTypesPost(request?: RegisterFeedbackTypesRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public botRegisterFeedbackTypesPost(request?: RegisterFeedbackTypesRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (oauth2) required
        if (this.configuration.accessToken) {
            let accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        let httpContentTypeSelected:string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set("Content-Type", httpContentTypeSelected);
        }

        return this.httpClient.post<any>(`${this.basePath}/Bot/RegisterFeedbackTypes`,
            request,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * Used by bots to register a detected post
     * 
     * @param request 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public botRegisterPostPost(request?: RegisterPostRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public botRegisterPostPost(request?: RegisterPostRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public botRegisterPostPost(request?: RegisterPostRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public botRegisterPostPost(request?: RegisterPostRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (oauth2) required
        if (this.configuration.accessToken) {
            let accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        let httpContentTypeSelected:string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set("Content-Type", httpContentTypeSelected);
        }

        return this.httpClient.post<any>(`${this.basePath}/Bot/RegisterPost`,
            request,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param request 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public botRegisterUserFeedbackByContentPost(request?: RegisterUserFeedbackByContentRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public botRegisterUserFeedbackByContentPost(request?: RegisterUserFeedbackByContentRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public botRegisterUserFeedbackByContentPost(request?: RegisterUserFeedbackByContentRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public botRegisterUserFeedbackByContentPost(request?: RegisterUserFeedbackByContentRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (oauth2) required
        if (this.configuration.accessToken) {
            let accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        let httpContentTypeSelected:string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set("Content-Type", httpContentTypeSelected);
        }

        return this.httpClient.post<any>(`${this.basePath}/Bot/RegisterUserFeedbackByContent`,
            request,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param request 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public botRegisterUserFeedbackPost(request?: RegisterUserFeedbackRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public botRegisterUserFeedbackPost(request?: RegisterUserFeedbackRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public botRegisterUserFeedbackPost(request?: RegisterUserFeedbackRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public botRegisterUserFeedbackPost(request?: RegisterUserFeedbackRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (oauth2) required
        if (this.configuration.accessToken) {
            let accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        let httpContentTypeSelected:string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set("Content-Type", httpContentTypeSelected);
        }

        return this.httpClient.post<any>(`${this.basePath}/Bot/RegisterUserFeedback`,
            request,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
