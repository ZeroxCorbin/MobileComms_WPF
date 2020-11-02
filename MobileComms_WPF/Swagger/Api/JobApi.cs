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
    public interface IJobApi
    {
        /// <summary>
        /// Get Job by ID 
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <returns>Job_</returns>
        Job_ GET6 (string id);
        /// <summary>
        /// Get a list of Job entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;Job_&gt;</returns>
        List<Job_> SECUREFILTERBYJOBID1 (string jobId);
        /// <summary>
        /// Get a list of Job entities filter by LastAssignedRobot 
        /// </summary>
        /// <param name="lastAssignedRobot">LastAssignedRobot</param>
        /// <returns>List&lt;Job_&gt;</returns>
        List<Job_> SECUREFILTERBYLASTASSIGNEDROBOT (string lastAssignedRobot);
        /// <summary>
        /// Get a list of Job entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Job_&gt;</returns>
        List<Job_> SECUREFILTERBYSTATUS1 (string status);
        /// <summary>
        /// Get a list of JobHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;JobHistory_&gt;</returns>
        List<JobHistory_> SECUREHISTORY (string sinceTime, string namekey);
        /// <summary>
        /// Get a list of Job entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Job_&gt;</returns>
        List<Job_> SECUREPOLL5 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class JobApi : IJobApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public JobApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="JobApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JobApi(String basePath)
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
        /// Get Job by ID 
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <returns>Job_</returns>
        public Job_ GET6 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET6");
    
            var path = "/Job/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET6: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET6: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Job_) ApiClient.Deserialize(response.Content, typeof(Job_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Job entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;Job_&gt;</returns>
        public List<Job_> SECUREFILTERBYJOBID1 (string jobId)
        {
            // verify the required parameter 'jobId' is set
            if (jobId == null) throw new ApiException(400, "Missing required parameter 'jobId' when calling SECUREFILTERBYJOBID1");
    
            var path = "/Job/ByJobId/{JobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Job_>) ApiClient.Deserialize(response.Content, typeof(List<Job_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Job entities filter by LastAssignedRobot 
        /// </summary>
        /// <param name="lastAssignedRobot">LastAssignedRobot</param>
        /// <returns>List&lt;Job_&gt;</returns>
        public List<Job_> SECUREFILTERBYLASTASSIGNEDROBOT (string lastAssignedRobot)
        {
            // verify the required parameter 'lastAssignedRobot' is set
            if (lastAssignedRobot == null) throw new ApiException(400, "Missing required parameter 'lastAssignedRobot' when calling SECUREFILTERBYLASTASSIGNEDROBOT");
    
            var path = "/Job/ByLastAssignedRobot/{LastAssignedRobot}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "LastAssignedRobot" + "}", ApiClient.ParameterToString(lastAssignedRobot));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYLASTASSIGNEDROBOT: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYLASTASSIGNEDROBOT: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Job_>) ApiClient.Deserialize(response.Content, typeof(List<Job_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Job entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Job_&gt;</returns>
        public List<Job_> SECUREFILTERBYSTATUS1 (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS1");
    
            var path = "/Job/ByStatus/{Status}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Job_>) ApiClient.Deserialize(response.Content, typeof(List<Job_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;JobHistory_&gt;</returns>
        public List<JobHistory_> SECUREHISTORY (string sinceTime, string namekey)
        {
    
            var path = "/Job/History";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobHistory_>) ApiClient.Deserialize(response.Content, typeof(List<JobHistory_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Job entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Job_&gt;</returns>
        public List<Job_> SECUREPOLL5 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL5");
    
            var path = "/Job/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL5: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL5: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Job_>) ApiClient.Deserialize(response.Content, typeof(List<Job_>), response.Headers);
        }
    
    }
}
