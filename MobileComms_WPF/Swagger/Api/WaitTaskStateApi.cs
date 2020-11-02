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
    public interface IWaitTaskStateApi
    {
        /// <summary>
        /// Get WaitTaskState by Robot Name 
        /// </summary>
        /// <param name="robot">Robot Name</param>
        /// <returns>WaitTaskStateQuery_</returns>
        WaitTaskStateQuery_ GET (string robot);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class WaitTaskStateApi : IWaitTaskStateApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaitTaskStateApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public WaitTaskStateApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="WaitTaskStateApi"/> class.
        /// </summary>
        /// <returns></returns>
        public WaitTaskStateApi(String basePath)
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
        /// Get WaitTaskState by Robot Name 
        /// </summary>
        /// <param name="robot">Robot Name</param>
        /// <returns>WaitTaskStateQuery_</returns>
        public WaitTaskStateQuery_ GET (string robot)
        {
            // verify the required parameter 'robot' is set
            if (robot == null) throw new ApiException(400, "Missing required parameter 'robot' when calling GET");
    
            var path = "/WaitTaskState/{robot}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "robot" + "}", ApiClient.ParameterToString(robot));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET: " + response.ErrorMessage, response.ErrorMessage);
    
            return (WaitTaskStateQuery_) ApiClient.Deserialize(response.Content, typeof(WaitTaskStateQuery_), response.Headers);
        }
    
    }
}
