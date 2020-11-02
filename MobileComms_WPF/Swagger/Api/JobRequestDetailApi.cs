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
    public interface IJobRequestDetailApi
    {
        /// <summary>
        /// Get JobRequestDetail by ID 
        /// </summary>
        /// <param name="id">JobRequestDetail Id</param>
        /// <returns>JobRequestDetail_</returns>
        JobRequestDetail_ GET7 (string id);
        /// <summary>
        /// Create JobRequestDetail 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse POST3 (JobRequestDetail_ body);
        /// <summary>
        /// Get a list of JobRequestDetail entities filter by JobRequest 
        /// </summary>
        /// <param name="jobRequest">JobRequest</param>
        /// <returns>List&lt;JobRequestDetail_&gt;</returns>
        List<JobRequestDetail_> SECUREFILTERBYJOBREQUEST (string jobRequest);
        /// <summary>
        /// Get a list of JobRequestDetail entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobRequestDetail_&gt;</returns>
        List<JobRequestDetail_> SECUREPOLL6 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class JobRequestDetailApi : IJobRequestDetailApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRequestDetailApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public JobRequestDetailApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRequestDetailApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JobRequestDetailApi(String basePath)
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
        /// Get JobRequestDetail by ID 
        /// </summary>
        /// <param name="id">JobRequestDetail Id</param>
        /// <returns>JobRequestDetail_</returns>
        public JobRequestDetail_ GET7 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET7");
    
            var path = "/JobRequestDetail/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET7: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET7: " + response.ErrorMessage, response.ErrorMessage);
    
            return (JobRequestDetail_) ApiClient.Deserialize(response.Content, typeof(JobRequestDetail_), response.Headers);
        }
    
        /// <summary>
        /// Create JobRequestDetail 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse POST3 (JobRequestDetail_ body)
        {
    
            var path = "/JobRequestDetail";
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
                throw new ApiException ((int)response.StatusCode, "Error calling POST3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling POST3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobRequestDetail entities filter by JobRequest 
        /// </summary>
        /// <param name="jobRequest">JobRequest</param>
        /// <returns>List&lt;JobRequestDetail_&gt;</returns>
        public List<JobRequestDetail_> SECUREFILTERBYJOBREQUEST (string jobRequest)
        {
            // verify the required parameter 'jobRequest' is set
            if (jobRequest == null) throw new ApiException(400, "Missing required parameter 'jobRequest' when calling SECUREFILTERBYJOBREQUEST");
    
            var path = "/JobRequestDetail/ByJobRequest/{JobRequest}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "JobRequest" + "}", ApiClient.ParameterToString(jobRequest));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBREQUEST: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBREQUEST: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobRequestDetail_>) ApiClient.Deserialize(response.Content, typeof(List<JobRequestDetail_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobRequestDetail entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobRequestDetail_&gt;</returns>
        public List<JobRequestDetail_> SECUREPOLL6 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL6");
    
            var path = "/JobRequestDetail/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL6: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL6: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobRequestDetail_>) ApiClient.Deserialize(response.Content, typeof(List<JobRequestDetail_>), response.Headers);
        }
    
    }
}
