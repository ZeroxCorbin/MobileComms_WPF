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
    public interface IJobSegmentModifyApi
    {
        /// <summary>
        /// Get JobSegmentModify by ID 
        /// </summary>
        /// <param name="id">JobSegmentModify Id</param>
        /// <returns>JobSegmentModify_</returns>
        JobSegmentModify_ GET9 (string id);
        /// <summary>
        /// Create JobSegmentModify 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        RestResponse POST5 (JobSegmentModify_ body);
        /// <summary>
        /// Delete JobSegmentModify by ID 
        /// </summary>
        /// <param name="id">JobSegmentModify Id</param>
        /// <returns>JobSegmentModify_</returns>
        JobSegmentModify_ SECUREDELETE3 (string id);
        /// <summary>
        /// Get a list of JobSegmentModify entities filter by SegmentId 
        /// </summary>
        /// <param name="segmentId">SegmentId</param>
        /// <returns>List&lt;JobSegmentModify_&gt;</returns>
        List<JobSegmentModify_> SECUREFILTERBYSEGMENTID (string segmentId);
        /// <summary>
        /// Get a list of JobSegmentModify entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobSegmentModify_&gt;</returns>
        List<JobSegmentModify_> SECUREPOLL8 (string sinceTime);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class JobSegmentModifyApi : IJobSegmentModifyApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSegmentModifyApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public JobSegmentModifyApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSegmentModifyApi"/> class.
        /// </summary>
        /// <returns></returns>
        public JobSegmentModifyApi(String basePath)
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
        /// Get JobSegmentModify by ID 
        /// </summary>
        /// <param name="id">JobSegmentModify Id</param>
        /// <returns>JobSegmentModify_</returns>
        public JobSegmentModify_ GET9 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling GET9");
    
            var path = "/JobSegmentModify/ByKey/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling GET9: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GET9: " + response.ErrorMessage, response.ErrorMessage);
    
            return (JobSegmentModify_) ApiClient.Deserialize(response.Content, typeof(JobSegmentModify_), response.Headers);
        }
    
        /// <summary>
        /// Create JobSegmentModify 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>RestResponse</returns>
        public RestResponse POST5 (JobSegmentModify_ body)
        {
    
            var path = "/JobSegmentModify";
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
                throw new ApiException ((int)response.StatusCode, "Error calling POST5: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling POST5: " + response.ErrorMessage, response.ErrorMessage);
    
            return (RestResponse) ApiClient.Deserialize(response.Content, typeof(RestResponse), response.Headers);
        }
    
        /// <summary>
        /// Delete JobSegmentModify by ID 
        /// </summary>
        /// <param name="id">JobSegmentModify Id</param>
        /// <returns>JobSegmentModify_</returns>
        public JobSegmentModify_ SECUREDELETE3 (string id)
        {
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling SECUREDELETE3");
    
            var path = "/JobSegmentModify/{id}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE3: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREDELETE3: " + response.ErrorMessage, response.ErrorMessage);
    
            return (JobSegmentModify_) ApiClient.Deserialize(response.Content, typeof(JobSegmentModify_), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegmentModify entities filter by SegmentId 
        /// </summary>
        /// <param name="segmentId">SegmentId</param>
        /// <returns>List&lt;JobSegmentModify_&gt;</returns>
        public List<JobSegmentModify_> SECUREFILTERBYSEGMENTID (string segmentId)
        {
            // verify the required parameter 'segmentId' is set
            if (segmentId == null) throw new ApiException(400, "Missing required parameter 'segmentId' when calling SECUREFILTERBYSEGMENTID");
    
            var path = "/JobSegmentModify/BySegmentId/{SegmentId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "SegmentId" + "}", ApiClient.ParameterToString(segmentId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSEGMENTID: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREFILTERBYSEGMENTID: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegmentModify_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegmentModify_>), response.Headers);
        }
    
        /// <summary>
        /// Get a list of JobSegmentModify entities updates since the given time 
        /// </summary>
        /// <param name="sinceTime">Timestamp millis</param>
        /// <returns>List&lt;JobSegmentModify_&gt;</returns>
        public List<JobSegmentModify_> SECUREPOLL8 (string sinceTime)
        {
            // verify the required parameter 'sinceTime' is set
            if (sinceTime == null) throw new ApiException(400, "Missing required parameter 'sinceTime' when calling SECUREPOLL8");
    
            var path = "/JobSegmentModify/UpdatedSince";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL8: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SECUREPOLL8: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<JobSegmentModify_>) ApiClient.Deserialize(response.Content, typeof(List<JobSegmentModify_>), response.Headers);
        }
    
    }
}
