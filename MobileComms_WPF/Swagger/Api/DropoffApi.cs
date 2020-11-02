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
    public interface IDropoffApi
    {
        /// <summary>
        /// Get Dropoff by ID 
        /// </summary>
        /// <param name="id">Dropoff Id</param>
        /// <returns>Dropoff_</returns>
        Dropoff_ GET3 (string id);
        /// <summary>
        /// Create Dropoff 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse POST1 (Dropoff_ body);
        /// <summary>
        /// Delete Dropoff by ID 
        /// </summary>
        /// <param name="id">Dropoff Id</param>
        /// <returns>Dropoff_</returns>
        Dropoff_ SECUREDELETE (string id);
        /// <summary>
        /// Get a list of Dropoff entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        List<Dropoff_> SECUREFILTERBYASSIGNEDJOBID (string assignedJobId);
        /// <summary>
        /// Get a list of Dropoff entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        List<Dropoff_> SECUREFILTERBYJOBID (string jobId);
        /// <summary>
        /// Get a list of Dropoff entities filter by Robot 
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        List<Dropoff_> SECUREFILTERBYROBOT (string robot);
        /// <summary>
        /// Get a list of Dropoff entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        List<Dropoff_> SECUREFILTERBYSTATUS (string status);
        /// <summary>
        /// Get a list of Dropoff entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        List<Dropoff_> SECUREPOLL2 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class DropoffApi : IDropoffApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropoffApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public DropoffApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="DropoffApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DropoffApi(String basePath)
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
        /// Get Dropoff by ID 
        /// </summary>
        /// <param name="id">Dropoff Id</param>
        /// <returns>Dropoff_</returns>
        public Dropoff_ GET3 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET3");
    
            var path = "/Dropoff/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Dropoff_) ApiClient.Deserialize(response.Content, typeof(Dropoff_), response.Headers);
        }
    
        /// <summary>
        /// Create Dropoff 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse POST1 (Dropoff_ body)
        {
    
            var path = "/Dropoff";
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
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling POST1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling POST1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Delete Dropoff by ID 
        /// </summary>
        /// <param name="id">Dropoff Id</param>
        /// <returns>Dropoff_</returns>
        public Dropoff_ SECUREDELETE (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling SECUREDELETE");
    
            var path = "/Dropoff/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "basicAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Dropoff_) ApiClient.Deserialize(response.Content, typeof(Dropoff_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Dropoff entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        public List<Dropoff_> SECUREFILTERBYASSIGNEDJOBID (string assignedJobId)
        {
            // verify the required parameter 'assignedJobId' is set
            if (assignedJobId == null) throw new ApiException(400, "Missing required parameter 'assignedJobId' when calling SECUREFILTERBYASSIGNEDJOBID");
    
            var path = "/Dropoff/ByAssignedJobId/{AssignedJobId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "AssignedJobId" + "}", ApiClient.ParameterToString(assignedJobId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Dropoff_>) ApiClient.Deserialize(response.Content, typeof(List<Dropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Dropoff entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        public List<Dropoff_> SECUREFILTERBYJOBID (string jobId)
        {
            // verify the required parameter 'jobId' is set
            if (jobId == null) throw new ApiException(400, "Missing required parameter 'jobId' when calling SECUREFILTERBYJOBID");
    
            var path = "/Dropoff/ByJobId/{JobId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "JobId" + "}", ApiClient.ParameterToString(jobId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Dropoff_>) ApiClient.Deserialize(response.Content, typeof(List<Dropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Dropoff entities filter by Robot 
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        public List<Dropoff_> SECUREFILTERBYROBOT (string robot)
        {
            // verify the required parameter 'robot' is set
            if (robot == null) throw new ApiException(400, "Missing required parameter 'robot' when calling SECUREFILTERBYROBOT");
    
            var path = "/Dropoff/ByRobot/{Robot}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYROBOT: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYROBOT: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Dropoff_>) ApiClient.Deserialize(response.Content, typeof(List<Dropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Dropoff entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        public List<Dropoff_> SECUREFILTERBYSTATUS (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS");
    
            var path = "/Dropoff/ByStatus/{Status}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Dropoff_>) ApiClient.Deserialize(response.Content, typeof(List<Dropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Dropoff entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Dropoff_&gt;</returns>
        public List<Dropoff_> SECUREPOLL2 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL2");
    
            var path = "/Dropoff/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Dropoff_>) ApiClient.Deserialize(response.Content, typeof(List<Dropoff_>), response.Headers);
        }
    
    }
}
