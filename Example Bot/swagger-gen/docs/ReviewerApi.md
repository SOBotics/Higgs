# IO.Swagger.Api.ReviewerApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ReviewerCheckGet**](ReviewerApi.md#reviewercheckget) | **GET** /Reviewer/Check | 
[**ReviewerClearFeedbackPost**](ReviewerApi.md#reviewerclearfeedbackpost) | **POST** /Reviewer/ClearFeedback | 
[**ReviewerNextReviewGet**](ReviewerApi.md#reviewernextreviewget) | **GET** /Reviewer/NextReview | 
[**ReviewerPendingReviewsGet**](ReviewerApi.md#reviewerpendingreviewsget) | **GET** /Reviewer/PendingReviews | Lists all pending reviews
[**ReviewerReportGet**](ReviewerApi.md#reviewerreportget) | **GET** /Reviewer/Report | 
[**ReviewerReportsGet**](ReviewerApi.md#reviewerreportsget) | **GET** /Reviewer/Reports | 
[**ReviewerSendFeedbackPost**](ReviewerApi.md#reviewersendfeedbackpost) | **POST** /Reviewer/SendFeedback | Lists all pending review


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
> void ReviewerPendingReviewsGet ()

Lists all pending reviews

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

            try
            {
                // Lists all pending reviews
                apiInstance.ReviewerPendingReviewsGet();
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
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

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
> List<ReviewerReportsResponse> ReviewerReportsGet ()



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

            try
            {
                List&lt;ReviewerReportsResponse&gt; result = apiInstance.ReviewerReportsGet();
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
This endpoint does not need any parameter.

### Return type

[**List<ReviewerReportsResponse>**](ReviewerReportsResponse.md)

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

