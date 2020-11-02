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
    public interface IPickupDropoffApi
    {
        /// <summary>
        /// Get PickupDropoff by ID 
        /// </summary>
        /// <param name="id">PickupDropoff Id</param>
        /// <returns>PickupDropoff_</returns>
        PickupDropoff_ GET11 (string id);
        /// <summary>
        /// Create PickupDropoff 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse POST6 (PickupDropoff_ body);
        /// <summary>
        /// Delete PickupDropoff by ID 
        /// </summary>
        /// <param name="id">PickupDropoff Id</param>
        /// <returns>PickupDropoff_</returns>
        PickupDropoff_ SECUREDELETE4 (string id);
        /// <summary>
        /// Get a list of PickupDropoff entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        List<PickupDropoff_> SECUREFILTERBYASSIGNEDJOBID2 (string assignedJobId);
        /// <summary>
        /// Get a list of PickupDropoff entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        List<PickupDropoff_> SECUREFILTERBYJOBID3 (string jobId);
        /// <summary>
        /// Get a list of PickupDropoff entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        List<PickupDropoff_> SECUREFILTERBYSTATUS4 (string status);
        /// <summary>
        /// Get a list of PickupDropoff entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        List<PickupDropoff_> SECUREPOLL10 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class PickupDropoffApi : IPickupDropoffApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PickupDropoffApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public PickupDropoffApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PickupDropoffApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PickupDropoffApi(String basePath)
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
        /// Get PickupDropoff by ID 
        /// </summary>
        /// <param name="id">PickupDropoff Id</param>
        /// <returns>PickupDropoff_</returns>
        public PickupDropoff_ GET11 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET11");
    
            var path = "/PickupDropoff/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET11: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET11: " + response.ErrorMessage, response.ErrorMessage);
    
            return (PickupDropoff_) ApiClient.Deserialize(response.Content, typeof(PickupDropoff_), response.Headers);
        }
    
        /// <summary>
        /// Create PickupDropoff 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse POST6 (PickupDropoff_ body)
        {
    
            var path = "/PickupDropoff";
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
                throw new ApiException ((int)response.StatusCode, "Error calling POST6: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling POST6: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Delete PickupDropoff by ID 
        /// </summary>
        /// <param name="id">PickupDropoff Id</param>
        /// <returns>PickupDropoff_</returns>
        public PickupDropoff_ SECUREDELETE4 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling SECUREDELETE4");
    
            var path = "/PickupDropoff/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE4: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE4: " + response.ErrorMessage, response.ErrorMessage);
    
            return (PickupDropoff_) ApiClient.Deserialize(response.Content, typeof(PickupDropoff_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of PickupDropoff entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        public List<PickupDropoff_> SECUREFILTERBYASSIGNEDJOBID2 (string assignedJobId)
        {
            // verify the required parameter 'assignedJobId' is set
            if (assignedJobId == null) throw new ApiException(400, "Missing required parameter 'assignedJobId' when calling SECUREFILTERBYASSIGNEDJOBID2");
    
            var path = "/PickupDropoff/ByAssignedJobId/{AssignedJobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID2: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID2: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<PickupDropoff_>) ApiClient.Deserialize(response.Content, typeof(List<PickupDropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of PickupDropoff entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        public List<PickupDropoff_> SECUREFILTERBYJOBID3 (string jobId)
        {
            // verify the required parameter 'jobId' is set
            if (jobId == null) throw new ApiException(400, "Missing required parameter 'jobId' when calling SECUREFILTERBYJOBID3");
    
            var path = "/PickupDropoff/ByJobId/{JobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<PickupDropoff_>) ApiClient.Deserialize(response.Content, typeof(List<PickupDropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of PickupDropoff entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        public List<PickupDropoff_> SECUREFILTERBYSTATUS4 (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS4");
    
            var path = "/PickupDropoff/ByStatus/{Status}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS4: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS4: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<PickupDropoff_>) ApiClient.Deserialize(response.Content, typeof(List<PickupDropoff_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of PickupDropoff entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;PickupDropoff_&gt;</returns>
        public List<PickupDropoff_> SECUREPOLL10 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL10");
    
            var path = "/PickupDropoff/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL10: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL10: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<PickupDropoff_>) ApiClient.Deserialize(response.Content, typeof(List<PickupDropoff_>), response.Headers);
        }
    
    }
}
