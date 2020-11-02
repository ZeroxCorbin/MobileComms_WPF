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
    public interface IJobRequestApi
    {
        /// <summary>
        /// Get JobRequest by ID 
        /// </summary>
        /// <param name="id">JobRequest Id</param>
        /// <returns>JobRequest_</returns>
        JobRequest_ GET8 (string id);
        /// <summary>
        /// Create JobRequest 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse POST4 (JobRequest_ body);
        /// <summary>
        /// Delete JobRequest by ID 
        /// </summary>
        /// <param name="id">JobRequest Id</param>
        /// <returns>JobRequest_</returns>
        JobRequest_ SECUREDELETE2 (string id);
        /// <summary>
        /// Get a list of JobRequest entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        List<JobRequest_> SECUREFILTERBYASSIGNEDJOBID1 (string assignedJobId);
        /// <summary>
        /// Get a list of JobRequest entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        List<JobRequest_> SECUREFILTERBYJOBID2 (string jobId);
        /// <summary>
        /// Get a list of JobRequest entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        List<JobRequest_> SECUREFILTERBYSTATUS2 (string status);
        /// <summary>
        /// Get a list of JobRequest entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        List<JobRequest_> SECUREPOLL7 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class JobRequestApi : IJobRequestApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRequestApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public JobRequestApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRequestApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JobRequestApi(String basePath)
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
        /// Get JobRequest by ID 
        /// </summary>
        /// <param name="id">JobRequest Id</param>
        /// <returns>JobRequest_</returns>
        public JobRequest_ GET8 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET8");
    
            var path = "/JobRequest/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET8: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET8: " + response.ErrorMessage, response.ErrorMessage);
    
            return (JobRequest_) ApiClient.Deserialize(response.Content, typeof(JobRequest_), response.Headers);
        }
    
        /// <summary>
        /// Create JobRequest 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse POST4 (JobRequest_ body)
        {
    
            var path = "/JobRequest";
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
                throw new ApiException ((int)response.StatusCode, "Error calling POST4: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling POST4: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Delete JobRequest by ID 
        /// </summary>
        /// <param name="id">JobRequest Id</param>
        /// <returns>JobRequest_</returns>
        public JobRequest_ SECUREDELETE2 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling SECUREDELETE2");
    
            var path = "/JobRequest/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (JobRequest_) ApiClient.Deserialize(response.Content, typeof(JobRequest_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobRequest entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        public List<JobRequest_> SECUREFILTERBYASSIGNEDJOBID1 (string assignedJobId)
        {
            // verify the required parameter 'assignedJobId' is set
            if (assignedJobId == null) throw new ApiException(400, "Missing required parameter 'assignedJobId' when calling SECUREFILTERBYASSIGNEDJOBID1");
    
            var path = "/JobRequest/ByAssignedJobId/{AssignedJobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobRequest_>) ApiClient.Deserialize(response.Content, typeof(List<JobRequest_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobRequest entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        public List<JobRequest_> SECUREFILTERBYJOBID2 (string jobId)
        {
            // verify the required parameter 'jobId' is set
            if (jobId == null) throw new ApiException(400, "Missing required parameter 'jobId' when calling SECUREFILTERBYJOBID2");
    
            var path = "/JobRequest/ByJobId/{JobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobRequest_>) ApiClient.Deserialize(response.Content, typeof(List<JobRequest_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobRequest entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        public List<JobRequest_> SECUREFILTERBYSTATUS2 (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS2");
    
            var path = "/JobRequest/ByStatus/{Status}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobRequest_>) ApiClient.Deserialize(response.Content, typeof(List<JobRequest_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobRequest entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobRequest_&gt;</returns>
        public List<JobRequest_> SECUREPOLL7 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL7");
    
            var path = "/JobRequest/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL7: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL7: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobRequest_>) ApiClient.Deserialize(response.Content, typeof(List<JobRequest_>), response.Headers);
        }
    
    }
}
