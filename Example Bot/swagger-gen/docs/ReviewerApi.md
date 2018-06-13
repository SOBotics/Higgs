# IO.Swagger.Api.ReviewerApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ReviewerBotByDashboardGet**](ReviewerApi.md#reviewerbotbydashboardget) | **GET** /Reviewer/BotByDashboard | 
[**ReviewerCheckGet**](ReviewerApi.md#reviewercheckget) | **GET** /Reviewer/Check | 
[**ReviewerClearFeedbackPost**](ReviewerApi.md#reviewerclearfeedbackpost) | **POST** /Reviewer/ClearFeedback | 
[**ReviewerDashboardsGet**](ReviewerApi.md#reviewerdashboardsget) | **GET** /Reviewer/Dashboards | 
[**ReviewerFeedbacksGet**](ReviewerApi.md#reviewerfeedbacksget) | **GET** /Reviewer/Feedbacks | 
[**ReviewerNextReviewGet**](ReviewerApi.md#reviewernextreviewget) | **GET** /Reviewer/NextReview | 
[**ReviewerPendingReviewsGet**](ReviewerApi.md#reviewerpendingreviewsget) | **GET** /Reviewer/PendingReviews | 
[**ReviewerReasonsGet**](ReviewerApi.md#reviewerreasonsget) | **GET** /Reviewer/Reasons | 
[**ReviewerReportGet**](ReviewerApi.md#reviewerreportget) | **GET** /Reviewer/Report | 
[**ReviewerReportsGet**](ReviewerApi.md#reviewerreportsget) | **GET** /Reviewer/Reports | 
[**ReviewerSendFeedbackPost**](ReviewerApi.md#reviewersendfeedbackpost) | **POST** /Reviewer/SendFeedback | Lists all pending review


<a name="reviewerbotbydashboardget"></a>
# **ReviewerBotByDashboardGet**
> int? ReviewerBotByDashboardGet (string dashboardName = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerBotByDashboardGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var dashboardName = dashboardName_example;  // string |  (optional) 

            try
            {
                int? result = apiInstance.ReviewerBotByDashboardGet(dashboardName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerBotByDashboardGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **dashboardName** | **string**|  | [optional] 

### Return type

**int?**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewercheckget"></a>
# **ReviewerCheckGet**
> List<ReviewerCheckResponse> ReviewerCheckGet (string contentUrl = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerCheckGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var contentUrl = contentUrl_example;  // string |  (optional) 

            try
            {
                List&lt;ReviewerCheckResponse&gt; result = apiInstance.ReviewerCheckGet(contentUrl);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerCheckGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentUrl** | **string**|  | [optional] 

### Return type

[**List<ReviewerCheckResponse>**](ReviewerCheckResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerclearfeedbackpost"></a>
# **ReviewerClearFeedbackPost**
> void ReviewerClearFeedbackPost (ClearFeedbackRequest request = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerClearFeedbackPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ReviewerApi();
            var request = new ClearFeedbackRequest(); // ClearFeedbackRequest |  (optional) 

            try
            {
                apiInstance.ReviewerClearFeedbackPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerClearFeedbackPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**ClearFeedbackRequest**](ClearFeedbackRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerdashboardsget"></a>
# **ReviewerDashboardsGet**
> List<ReviewerDashboardsResponse> ReviewerDashboardsGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerDashboardsGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();

            try
            {
                List&lt;ReviewerDashboardsResponse&gt; result = apiInstance.ReviewerDashboardsGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerDashboardsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ReviewerDashboardsResponse>**](ReviewerDashboardsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerfeedbacksget"></a>
# **ReviewerFeedbacksGet**
> List<ReviewerFeedbacksResponse> ReviewerFeedbacksGet (string dashboardName = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerFeedbacksGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var dashboardName = dashboardName_example;  // string |  (optional) 

            try
            {
                List&lt;ReviewerFeedbacksResponse&gt; result = apiInstance.ReviewerFeedbacksGet(dashboardName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerFeedbacksGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **dashboardName** | **string**|  | [optional] 

### Return type

[**List<ReviewerFeedbacksResponse>**](ReviewerFeedbacksResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewernextreviewget"></a>
# **ReviewerNextReviewGet**
> ReviewerReportResponse ReviewerNextReviewGet (int? lastId = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerNextReviewGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ReviewerApi();
            var lastId = 56;  // int? |  (optional) 

            try
            {
                ReviewerReportResponse result = apiInstance.ReviewerNextReviewGet(lastId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerNextReviewGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **lastId** | **int?**|  | [optional] 

### Return type

[**ReviewerReportResponse**](ReviewerReportResponse.md)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerpendingreviewsget"></a>
# **ReviewerPendingReviewsGet**
> PagingResponseInt32 ReviewerPendingReviewsGet (string dashboardName = null, int? pageNumber = null, int? pageSize = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerPendingReviewsGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var dashboardName = dashboardName_example;  // string |  (optional) 
            var pageNumber = 56;  // int? |  (optional) 
            var pageSize = 56;  // int? |  (optional) 

            try
            {
                PagingResponseInt32 result = apiInstance.ReviewerPendingReviewsGet(dashboardName, pageNumber, pageSize);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerPendingReviewsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **dashboardName** | **string**|  | [optional] 
 **pageNumber** | **int?**|  | [optional] 
 **pageSize** | **int?**|  | [optional] 

### Return type

[**PagingResponseInt32**](PagingResponseInt32.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerreasonsget"></a>
# **ReviewerReasonsGet**
> List<ReviewerReasonsResponse> ReviewerReasonsGet (string dashboardName = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerReasonsGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var dashboardName = dashboardName_example;  // string |  (optional) 

            try
            {
                List&lt;ReviewerReasonsResponse&gt; result = apiInstance.ReviewerReasonsGet(dashboardName);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerReasonsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **dashboardName** | **string**|  | [optional] 

### Return type

[**List<ReviewerReasonsResponse>**](ReviewerReasonsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerreportget"></a>
# **ReviewerReportGet**
> ReviewerReportResponse ReviewerReportGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerReportGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var id = 56;  // int? | 

            try
            {
                ReviewerReportResponse result = apiInstance.ReviewerReportGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerReportGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

[**ReviewerReportResponse**](ReviewerReportResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewerreportsget"></a>
# **ReviewerReportsGet**
> PagingResponseReviewerReportsResponse ReviewerReportsGet (string content = null, int? botId = null, bool? hasFeedback = null, bool? conflicted = null, List<int?> feedbacks = null, List<int?> reasons = null, int? pageNumber = null, int? pageSize = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerReportsGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewerApi();
            var content = content_example;  // string |  (optional) 
            var botId = 56;  // int? |  (optional) 
            var hasFeedback = true;  // bool? |  (optional) 
            var conflicted = true;  // bool? |  (optional) 
            var feedbacks = new List<int?>(); // List<int?> |  (optional) 
            var reasons = new List<int?>(); // List<int?> |  (optional) 
            var pageNumber = 56;  // int? |  (optional) 
            var pageSize = 56;  // int? |  (optional) 

            try
            {
                PagingResponseReviewerReportsResponse result = apiInstance.ReviewerReportsGet(content, botId, hasFeedback, conflicted, feedbacks, reasons, pageNumber, pageSize);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerReportsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **content** | **string**|  | [optional] 
 **botId** | **int?**|  | [optional] 
 **hasFeedback** | **bool?**|  | [optional] 
 **conflicted** | **bool?**|  | [optional] 
 **feedbacks** | [**List&lt;int?&gt;**](int?.md)|  | [optional] 
 **reasons** | [**List&lt;int?&gt;**](int?.md)|  | [optional] 
 **pageNumber** | **int?**|  | [optional] 
 **pageSize** | **int?**|  | [optional] 

### Return type

[**PagingResponseReviewerReportsResponse**](PagingResponseReviewerReportsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="reviewersendfeedbackpost"></a>
# **ReviewerSendFeedbackPost**
> void ReviewerSendFeedbackPost (SendFeedbackRequest request = null)

Lists all pending review

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewerSendFeedbackPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ReviewerApi();
            var request = new SendFeedbackRequest(); // SendFeedbackRequest |  (optional) 

            try
            {
                // Lists all pending review
                apiInstance.ReviewerSendFeedbackPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewerApi.ReviewerSendFeedbackPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**SendFeedbackRequest**](SendFeedbackRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

