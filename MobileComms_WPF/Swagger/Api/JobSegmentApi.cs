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
    public interface IJobSegmentApi
    {
        /// <summary>
        /// Get JobSegment by ID 
        /// </summary>
        /// <param name="id">JobSegment Id</param>
        /// <returns>JobSegment_</returns>
        JobSegment_ GET10 (string id);
        /// <summary>
        /// Get a list of JobSegment entities filter by GoalName 
        /// </summary>
        /// <param name="goalName">GoalName</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        List<JobSegment_> SECUREFILTERBYGOALNAME (string goalName);
        /// <summary>
        /// Get a list of JobSegment entities filter by Job 
        /// </summary>
        /// <param name="job">Job</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        List<JobSegment_> SECUREFILTERBYJOB (string job);
        /// <summary>
        /// Get a list of JobSegment entities filter by Robot 
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        List<JobSegment_> SECUREFILTERBYROBOT1 (string robot);
        /// <summary>
        /// Get a list of JobSegment entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        List<JobSegment_> SECUREFILTERBYSTATUS3 (string status);
        /// <summary>
        /// Get a list of JobSegmentHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;JobSegmentHistory_&gt;</returns>
        List<JobSegmentHistory_> SECUREHISTORY1 (string sinceTime, string namekey);
        /// <summary>
        /// Get a list of JobSegment entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        List<JobSegment_> SECUREPOLL9 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class JobSegmentApi : IJobSegmentApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSegmentApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public JobSegmentApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSegmentApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JobSegmentApi(String basePath)
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
        /// Get JobSegment by ID 
        /// </summary>
        /// <param name="id">JobSegment Id</param>
        /// <returns>JobSegment_</returns>
        public JobSegment_ GET10 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET10");
    
            var path = "/JobSegment/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET10: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET10: " + response.ErrorMessage, response.ErrorMessage);
    
            return (JobSegment_) ApiClient.Deserialize(response.Content, typeof(JobSegment_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegment entities filter by GoalName 
        /// </summary>
        /// <param name="goalName">GoalName</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        public List<JobSegment_> SECUREFILTERBYGOALNAME (string goalName)
        {
            // verify the required parameter 'goalName' is set
            if (goalName == null) throw new ApiException(400, "Missing required parameter 'goalName' when calling SECUREFILTERBYGOALNAME");
    
            var path = "/JobSegment/ByGoalName/{GoalName}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "GoalName" + "}", ApiClient.ParameterToString(goalName));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYGOALNAME: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYGOALNAME: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegment_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegment_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegment entities filter by Job 
        /// </summary>
        /// <param name="job">Job</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        public List<JobSegment_> SECUREFILTERBYJOB (string job)
        {
            // verify the required parameter 'job' is set
            if (job == null) throw new ApiException(400, "Missing required parameter 'job' when calling SECUREFILTERBYJOB");
    
            var path = "/JobSegment/ByJob/{Job}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "Job" + "}", ApiClient.ParameterToString(job));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOB: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOB: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegment_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegment_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegment entities filter by Robot 
        /// </summary>
        /// <param name="robot">Robot</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        public List<JobSegment_> SECUREFILTERBYROBOT1 (string robot)
        {
            // verify the required parameter 'robot' is set
            if (robot == null) throw new ApiException(400, "Missing required parameter 'robot' when calling SECUREFILTERBYROBOT1");
    
            var path = "/JobSegment/ByRobot/{Robot}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYROBOT1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYROBOT1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegment_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegment_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegment entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        public List<JobSegment_> SECUREFILTERBYSTATUS3 (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS3");
    
            var path = "/JobSegment/ByStatus/{Status}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegment_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegment_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegmentHistory entities 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <param name="namekey">Namekey of an Entity</param>
        /// <returns>List&lt;JobSegmentHistory_&gt;</returns>
        public List<JobSegmentHistory_> SECUREHISTORY1 (string sinceTime, string namekey)
        {
    
            var path = "/JobSegment/History";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY1: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREHISTORY1: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegmentHistory_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegmentHistory_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegment entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobSegment_&gt;</returns>
        public List<JobSegment_> SECUREPOLL9 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL9");
    
            var path = "/JobSegment/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL9: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL9: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegment_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegment_>), response.Headers);
        }
    
    }
}
