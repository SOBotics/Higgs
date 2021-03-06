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

import { Observable }                                        from 'rxjs';

import { ClearFeedbackRequest } from '../model/clearFeedbackRequest';
import { PagingResponseInt32 } from '../model/pagingResponseInt32';
import { PagingResponseReviewerReportsResponse } from '../model/pagingResponseReviewerReportsResponse';
import { ReviewerCheckResponse } from '../model/reviewerCheckResponse';
import { ReviewerDashboardResponse } from '../model/reviewerDashboardResponse';
import { ReviewerDashboardsResponse } from '../model/reviewerDashboardsResponse';
import { ReviewerFeedbacksResponse } from '../model/reviewerFeedbacksResponse';
import { ReviewerReasonsResponse } from '../model/reviewerReasonsResponse';
import { ReviewerReportResponse } from '../model/reviewerReportResponse';
import { SendFeedbackRequest } from '../model/sendFeedbackRequest';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class ReviewerService {

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
     * @param contentUrl 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerCheckGet(contentUrl?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<ReviewerCheckResponse>>;
    public reviewerCheckGet(contentUrl?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ReviewerCheckResponse>>>;
    public reviewerCheckGet(contentUrl?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ReviewerCheckResponse>>>;
    public reviewerCheckGet(contentUrl?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (contentUrl !== undefined) {
            queryParameters = queryParameters.set('contentUrl', <any>contentUrl);
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

        return this.httpClient.get<Array<ReviewerCheckResponse>>(`${this.basePath}/Reviewer/Check`,
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
     * 
     * 
     * @param request 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerClearFeedbackPost(request?: ClearFeedbackRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public reviewerClearFeedbackPost(request?: ClearFeedbackRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public reviewerClearFeedbackPost(request?: ClearFeedbackRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public reviewerClearFeedbackPost(request?: ClearFeedbackRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.post<any>(`${this.basePath}/Reviewer/ClearFeedback`,
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
     * @param dashboardName 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerDashboardGet(dashboardName?: string, observe?: 'body', reportProgress?: boolean): Observable<ReviewerDashboardResponse>;
    public reviewerDashboardGet(dashboardName?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<ReviewerDashboardResponse>>;
    public reviewerDashboardGet(dashboardName?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<ReviewerDashboardResponse>>;
    public reviewerDashboardGet(dashboardName?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (dashboardName !== undefined) {
            queryParameters = queryParameters.set('dashboardName', <any>dashboardName);
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

        return this.httpClient.get<ReviewerDashboardResponse>(`${this.basePath}/Reviewer/Dashboard`,
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
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerDashboardsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<ReviewerDashboardsResponse>>;
    public reviewerDashboardsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ReviewerDashboardsResponse>>>;
    public reviewerDashboardsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ReviewerDashboardsResponse>>>;
    public reviewerDashboardsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.get<Array<ReviewerDashboardsResponse>>(`${this.basePath}/Reviewer/Dashboards`,
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
     * @param dashboardName 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerFeedbacksGet(dashboardName?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<ReviewerFeedbacksResponse>>;
    public reviewerFeedbacksGet(dashboardName?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ReviewerFeedbacksResponse>>>;
    public reviewerFeedbacksGet(dashboardName?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ReviewerFeedbacksResponse>>>;
    public reviewerFeedbacksGet(dashboardName?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (dashboardName !== undefined) {
            queryParameters = queryParameters.set('dashboardName', <any>dashboardName);
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

        return this.httpClient.get<Array<ReviewerFeedbacksResponse>>(`${this.basePath}/Reviewer/Feedbacks`,
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
     * 
     * 
     * @param lastId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerNextReviewGet(lastId?: number, observe?: 'body', reportProgress?: boolean): Observable<ReviewerReportResponse>;
    public reviewerNextReviewGet(lastId?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<ReviewerReportResponse>>;
    public reviewerNextReviewGet(lastId?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<ReviewerReportResponse>>;
    public reviewerNextReviewGet(lastId?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (lastId !== undefined) {
            queryParameters = queryParameters.set('lastId', <any>lastId);
        }

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
        ];

        return this.httpClient.get<ReviewerReportResponse>(`${this.basePath}/Reviewer/NextReview`,
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
     * 
     * 
     * @param dashboardName 
     * @param pageNumber 
     * @param pageSize 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerPendingReviewsGet(dashboardName?: string, pageNumber?: number, pageSize?: number, observe?: 'body', reportProgress?: boolean): Observable<PagingResponseInt32>;
    public reviewerPendingReviewsGet(dashboardName?: string, pageNumber?: number, pageSize?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PagingResponseInt32>>;
    public reviewerPendingReviewsGet(dashboardName?: string, pageNumber?: number, pageSize?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PagingResponseInt32>>;
    public reviewerPendingReviewsGet(dashboardName?: string, pageNumber?: number, pageSize?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (dashboardName !== undefined) {
            queryParameters = queryParameters.set('dashboardName', <any>dashboardName);
        }
        if (pageNumber !== undefined) {
            queryParameters = queryParameters.set('PageNumber', <any>pageNumber);
        }
        if (pageSize !== undefined) {
            queryParameters = queryParameters.set('PageSize', <any>pageSize);
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

        return this.httpClient.get<PagingResponseInt32>(`${this.basePath}/Reviewer/PendingReviews`,
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
     * 
     * 
     * @param dashboardName 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerReasonsGet(dashboardName?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<ReviewerReasonsResponse>>;
    public reviewerReasonsGet(dashboardName?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ReviewerReasonsResponse>>>;
    public reviewerReasonsGet(dashboardName?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ReviewerReasonsResponse>>>;
    public reviewerReasonsGet(dashboardName?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (dashboardName !== undefined) {
            queryParameters = queryParameters.set('dashboardName', <any>dashboardName);
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

        return this.httpClient.get<Array<ReviewerReasonsResponse>>(`${this.basePath}/Reviewer/Reasons`,
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
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerReportGet(id: number, observe?: 'body', reportProgress?: boolean): Observable<ReviewerReportResponse>;
    public reviewerReportGet(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<ReviewerReportResponse>>;
    public reviewerReportGet(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<ReviewerReportResponse>>;
    public reviewerReportGet(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {
        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling reviewerReportGet.');
        }

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (id !== undefined) {
            queryParameters = queryParameters.set('id', <any>id);
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

        return this.httpClient.get<ReviewerReportResponse>(`${this.basePath}/Reviewer/Report`,
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
     * 
     * 
     * @param content 
     * @param dashboardId 
     * @param hasFeedback 
     * @param conflicted 
     * @param feedbacks 
     * @param reasons 
     * @param pageNumber 
     * @param pageSize 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerReportsGet(content?: string, dashboardId?: number, hasFeedback?: boolean, conflicted?: boolean, feedbacks?: Array<number>, reasons?: Array<number>, pageNumber?: number, pageSize?: number, observe?: 'body', reportProgress?: boolean): Observable<PagingResponseReviewerReportsResponse>;
    public reviewerReportsGet(content?: string, dashboardId?: number, hasFeedback?: boolean, conflicted?: boolean, feedbacks?: Array<number>, reasons?: Array<number>, pageNumber?: number, pageSize?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PagingResponseReviewerReportsResponse>>;
    public reviewerReportsGet(content?: string, dashboardId?: number, hasFeedback?: boolean, conflicted?: boolean, feedbacks?: Array<number>, reasons?: Array<number>, pageNumber?: number, pageSize?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PagingResponseReviewerReportsResponse>>;
    public reviewerReportsGet(content?: string, dashboardId?: number, hasFeedback?: boolean, conflicted?: boolean, feedbacks?: Array<number>, reasons?: Array<number>, pageNumber?: number, pageSize?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (content !== undefined) {
            queryParameters = queryParameters.set('Content', <any>content);
        }
        if (dashboardId !== undefined) {
            queryParameters = queryParameters.set('DashboardId', <any>dashboardId);
        }
        if (hasFeedback !== undefined) {
            queryParameters = queryParameters.set('HasFeedback', <any>hasFeedback);
        }
        if (conflicted !== undefined) {
            queryParameters = queryParameters.set('Conflicted', <any>conflicted);
        }
        if (feedbacks) {
            feedbacks.forEach((element) => {
                queryParameters = queryParameters.append('Feedbacks', <any>element);
            })
        }
        if (reasons) {
            reasons.forEach((element) => {
                queryParameters = queryParameters.append('Reasons', <any>element);
            })
        }
        if (pageNumber !== undefined) {
            queryParameters = queryParameters.set('PageNumber', <any>pageNumber);
        }
        if (pageSize !== undefined) {
            queryParameters = queryParameters.set('PageSize', <any>pageSize);
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

        return this.httpClient.get<PagingResponseReviewerReportsResponse>(`${this.basePath}/Reviewer/Reports`,
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
     * Lists all pending review
     * 
     * @param request 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public reviewerSendFeedbackPost(request?: SendFeedbackRequest, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public reviewerSendFeedbackPost(request?: SendFeedbackRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public reviewerSendFeedbackPost(request?: SendFeedbackRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public reviewerSendFeedbackPost(request?: SendFeedbackRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.post<any>(`${this.basePath}/Reviewer/SendFeedback`,
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
