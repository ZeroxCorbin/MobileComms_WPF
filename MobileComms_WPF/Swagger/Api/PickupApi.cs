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
    public interface IPickupApi
    {
        /// <summary>
        /// Get Pickup by ID 
        /// </summary>
        /// <param name="id">Pickup Id</param>
        /// <returns>Pickup_</returns>
        Pickup_ GET12 (string id);
        /// <summary>
        /// Create Pickup 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse POST7 (Pickup_ body);
        /// <summary>
        /// Delete Pickup by ID 
        /// </summary>
        /// <param name="id">Pickup Id</param>
        /// <returns>Pickup_</returns>
        Pickup_ SECUREDELETE5 (string id);
        /// <summary>
        /// Get a list of Pickup entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        List<Pickup_> SECUREFILTERBYASSIGNEDJOBID3 (string assignedJobId);
        /// <summary>
        /// Get a list of Pickup entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        List<Pickup_> SECUREFILTERBYJOBID4 (string jobId);
        /// <summary>
        /// Get a list of Pickup entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        List<Pickup_> SECUREFILTERBYSTATUS5 (string status);
        /// <summary>
        /// Get a list of Pickup entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        List<Pickup_> SECUREPOLL11 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class PickupApi : IPickupApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PickupApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public PickupApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PickupApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PickupApi(String basePath)
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
        /// Get Pickup by ID 
        /// </summary>
        /// <param name="id">Pickup Id</param>
        /// <returns>Pickup_</returns>
        public Pickup_ GET12 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET12");
    
            var path = "/Pickup/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET12: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET12: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Pickup_) ApiClient.Deserialize(response.Content, typeof(Pickup_), response.Headers);
        }
    
        /// <summary>
        /// Create Pickup 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse POST7 (Pickup_ body)
        {
    
            var path = "/Pickup";
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
                throw new ApiException ((int)response.StatusCode, "Error calling POST7: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling POST7: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Delete Pickup by ID 
        /// </summary>
        /// <param name="id">Pickup Id</param>
        /// <returns>Pickup_</returns>
        public Pickup_ SECUREDELETE5 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling SECUREDELETE5");
    
            var path = "/Pickup/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE5: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE5: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Pickup_) ApiClient.Deserialize(response.Content, typeof(Pickup_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Pickup entities filter by AssignedJobId 
        /// </summary>
        /// <param name="assignedJobId">AssignedJobId</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        public List<Pickup_> SECUREFILTERBYASSIGNEDJOBID3 (string assignedJobId)
        {
            // verify the required parameter 'assignedJobId' is set
            if (assignedJobId == null) throw new ApiException(400, "Missing required parameter 'assignedJobId' when calling SECUREFILTERBYASSIGNEDJOBID3");
    
            var path = "/Pickup/ByAssignedJobId/{AssignedJobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYASSIGNEDJOBID3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Pickup_>) ApiClient.Deserialize(response.Content, typeof(List<Pickup_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Pickup entities filter by JobId 
        /// </summary>
        /// <param name="jobId">JobId</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        public List<Pickup_> SECUREFILTERBYJOBID4 (string jobId)
        {
            // verify the required parameter 'jobId' is set
            if (jobId == null) throw new ApiException(400, "Missing required parameter 'jobId' when calling SECUREFILTERBYJOBID4");
    
            var path = "/Pickup/ByJobId/{JobId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID4: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYJOBID4: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Pickup_>) ApiClient.Deserialize(response.Content, typeof(List<Pickup_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Pickup entities filter by Status 
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        public List<Pickup_> SECUREFILTERBYSTATUS5 (string status)
        {
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling SECUREFILTERBYSTATUS5");
    
            var path = "/Pickup/ByStatus/{Status}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS5: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSTATUS5: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Pickup_>) ApiClient.Deserialize(response.Content, typeof(List<Pickup_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of Pickup entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;Pickup_&gt;</returns>
        public List<Pickup_> SECUREPOLL11 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL11");
    
            var path = "/Pickup/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL11: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL11: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<Pickup_>) ApiClient.Deserialize(response.Content, typeof(List<Pickup_>), response.Headers);
        }
    
    }
}
