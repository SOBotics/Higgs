# IO.Swagger.Api.BotApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**BotAquireTokenPost**](BotApi.md#botaquiretokenpost) | **POST** /Bot/AquireToken | 
[**BotRegisterPostPost**](BotApi.md#botregisterpostpost) | **POST** /Bot/RegisterPost | Used by bots to register a detected post
[**BotRegisterUserFeedbackByContentPost**](BotApi.md#botregisteruserfeedbackbycontentpost) | **POST** /Bot/RegisterUserFeedbackByContent | 
[**BotRegisterUserFeedbackPost**](BotApi.md#botregisteruserfeedbackpost) | **POST** /Bot/RegisterUserFeedback | 


<a name="botaquiretokenpost"></a>
# **BotAquireTokenPost**
> AquireTokenResponse BotAquireTokenPost (AquireTokenRequest request = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BotAquireTokenPostExample
    {
        public void main()
        {
            var apiInstance = new BotApi();
            var request = new AquireTokenRequest(); // AquireTokenRequest |  (optional) 

            try
            {
                AquireTokenResponse result = apiInstance.BotAquireTokenPost(request);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BotApi.BotAquireTokenPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**AquireTokenRequest**](AquireTokenRequest.md)|  | [optional] 

### Return type

[**AquireTokenResponse**](AquireTokenResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="botregisterpostpost"></a>
# **BotRegisterPostPost**
> int? BotRegisterPostPost (RegisterPostRequest request = null)

Used by bots to register a detected post

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BotRegisterPostPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new BotApi();
            var request = new RegisterPostRequest(); // RegisterPostRequest |  (optional) 

            try
            {
                // Used by bots to register a detected post
                int? result = apiInstance.BotRegisterPostPost(request);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BotApi.BotRegisterPostPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**RegisterPostRequest**](RegisterPostRequest.md)|  | [optional] 

### Return type

**int?**

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="botregisteruserfeedbackbycontentpost"></a>
# **BotRegisterUserFeedbackByContentPost**
> void BotRegisterUserFeedbackByContentPost (RegisterUserFeedbackByContentRequest request = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BotRegisterUserFeedbackByContentPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new BotApi();
            var request = new RegisterUserFeedbackByContentRequest(); // RegisterUserFeedbackByContentRequest |  (optional) 

            try
            {
                apiInstance.BotRegisterUserFeedbackByContentPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BotApi.BotRegisterUserFeedbackByContentPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**RegisterUserFeedbackByContentRequest**](RegisterUserFeedbackByContentRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="botregisteruserfeedbackpost"></a>
# **BotRegisterUserFeedbackPost**
> void BotRegisterUserFeedbackPost (RegisterUserFeedbackRequest request = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BotRegisterUserFeedbackPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new BotApi();
            var request = new RegisterUserFeedbackRequest(); // RegisterUserFeedbackRequest |  (optional) 

            try
            {
                apiInstance.BotRegisterUserFeedbackPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BotApi.BotRegisterUserFeedbackPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**RegisterUserFeedbackRequest**](RegisterUserFeedbackRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

