using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDataStoreItemApi
    {
        /// <summary>
        /// Get DataStoreItem by ID 
        /// </summary>
        /// <param name="id">DataStoreItem Id</param>
        /// <returns>DataStoreItem_</returns>
        DataStoreItem_ GET1 (string id);
        /// <summary>
        /// Get a list of DataStoreItem entities filter by Category 
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREFILTERBYCATEGORY (string category);
        /// <summary>
        /// Get a list of DataStoreItem entities filter by DisplayName 
        /// </summary>
        /// <param name="displayName">DisplayName</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREFILTERBYDISPLAYNAME (string displayName);
        /// <summary>
        /// Get a list of DataStoreItem entities filter by GroupName 
        /// </summary>
        /// <param name="groupName">GroupName</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREFILTERBYGROUPNAME (string groupName);
        /// <summary>
        /// Get a list of DataStoreItem entities filter by ItemName 
        /// </summary>
        /// <param name="itemName">ItemName</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREFILTERBYITEMNAME (string itemName);
        /// <summary>
        /// Get a list of DataStoreItem entities filter by Source 
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREFILTERBYSOURCE (string source);
        /// <summary>
        /// Get a list of DataStoreItem entities filter by Type 
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREFILTERBYTYPE (string type);
        /// <summary>
        /// Get a list of DataStoreItem entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        List<DataStoreItem_> SECUREPOLL (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class DataStoreItemApi : IDataStoreItemApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStoreItemApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public DataStoreItemApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStoreItemApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DataStoreItemApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// Get DataStoreItem by ID 
        /// </summary>
        /// <param name="id">DataStoreItem Id</param>
        /// <returns>DataStoreItem_</returns>
        public DataStoreItem_ GET1 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET1");
    
            var path = "/DataStoreItem/ByKey/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GET1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (DataStoreItem_) ApiClient.Deserialize(response.Content, typeof(DataStoreItem_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities filter by Category 
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREFILTERBYCATEGORY (string category)
        {
            // verify the required parameter 'category' is set
            if (category == null) throw new ApiException(400, "Missing required parameter 'category' when calling SECUREFILTERBYCATEGORY");
    
            var path = "/DataStoreItem/ByCategory/{Category}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Category" + "}", ApiClient.ParameterToString(category));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYCATEGORY: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYCATEGORY: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities filter by DisplayName 
        /// </summary>
        /// <param name="displayName">DisplayName</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREFILTERBYDISPLAYNAME (string displayName)
        {
            // verify the required parameter 'displayName' is set
            if (displayName == null) throw new ApiException(400, "Missing required parameter 'displayName' when calling SECUREFILTERBYDISPLAYNAME");
    
            var path = "/DataStoreItem/ByDisplayName/{DisplayName}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "DisplayName" + "}", ApiClient.ParameterToString(displayName));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYDISPLAYNAME: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYDISPLAYNAME: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities filter by GroupName 
        /// </summary>
        /// <param name="groupName">GroupName</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREFILTERBYGROUPNAME (string groupName)
        {
            // verify the required parameter 'groupName' is set
            if (groupName == null) throw new ApiException(400, "Missing required parameter 'groupName' when calling SECUREFILTERBYGROUPNAME");
    
            var path = "/DataStoreItem/ByGroupName/{GroupName}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "GroupName" + "}", ApiClient.ParameterToString(groupName));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYGROUPNAME: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYGROUPNAME: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities filter by ItemName 
        /// </summary>
        /// <param name="itemName">ItemName</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREFILTERBYITEMNAME (string itemName)
        {
            // verify the required parameter 'itemName' is set
            if (itemName == null) throw new ApiException(400, "Missing required parameter 'itemName' when calling SECUREFILTERBYITEMNAME");
    
            var path = "/DataStoreItem/ByItemName/{ItemName}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "ItemName" + "}", ApiClient.ParameterToString(itemName));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYITEMNAME: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYITEMNAME: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities filter by Source 
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREFILTERBYSOURCE (string source)
        {
            // verify the required parameter 'source' is set
            if (source == null) throw new ApiException(400, "Missing required parameter 'source' when calling SECUREFILTERBYSOURCE");
    
            var path = "/DataStoreItem/BySource/{Source}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Source" + "}", ApiClient.ParameterToString(source));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSOURCE: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSOURCE: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities filter by Type 
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREFILTERBYTYPE (string type)
        {
            // verify the required parameter 'type' is set
            if (type == null) throw new ApiException(400, "Missing required parameter 'type' when calling SECUREFILTERBYTYPE");
    
            var path = "/DataStoreItem/ByType/{Type}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Type" + "}", ApiClient.ParameterToString(type));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYTYPE: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYTYPE: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of DataStoreItem entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;DataStoreItem_&gt;</returns>
        public List<DataStoreItem_> SECUREPOLL (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL");
    
            var path = "/DataStoreItem/UpdatedSince";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (sinceTime != null) queryParams.Add("sinceTime", ApiClient.ParameterToString(sinceTime)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<DataStoreItem_>) ApiClient.Deserialize(response.Content, typeof(List<DataStoreItem_>), response.Headers);
        }
    
    }
}
