using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.IntegrationToolkit.JSON_Types
{
    public class DataStoreItem_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public int itemId { get; set; }
        public string source { get; set; }
        public string category { get; set; }
        public string groupName { get; set; }
        public string groupDescr { get; set; }
        public string itemName { get; set; }
        public string displayName { get; set; }
        public string type { get; set; }
        public string description { get; set; }
    }
    public class DataStoreValue_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public string value { get; set; }
    }
    public class DataStoreValueLatest_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public string value { get; set; }
    }

    public class Subscription_GET
    {
        public string namekey { get; set; }
        public Audit audit { get; set; } = new Audit();
        public string subscriptionInterval { get; set; }
    }
    public class Subscription_PUT
    {
        public string namekey { get; set; }
        public string subscriptionInterval { get; set; }
    }

    public class Pickup_GET
    {
        public string namekey { get; set; }
        public Audit audit { get; set; } = new Audit();
        public string goal { get; set; }
        public string priority { get; set; }
        public string jobId { get; set; }
        public string status { get; set; }
        public string assignedJobId { get; set; }
    }
    public class Pickup_POST
    {
        public string namekey { get; set; }
        public string goal { get; set; }
        public int priority { get; set; }
        public string jobId { get; set; }
    }

    public class PickupDropoff_GET
    {
        public string namekey { get; set; }
        public string pickupGoal { get; set; }
        public int pickupPriority { get; set; }
        public string dropoffGoal { get; set; }
        public int dropoffPriority { get; set; }
        public string jobId { get; set; }
        public string status { get; set; }
        public string assignedJobId { get; set; }
    }
    public class PickupDropoff_POST
    {
        public string namekey { get; set; }
        public string pickupGoal { get; set; }
        public int pickupPriority { get; set; }
        public string dropoffGoal { get; set; }
        public int dropoffPriority { get; set; }
        public string jobId { get; set; }
    }

    public class Dropoff_GET
    {
        public string namekey { get; set; }
        public string robot { get; set; }
        public string goal { get; set; }
        public int priority { get; set; }
        public string jobId { get; set; }
        public string status { get; set; }
        public string assignedJobId { get; set; }
    }
    public class Dropoff_POST
    {
        public string namekey { get; set; }
        public string robot { get; set; }
        public string goal { get; set; }
        public int priority { get; set; }
        public string jobId { get; set; }
    }

    public class JobRequestDetail_GET
    {
        public JobRequestDetail_Segement_GET[] details { get; set; }
    }
    public class JobRequestDetail_Segement_GET
    {
        public string pickupGoal { get; set; }
        public int priority { get; set; }
        public string dropoffGoal { get; set; }
    }

    public class JobRequest_GET
    {
        public string namekey { get; set; }
        public Audit audit { get; set; } = new Audit();
        public string jobId { get; set; }
        public bool defaultPriority { get; set; }
        public JobRequest_Segment_GET[] details { get; set; }
        public string status { get; set; }
        public string assignedJobId { get; set; }
    }
    public class JobRequest_Segment_GET
    {
        public string segmentType { get; set; }
        public int priority { get; set; }
    }
    public class JobRequest_POST
    {
        public string namekey { get; set; }
        public string jobId { get; set; }
        public bool defaultPriority { get; set; }
        public JobRequest_Segment_POST[] details { get; set; }
    }
    public class JobRequest_Segment_POST
    {
        public string pickupGoal { get; set; }
        public int priority { get; set; }
        public string dropoffGoal { get; set; }
    }

    public class JobMonitoring_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public string jobId { get; set; }
        public string jobType { get; set; }
        public Timestamp queuedTimestamp { get; set; } = new Timestamp();
        public Timestamp completedTimestamp { get; set; } = new Timestamp();
        public string status { get; set; }
        public string linkedJob { get; set; }
        public int failCount { get; set; }
        public string lastAssignedRobot { get; set; }
        public string cancelReason { get; set; }
    }
    public class JobSegmentMonitoring_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public int seq { get; set; }
        public string segmentId { get; set; }
        public string segmentType { get; set; }
        public string status { get; set; }
        public string subStatus { get; set; }
        public string job { get; set; }
        public string robot { get; set; }
        public string linkedJobSegment { get; set; }
        public string goalName { get; set; }
        public int priority { get; set; }
        public Timestamp completedTimestamp { get; set; } = new Timestamp();
        public string cancelReason { get; set; }
    }

    public class JobSegmentModify_GET
    {
        public string namekey { get; set; }
        public Audit audit { get; set; } = new Audit();
        public string segmentId { get; set; }
        public string segmentNamekey { get; set; }
        public int priority { get; set; }
        public string goal { get; set; }
        public string status { get; set; }
    }
    public class JobSegmentModify_POST
    {
        public string segmentId { get; set; }
        public int priority { get; set; }
        public string goal { get; set; }
    }

    public class JobCancel_GET
    {
        public string namekey { get; set; }
        public string cancelType { get; set; }
        public string cancelValue { get; set; }
        public string cancelReason { get; set; }
        public string status { get; set; }
    }
    public class JobCancel_POST
    {
        public string namekey { get; set; }
        public string jobId { get; set; }
        public string cancelReason { get; set; }
    }

    public class Robot_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public string status { get; set; }
        public string subStatus { get; set; }
    }
    public class RobotFault_GET
    {
        public string namekey { get; set; }
        public Timestamp upd { get; set; } = new Timestamp();
        public string robot { get; set; }
        public bool active { get; set; }
        public bool blockDriving { get; set; }
        public bool driving { get; set; }
        public bool critical { get; set; }
        public bool clearedOnGo { get; set; }
        public bool clearedOnAck { get; set; }
        public bool application { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
    }

    public class Timestamp
    {
        public long millis { get; set; }
    }

    public class Audit
    {
        public string namekey { get; set; }
        public Timestamp crt { get; set; } = new Timestamp();
        public Timestamp upd { get; set; } = new Timestamp();
        public int ver { get; set; }
    }
}
