# IO.Swagger.Api.AdminApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AdminBotGet**](AdminApi.md#adminbotget) | **GET** /Admin/Bot | 
[**AdminBotScopesGet**](AdminApi.md#adminbotscopesget) | **GET** /Admin/BotScopes | 
[**AdminBotsGet**](AdminApi.md#adminbotsget) | **GET** /Admin/Bots | Lists all bots
[**AdminDeactiveateBotPost**](AdminApi.md#admindeactiveatebotpost) | **POST** /Admin/DeactiveateBot | Deactivates a bot
[**AdminEditBotFeedbackTypesPost**](AdminApi.md#admineditbotfeedbacktypespost) | **POST** /Admin/EditBotFeedbackTypes | 
[**AdminEditBotPost**](AdminApi.md#admineditbotpost) | **POST** /Admin/EditBot | Update a bots details
[**AdminRegisterBotPost**](AdminApi.md#adminregisterbotpost) | **POST** /Admin/RegisterBot | Register a bot
[**AdminScopesGet**](AdminApi.md#adminscopesget) | **GET** /Admin/Scopes | 
[**AdminSetBotScopesPost**](AdminApi.md#adminsetbotscopespost) | **POST** /Admin/SetBotScopes | Set bot scopes
[**AdminSetUserScopesPost**](AdminApi.md#adminsetuserscopespost) | **POST** /Admin/SetUserScopes | Add a scope to a user
[**AdminUsersGet**](AdminApi.md#adminusersget) | **GET** /Admin/Users | Lists all users
[**AdminViewBotFeedbackTypesGet**](AdminApi.md#adminviewbotfeedbacktypesget) | **GET** /Admin/ViewBotFeedbackTypes | 


<a name="adminbotget"></a>
# **AdminBotGet**
> BotResponse AdminBotGet (int? botId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminBotGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var botId = 56;  // int? | 

            try
            {
                BotResponse result = apiInstance.AdminBotGet(botId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminBotGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **botId** | **int?**|  | 

### Return type

[**BotResponse**](BotResponse.md)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminbotscopesget"></a>
# **AdminBotScopesGet**
> List<string> AdminBotScopesGet (int? botId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminBotScopesGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var botId = 56;  // int? | 

            try
            {
                List&lt;string&gt; result = apiInstance.AdminBotScopesGet(botId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminBotScopesGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **botId** | **int?**|  | 

### Return type

**List<string>**

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminbotsget"></a>
# **AdminBotsGet**
> List<BotsResponse> AdminBotsGet ()

Lists all bots

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminBotsGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();

            try
            {
                // Lists all bots
                List&lt;BotsResponse&gt; result = apiInstance.AdminBotsGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminBotsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<BotsResponse>**](BotsResponse.md)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="admindeactiveatebotpost"></a>
# **AdminDeactiveateBotPost**
> void AdminDeactiveateBotPost (DeleteBotRequest request = null)

Deactivates a bot

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminDeactiveateBotPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new DeleteBotRequest(); // DeleteBotRequest |  (optional) 

            try
            {
                // Deactivates a bot
                apiInstance.AdminDeactiveateBotPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminDeactiveateBotPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**DeleteBotRequest**](DeleteBotRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="admineditbotfeedbacktypespost"></a>
# **AdminEditBotFeedbackTypesPost**
> void AdminEditBotFeedbackTypesPost (EditBotFeedbackTypesRequest request = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminEditBotFeedbackTypesPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new EditBotFeedbackTypesRequest(); // EditBotFeedbackTypesRequest |  (optional) 

            try
            {
                apiInstance.AdminEditBotFeedbackTypesPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminEditBotFeedbackTypesPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**EditBotFeedbackTypesRequest**](EditBotFeedbackTypesRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="admineditbotpost"></a>
# **AdminEditBotPost**
> void AdminEditBotPost (EditCreateBotRequest request = null)

Update a bots details

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminEditBotPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new EditCreateBotRequest(); // EditCreateBotRequest |  (optional) 

            try
            {
                // Update a bots details
                apiInstance.AdminEditBotPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminEditBotPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**EditCreateBotRequest**](EditCreateBotRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminregisterbotpost"></a>
# **AdminRegisterBotPost**
> int? AdminRegisterBotPost (CreateBotRequest request = null)

Register a bot

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminRegisterBotPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new CreateBotRequest(); // CreateBotRequest |  (optional) 

            try
            {
                // Register a bot
                int? result = apiInstance.AdminRegisterBotPost(request);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminRegisterBotPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**CreateBotRequest**](CreateBotRequest.md)|  | [optional] 

### Return type

**int?**

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminscopesget"></a>
# **AdminScopesGet**
> List<string> AdminScopesGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminScopesGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();

            try
            {
                List&lt;string&gt; result = apiInstance.AdminScopesGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminScopesGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

**List<string>**

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminsetbotscopespost"></a>
# **AdminSetBotScopesPost**
> void AdminSetBotScopesPost (SetBotScopesRequest request = null)

Set bot scopes

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminSetBotScopesPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new SetBotScopesRequest(); // SetBotScopesRequest |  (optional) 

            try
            {
                // Set bot scopes
                apiInstance.AdminSetBotScopesPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminSetBotScopesPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**SetBotScopesRequest**](SetBotScopesRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminsetuserscopespost"></a>
# **AdminSetUserScopesPost**
> void AdminSetUserScopesPost (AddUserScopeRequest request = null)

Add a scope to a user

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminSetUserScopesPostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new AddUserScopeRequest(); // AddUserScopeRequest |  (optional) 

            try
            {
                // Add a scope to a user
                apiInstance.AdminSetUserScopesPost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminSetUserScopesPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**AddUserScopeRequest**](AddUserScopeRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminusersget"></a>
# **AdminUsersGet**
> UsersResponse AdminUsersGet ()

Lists all users

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminUsersGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();

            try
            {
                // Lists all users
                UsersResponse result = apiInstance.AdminUsersGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminUsersGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**UsersResponse**](UsersResponse.md)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminviewbotfeedbacktypesget"></a>
# **AdminViewBotFeedbackTypesGet**
> List<ViewBotFeedbackTypesResponse> AdminViewBotFeedbackTypesGet (int? botId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminViewBotFeedbackTypesGetExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var botId = 56;  // int? | 

            try
            {
                List&lt;ViewBotFeedbackTypesResponse&gt; result = apiInstance.AdminViewBotFeedbackTypesGet(botId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminViewBotFeedbackTypesGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **botId** | **int?**|  | 

### Return type

[**List<ViewBotFeedbackTypesResponse>**](ViewBotFeedbackTypesResponse.md)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

