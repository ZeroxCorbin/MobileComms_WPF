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
    public interface ISubscriptionConfigApi
    {
        /// <summary>
        /// Get SubscriptionConfig by ID 
        /// </summary>
        /// <param name="id">SubscriptionConfig Id</param>
        /// <returns>SubscriptionConfig_</returns>
        SubscriptionConfig_ GET15 (string id);
        /// <summary>
        /// Update SubscriptionConfig 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse PUT (SubscriptionConfig_ body);
        /// <summary>
        /// Get a list of SubscriptionConfig entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;SubscriptionConfig_&gt;</returns>
        List<SubscriptionConfig_> SECUREPOLL14 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class SubscriptionConfigApi : ISubscriptionConfigApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionConfigApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public SubscriptionConfigApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionConfigApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SubscriptionConfigApi(String basePath)
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
        /// Get SubscriptionConfig by ID 
        /// </summary>
        /// <param name="id">SubscriptionConfig Id</param>
        /// <returns>SubscriptionConfig_</returns>
        public SubscriptionConfig_ GET15 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET15");
    
            var path = "/SubscriptionConfig/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET15: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET15: " + response.ErrorMessage, response.ErrorMessage);
    
            return (SubscriptionConfig_) ApiClient.Deserialize(response.Content, typeof(SubscriptionConfig_), response.Headers);
        }
    
        /// <summary>
        /// Update SubscriptionConfig 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse PUT (SubscriptionConfig_ body)
        {
    
            var path = "/SubscriptionConfig";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(body); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling PUT: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling PUT: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Get a list of SubscriptionConfig entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;SubscriptionConfig_&gt;</returns>
        public List<SubscriptionConfig_> SECUREPOLL14 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL14");
    
            var path = "/SubscriptionConfig/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL14: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL14: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<SubscriptionConfig_>) ApiClient.Deserialize(response.Content, typeof(List<SubscriptionConfig_>), response.Headers);
        }
    
    }
}
