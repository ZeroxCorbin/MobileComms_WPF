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
    public interface IRobotFaultApi
    {
        /// <summary>
        /// Get RobotFault by ID 
        /// </summary>
        /// <param name="id">RobotFault Id</param>
        /// <returns>RobotFault_</returns>
        RobotFault_ GET13 (string id);
        /// <summary>
        /// Get a list of RobotFault entities filter by Active 
        /// </summary>
        /// <param name="active">Active</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        List<RobotFault_> SECUREFILTERBYACTIVE (string active);
        /// <summary>
        /// Get a list of RobotFault entities filter by Name 
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        List<RobotFault_> SECUREFILTERBYNAME (string name);
        /// <summary>
        /// Get a list of RobotFault entities filter by Robot 
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        List<RobotFault_> SECUREFILTERBYROBOT2 (string robot);
        /// <summary>
        /// Get a list of RobotFault entities filter by Type 
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        List<RobotFault_> SECUREFILTERBYTYPE1 (string type);
        /// <summary>
        /// Get a list of RobotFaultHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;RobotFaultHistory_&gt;</returns>
        List<RobotFaultHistory_> SECUREHISTORY2 (string sinceTime, string namekey);
        /// <summary>
        /// Get a list of RobotFault entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        List<RobotFault_> SECUREPOLL12 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class RobotFaultApi : IRobotFaultApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RobotFaultApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public RobotFaultApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="RobotFaultApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RobotFaultApi(String basePath)
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
        /// Get RobotFault by ID 
        /// </summary>
        /// <param name="id">RobotFault Id</param>
        /// <returns>RobotFault_</returns>
        public RobotFault_ GET13 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET13");
    
            var path = "/RobotFault/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET13: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET13: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RobotFault_) ApiClient.Deserialize(response.Content, typeof(RobotFault_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotFault entities filter by Active 
        /// </summary>
        /// <param name="active">Active</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        public List<RobotFault_> SECUREFILTERBYACTIVE (string active)
        {
            // verify the required parameter 'active' is set
            if (active == null) throw new ApiException(400, "Missing required parameter 'active' when calling SECUREFILTERBYACTIVE");
    
            var path = "/RobotFault/ByActive/{Active}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Active" + "}", ApiClient.ParameterToString(active));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYACTIVE: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYACTIVE: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotFault_>) ApiClient.Deserialize(response.Content, typeof(List<RobotFault_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotFault entities filter by Name 
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        public List<RobotFault_> SECUREFILTERBYNAME (string name)
        {
            // verify the required parameter 'name' is set
            if (name == null) throw new ApiException(400, "Missing required parameter 'name' when calling SECUREFILTERBYNAME");
    
            var path = "/RobotFault/ByName/{Name}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Name" + "}", ApiClient.ParameterToString(name));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYNAME: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYNAME: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotFault_>) ApiClient.Deserialize(response.Content, typeof(List<RobotFault_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotFault entities filter by Robot 
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        public List<RobotFault_> SECUREFILTERBYROBOT2 (string robot)
        {
            // verify the required parameter 'robot' is set
            if (robot == null) throw new ApiException(400, "Missing required parameter 'robot' when calling SECUREFILTERBYROBOT2");
    
            var path = "/RobotFault/ByRobot/{Robot}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Robot" + "}", ApiClient.ParameterToString(robot));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYROBOT2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYROBOT2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotFault_>) ApiClient.Deserialize(response.Content, typeof(List<RobotFault_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotFault entities filter by Type 
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        public List<RobotFault_> SECUREFILTERBYTYPE1 (string type)
        {
            // verify the required parameter 'type' is set
            if (type == null) throw new ApiException(400, "Missing required parameter 'type' when calling SECUREFILTERBYTYPE1");
    
            var path = "/RobotFault/ByType/{Type}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYTYPE1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYTYPE1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotFault_>) ApiClient.Deserialize(response.Content, typeof(List<RobotFault_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotFaultHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;RobotFaultHistory_&gt;</returns>
        public List<RobotFaultHistory_> SECUREHISTORY2 (string sinceTime, string namekey)
        {
    
            var path = "/RobotFault/History";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotFaultHistory_>) ApiClient.Deserialize(response.Content, typeof(List<RobotFaultHistory_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of RobotFault entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;RobotFault_&gt;</returns>
        public List<RobotFault_> SECUREPOLL12 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL12");
    
            var path = "/RobotFault/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL12: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL12: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RobotFault_>) ApiClient.Deserialize(response.Content, typeof(List<RobotFault_>), response.Headers);
        }
    
    }
}
