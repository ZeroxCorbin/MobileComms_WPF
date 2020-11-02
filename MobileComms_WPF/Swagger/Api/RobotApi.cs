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
    public interface IRobotApi
    {
        /// <summary>
        /// Get Robot by ID 
        /// </summary>
        /// <param name="id">Robot Id</param>
        /// <returns>Robot_</returns>
        Robot_ GET14 (string id);
        /// <summary>
        /// Get a list of Robot entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Robot_&gt;</returns>
        List<Robot_> SECUREFILTERBYSTATUS6 (string status);
        /// <summary>
        /// Get a list of Robot entities filter by SubStatus 
        /// </summary>
        /// <param name="subStatus">SubStatus</param>
        /// <returns>List&lt;Robot_&gt;</returns>
        List<Robot_> SECUREFILTERBYSUBSTATUS (string subStatus);
        /// <summary>
        /// Get a list of RobotHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;RobotHistory_&gt;</returns>
        List<RobotHistory_> SECUREHISTORY3 (string sinceTime, string namekey);
        /// <summary>
        /// Get a list of Robot entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Robot_&gt;</returns>
        List<Robot_> SECUREPOLL13 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class RobotApi : IRobotApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RobotApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public RobotApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="RobotApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RobotApi(String basePath)
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
        /// Get Robot by ID 
        /// </summary>
        /// <param name="id">Robot Id</param>
        /// <returns>Robot_</returns>
        public Robot_ GET14 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET14");
    
            var path = "/Robot/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET14: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET14: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Robot_) ApiClient.Deserialize(response.Content, typeof(Robot_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Robot entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Robot_&gt;</returns>
        public List<Robot_> SECUREFILTERBYSTATUS6 (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS6");
    
            var path = "/Robot/ByStatus/{Status}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Status" + "}", ApiClient.ParameterToString(status));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS6: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS6: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Robot_>) ApiClient.Deserialize(response.Content, typeof(List<Robot_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Robot entities filter by SubStatus 
        /// </summary>
        /// <param name="subStatus">SubStatus</param>
        /// <returns>List&lt;Robot_&gt;</returns>
        public List<Robot_> SECUREFILTERBYSUBSTATUS (string subStatus)
        {
            // verify the required parameter 'subStatus' is set
            if (subStatus == null) throw new ApiException(400, "Missing required parameter 'subStatus' when calling SECUREFILTERBYSUBSTATUS");
    
            var path = "/Robot/BySubStatus/{SubStatus}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "SubStatus" + "}", ApiClient.ParameterToString(subStatus));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSUBSTATUS: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSUBSTATUS: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Robot_>) ApiClient.Deserialize(response.Content, typeof(List<Robot_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;RobotHistory_&gt;</returns>
        public List<RobotHistory_> SECUREHISTORY3 (string sinceTime, string namekey)
        {
    
            var path = "/Robot/History";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (sinceTime != null) queryParams.Add("sinceTime", ApiClient.ParameterToString(sinceTime)); // query parameter
 if (namekey != null) queryParams.Add("namekey", ApiClient.ParameterToString(namekey)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotHistory_>) ApiClient.Deserialize(response.Content, typeof(List<RobotHistory_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Robot entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Robot_&gt;</returns>
        public List<Robot_> SECUREPOLL13 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL13");
    
            var path = "/Robot/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL13: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL13: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Robot_>) ApiClient.Deserialize(response.Content, typeof(List<Robot_>), response.Headers);
        }
    
    }
}
