using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Classes.IntegrationToolkit
{
    public class REST
    {
        public enum REST_ACTIONS
        {
            GET,
            PUT,
            POST,
            DELETE
        }

        public static Dictionary<string, List<string>> Commands { get; } = new Dictionary<string, List<string>>()
        {
            {"DataStoreItem_GET", new List<string>()
        {
            "/DataStoreItem/ByKey/{namekey}",
            "/DataStoreItem/UpdatedSince?sinceTime={ms}",
            "/DataStoreItem/BySource/{Source}",
            "/DataStoreItem/ByItemName/{ItemName}",
            "/DataStoreItem/ByType/{Type}",
            "/DataStoreItem/ByDisplayName/{DisplayName}",
            "/DataStoreItem/ByGroupName/{GroupName}",
            "/DataStoreItem/ByCategory/{Category}"

        }},
            {"DataStoreValue_GET", new List<string>()
                {
                    "/DataStoreValue/ByKey/{namekey}",
                    "/DataStoreValue/UpdatedSince?sinceTime={ms}",
                    "/DataStoreValue/Stream",
                }},
            {"DataStoreValueLatest_GET", new List<string>()
                {
                    "/DataStoreValueLatest/{DataStore item name}",
                    "/DataStoreValueLatest/{DataStore item name}:{AMR name}",
                    "/DataStoreValueLatest/{DataStore item name}:*",
                }},

            {"Subscription_GET", new List<string>()
                {
                    "/SubscriptionConfig/ByKey/{namekey}",
                    "/SubscriptionConfig/UpdatedSince?sinceTime={ms}",
                    "/SubscriptionConfig/Stream",
                }},
            {"Subscription_PUT", new List<string>()
                {
                    "/SubscriptionConfig",
                }},

            {"Pickup_GET", new List<string>()
                {
                    "/Pickup/UpdatedSince?sinceTime={time millis}",
                    "/Pickup/Stream",
                    "/Pickup/ByKey/{namekey}",
                    "/Pickup/ByJobId/{JobId}",
                    "/Pickup/ByStatus/{Status}",
                    "/Pickup/ByAssignedJobId/{AssignedJobId}",
                }},
            {"Pickup_POST", new List<string>()
                {
                    "/Pickup",
                }},
            {"Pickup_DELETE", new List<string>()
                {
                    "/Pickup/{namekey}",
                }},

            {"PickupDropoff_GET", new List<string>()
                {
                    "/PickupDropoff/UpdatedSince?sinceTime={time millis}",
                    "/PickupDropoff/Stream",
                    "/PickupDropoff/ByKey/{namekey}",
                    "/PickupDropoff/ByJobId/{JobId}",
                    "/PickupDropoff/ByStatus/{Status}",
                    "/PickupDropoff/ByAssignedJobId/{AssignedJobId}",
                }},
            {"PickupDropoff_POST", new List<string>()
                {
                    "/PickupDropoff",
                }},
            {"PickupDropoff_DELETE", new List<string>()
                {
                    "/PickupDropoff/{namekey}",
                }},

            {"Dropoff_GET", new List<string>()
                {
                    "/Dropoff/UpdatedSince?sinceTime={time millis}",
                    "/Dropoff/Stream",
                    "/Dropoff/ByKey/{namekey}",
                    "/Dropoff/ByJobId/{JobId}",
                    "/Dropoff/ByStatus/{Status}",
                    "/Dropoff/ByAssignedJobId/{AssignedJobId}",
                    "/Dropoff/ByRobot/{AMR}"
                }},
            {"Dropoff_POST", new List<string>()
                {
                    "/Dropoff",
                }},

            {"JobRequest_GET", new List<string>()
                {
                    "/JobRequest/UpdatedSince?sinceTime={time millis}",
                    "/JobRequest/Stream",
                    "/JobRequest/ByKey/{namekey}",
                    "/JobRequest/ByJobId/{JobId}",
                    "/JobRequest/ByStatus/{Status}",
                    "/JobRequest/ByAssignedJobId/{AssignedJobId}",
                }},
            {"JobRequest_POST", new List<string>()
                {
                    "/JobRequest",
                }},
            {"JobRequest_DELETE", new List<string>()
                {
                    "/JobRequest/{namekey}",
                }},

            {"JobRequestDetail_GET", new List<string>()
                {
                    "/JobRequestDetail/ByKey/{namekey}",
                    "/JobRequestDetail/UpdatedSince?sinceTime={time millis}",
                    "/JobRequestDetail/ByJobRequest/{JobRequestnamekey}",
                }},

            {"JobMonitoring_GET", new List<string>()
                {
                    "/Job/ByKey/{namekey}",
                    "/Job/UpdatedSince?sinceTime={time millis}",
                    "/Job/Stream",
                    "/Job/History?sinceTime={time millis}",
                    "/Job/History?sinceTime={time millis}&namekey={namekey}",
                    "/Job/ByJobId/{JobId}",
                    "/Job/ByLastAssignedRobot/{LastAssignedRobot}",
                    "/Job/ByStatus/{Status}",
                }},
            {"JobSegmentMonitoring_GET", new List<string>()
                {
                    "/JobSegment/ByKey/{namekey}",
                    "/JobSegment/UpdatedSince?sinceTime={time millis}",
                    "/JobSegment/Stream",
                    "/JobSegment/History?sinceTime={time millis}",
                    "/JobSegment/History?sinceTime={time millis}&namekey={namekey}",
                    "/JobSegment/ByStatus/{Status}",
                    "/JobSegment/ByJob/{Job}",
                    "/JobSegment/ByRobot/{AMR}",
                    "/JobSegment/ByGoalName/{GoalName}"
                }},

            {"JobSegmentModify_GET", new List<string>()
                {
                    "/JobSegmentModify/ByKey/{namekey}",
                    "/JobSegmentModify/History?sinceTime={time millis}",
                    "/JobSegmentModify/BySegmentId/{SegmentID}",
                }},
            {"JobSegmentModify_POST", new List<string>()
                {
                    "/JobSegmentModify",
                }},
            {"JobSegmentModify_DELETE", new List<string>()
                {
                    "/JobSegmentModify/{namekey}",
                }},

            {"JobCancel_GET", new List<string>()
                {
                    "/JobCancel/ByKey/{namekey}",
                    "/JobCancel/UpdatedSince?sinceTime={time millis}",
                }},
            {"JobCancel_POST", new List<string>()
                {
                    "/JobCancel",
                }},
            {"JobCancel_DELETE", new List<string>()
                {
                    "/JobCancel/{namekey}",
                }},

            {"Robot_GET", new List<string>()
                {
                    "/Robot/ByKey/{namekey}",
                    "/Robot/History?sinceTime={time millis}",
                    "/Robot/History?sinceTime={time millis}&namekey={namekey}",
                    "/Robot/ByStatus/{Status}",
                    "/Robot/BySubStatus/{SubStatus}",
                    "/Robot/UpdatedSince?sinceTime={time millis}"
                }},
            {"RobotFault_GET", new List<string>()
                {
                    "/RobotFault/ByKey/{namekey}",
                    "/RobotFault/UpdatedSince?sinceTime={time millis}",
                    "/RobotFault/History?sinceTime={time millis}",
                    "/RobotFault/History?sinceTime={time millis}&namekey={namekey}",
                    "/RobotFault/ByRobot/{AMR}",
                    "/RobotFault/ByType/{Type}",
                    "/RobotFault/ByName/{Name}",
                    "/RobotFault/ByActive/{Value}"
                }
            }
        };


        public static string UserName => "toolkitadmin";
        public static string ConnectionString(string host) => $"https://{host}:8443";
        public static string  ConnectionString(string host, string resource) => $"https://{host}:8443{resource}";

        public static bool IsException { get; private set; }
        public static Exception RESTException { get; private set; }

        public static string RESTPost(string url, string pass, string jSONData)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender1, certificate, chain, sslPolicyErrors) => true;

            byte[] cred = UTF8Encoding.UTF8.GetBytes($"{UserName}:{pass}");
            using HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(jSONData, UTF8Encoding.UTF8, "application/json");
            try
            {
                return client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback -= (sender1, certificate, chain, sslPolicyErrors) => true;
            }

        }
        public static string RESTPut(string url, string pass, string jSONData)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender1, certificate, chain, sslPolicyErrors) => true;

            byte[] cred = UTF8Encoding.UTF8.GetBytes($"{UserName}:{pass}");
            using HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(jSONData, UTF8Encoding.UTF8, "application/json");
            try
            {
                return client.PutAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback -= (sender1, certificate, chain, sslPolicyErrors) => true;
            }

        }
        public static string RESTDelete(string url, string pass)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender1, certificate, chain, sslPolicyErrors) => true;

            byte[] cred = UTF8Encoding.UTF8.GetBytes($"{UserName}:{pass}");
            using HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                return client.DeleteAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback -= (sender1, certificate, chain, sslPolicyErrors) => true;
            }

        }
        public static string RESTGet(string url, string pass)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender1, certificate, chain, sslPolicyErrors) => true;
            
            byte[] cred = UTF8Encoding.UTF8.GetBytes($"{UserName}:{pass}");
            using HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback -= (sender1, certificate, chain, sslPolicyErrors) => true;
            }
        }
        public static Stream RESTStream(string url, string pass)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender1, certificate, chain, sslPolicyErrors) => true;

            byte[] cred = UTF8Encoding.UTF8.GetBytes($"{UserName}:{pass}");
            using HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));

            try
            {
                return client.GetStreamAsync(url).Result;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback -= (sender1, certificate, chain, sslPolicyErrors) => true;
            }


        }

    }
}
