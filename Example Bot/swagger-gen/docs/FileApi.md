# IO.Swagger.Api.FileApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FileByIdGet**](FileApi.md#filebyidget) | **GET** /File/{id} | 
[**FileGet**](FileApi.md#fileget) | **GET** /File | 
[**FilePost**](FileApi.md#filepost) | **POST** /File | 


<a name="filebyidget"></a>
# **FileByIdGet**
> void FileByIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class FileByIdGetExample
    {
        public void main()
        {
            var apiInstance = new FileApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.FileByIdGet(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FileApi.FileByIdGet: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fileget"></a>
# **FileGet**
> void FileGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class FileGetExample
    {
        public void main()
        {
            var apiInstance = new FileApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.FileGet(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FileApi.FileGet: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filepost"></a>
# **FilePost**
> void FilePost (AddFileRequest request = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class FilePostExample
    {
        public void main()
        {
            var apiInstance = new FileApi();
            var request = new AddFileRequest(); // AddFileRequest |  (optional) 

            try
            {
                apiInstance.FilePost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FileApi.FilePost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**AddFileRequest**](AddFileRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

