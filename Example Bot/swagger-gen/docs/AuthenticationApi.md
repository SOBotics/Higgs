# IO.Swagger.Api.AuthenticationApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AuthenticationRefreshTokenPost**](AuthenticationApi.md#authenticationrefreshtokenpost) | **POST** /Authentication/RefreshToken | 


<a name="authenticationrefreshtokenpost"></a>
# **AuthenticationRefreshTokenPost**
> string AuthenticationRefreshTokenPost ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AuthenticationRefreshTokenPostExample
    {
        public void main()
        {
            var apiInstance = new AuthenticationApi();

            try
            {
                string result = apiInstance.AuthenticationRefreshTokenPost();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AuthenticationApi.AuthenticationRefreshTokenPost: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

