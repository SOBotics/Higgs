# IO.Swagger.Api.AdminApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AdminAddBotScopePost**](AdminApi.md#adminaddbotscopepost) | **POST** /Admin/AddBotScope | Add a scope to a bot
[**AdminAddUserScopePost**](AdminApi.md#adminadduserscopepost) | **POST** /Admin/AddUserScope | Add a scope to a user
[**AdminBotGet**](AdminApi.md#adminbotget) | **GET** /Admin/Bot | 
[**AdminBotsGet**](AdminApi.md#adminbotsget) | **GET** /Admin/Bots | Lists all bots
[**AdminDeactiveateBotPost**](AdminApi.md#admindeactiveatebotpost) | **POST** /Admin/DeactiveateBot | Deactivates a bot
[**AdminEditBotPost**](AdminApi.md#admineditbotpost) | **POST** /Admin/EditBot | Update a bots details
[**AdminRegisterBotPost**](AdminApi.md#adminregisterbotpost) | **POST** /Admin/RegisterBot | Register a bot
[**AdminRemoveBotScopePost**](AdminApi.md#adminremovebotscopepost) | **POST** /Admin/RemoveBotScope | Remove a scope from a bot
[**AdminRemoveUserScopePost**](AdminApi.md#adminremoveuserscopepost) | **POST** /Admin/RemoveUserScope | Remove a scope from a user
[**AdminUsersGet**](AdminApi.md#adminusersget) | **GET** /Admin/Users | Lists all users


<a name="adminaddbotscopepost"></a>
# **AdminAddBotScopePost**
> void AdminAddBotScopePost (DeleteCreateBotRequest request = null)

Add a scope to a bot

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminAddBotScopePostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new DeleteCreateBotRequest(); // DeleteCreateBotRequest |  (optional) 

            try
            {
                // Add a scope to a bot
                apiInstance.AdminAddBotScopePost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminAddBotScopePost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**DeleteCreateBotRequest**](DeleteCreateBotRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminadduserscopepost"></a>
# **AdminAddUserScopePost**
> void AdminAddUserScopePost (AddUserScopeRequest request = null)

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
    public class AdminAddUserScopePostExample
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
                apiInstance.AdminAddUserScopePost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminAddUserScopePost: " + e.Message );
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
> void AdminDeactiveateBotPost (DeleteCreateBotRequest request = null)

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
            var request = new DeleteCreateBotRequest(); // DeleteCreateBotRequest |  (optional) 

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
 **request** | [**DeleteCreateBotRequest**](DeleteCreateBotRequest.md)|  | [optional] 

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

<a name="adminremovebotscopepost"></a>
# **AdminRemoveBotScopePost**
> void AdminRemoveBotScopePost (AddBotScopeRequest request = null)

Remove a scope from a bot

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminRemoveBotScopePostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new AddBotScopeRequest(); // AddBotScopeRequest |  (optional) 

            try
            {
                // Remove a scope from a bot
                apiInstance.AdminRemoveBotScopePost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminRemoveBotScopePost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**AddBotScopeRequest**](AddBotScopeRequest.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[oauth2](../README.md#oauth2)

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="adminremoveuserscopepost"></a>
# **AdminRemoveUserScopePost**
> void AdminRemoveUserScopePost (RemoveUserScopeRequest request = null)

Remove a scope from a user

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AdminRemoveUserScopePostExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: oauth2
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AdminApi();
            var request = new RemoveUserScopeRequest(); // RemoveUserScopeRequest |  (optional) 

            try
            {
                // Remove a scope from a user
                apiInstance.AdminRemoveUserScopePost(request);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AdminApi.AdminRemoveUserScopePost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **request** | [**RemoveUserScopeRequest**](RemoveUserScopeRequest.md)|  | [optional] 

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

