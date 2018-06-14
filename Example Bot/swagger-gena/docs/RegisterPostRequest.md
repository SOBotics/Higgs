# IO.Swagger.Model.RegisterPostRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Title** | **string** | Title of the report (for example, the question title) | 
**ContentUrl** | **string** | Link to detected content | 
**ContentSite** | **string** | The site on which the content was detected | [optional] 
**ContentType** | **string** | The type of content (question, answer, comment, etc) | [optional] 
**ContentId** | **int?** | The Id of the content | [optional] 
**DetectionScore** | **double?** | The score of the report, between 0 and 1 | [optional] 
**Content** | **string** | The content of the report | [optional] 
**ContentFragments** | [**List&lt;RegisterPostContentFragment&gt;**](RegisterPostContentFragment.md) | Additional content fragments | [optional] 
**AuthorName** | **string** | The name of the author who created the content | [optional] 
**AuthorReputation** | **int?** | The author&#39;s reputation | [optional] 
**ContentCreationDate** | **DateTime?** | The UTC date the content was created | [optional] 
**DetectedDate** | **DateTime?** | The UTC date the content was detected | [optional] 
**Reasons** | [**List&lt;RegisterPostReason&gt;**](RegisterPostReason.md) | A list of reasons the report was detected | [optional] 
**AllowedFeedback** | **List&lt;string&gt;** | A list of feedback types | [optional] 
**Attributes** | [**List&lt;RegisterPostAttribute&gt;**](RegisterPostAttribute.md) | Any custom attributes to be associated with the report | [optional] 
**RequiredFeedback** | **int?** |  | [optional] 
**RequiredFeedbackConflicted** | **int?** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

