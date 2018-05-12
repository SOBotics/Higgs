# IO.Swagger.Api.AnalyticsApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AnalyticsReportsByFeedbackGet**](AnalyticsApi.md#analyticsreportsbyfeedbackget) | **GET** /Analytics/ReportsByFeedback | 
[**AnalyticsReportsByReasonGet**](AnalyticsApi.md#analyticsreportsbyreasonget) | **GET** /Analytics/ReportsByReason | 
[**AnalyticsReportsOverTimeGet**](AnalyticsApi.md#analyticsreportsovertimeget) | **GET** /Analytics/ReportsOverTime | 
[**AnalyticsReportsTotalGet**](AnalyticsApi.md#analyticsreportstotalget) | **GET** /Analytics/ReportsTotal | 


<a name="analyticsreportsbyfeedbackget"></a>
# **AnalyticsReportsByFeedbackGet**
> List<ReportsByFeedbackResponse> AnalyticsReportsByFeedbackGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AnalyticsReportsByFeedbackGetExample
    {
        public void main()
        {
            var apiInstance = new AnalyticsApi();

            try
            {
                List&lt;ReportsByFeedbackResponse&gt; result = apiInstance.AnalyticsReportsByFeedbackGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsReportsByFeedbackGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ReportsByFeedbackResponse>**](ReportsByFeedbackResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="analyticsreportsbyreasonget"></a>
# **AnalyticsReportsByReasonGet**
> List<ReportsByReasonResponse> AnalyticsReportsByReasonGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AnalyticsReportsByReasonGetExample
    {
        public void main()
        {
            var apiInstance = new AnalyticsApi();

            try
            {
                List&lt;ReportsByReasonResponse&gt; result = apiInstance.AnalyticsReportsByReasonGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsReportsByReasonGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ReportsByReasonResponse>**](ReportsByReasonResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="analyticsreportsovertimeget"></a>
# **AnalyticsReportsOverTimeGet**
> List<ReportsOverTimeResponse> AnalyticsReportsOverTimeGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AnalyticsReportsOverTimeGetExample
    {
        public void main()
        {
            var apiInstance = new AnalyticsApi();

            try
            {
                List&lt;ReportsOverTimeResponse&gt; result = apiInstance.AnalyticsReportsOverTimeGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsReportsOverTimeGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ReportsOverTimeResponse>**](ReportsOverTimeResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="analyticsreportstotalget"></a>
# **AnalyticsReportsTotalGet**
> List<ReportsTotalResponse> AnalyticsReportsTotalGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AnalyticsReportsTotalGetExample
    {
        public void main()
        {
            var apiInstance = new AnalyticsApi();

            try
            {
                List&lt;ReportsTotalResponse&gt; result = apiInstance.AnalyticsReportsTotalGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnalyticsApi.AnalyticsReportsTotalGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<ReportsTotalResponse>**](ReportsTotalResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

