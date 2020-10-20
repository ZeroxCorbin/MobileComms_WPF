using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.IntegrationToolkit
{
    public static class RabbitMQ
    {
        public static List<string> InboundQueues { get; } = new List<string>()
        {
            "inbound.Dropoff",
            "inbound.JobCancel",
            "inbound.JobRequest",
            "inbound.JobSegmentModify",
            "inbound.Pickup",
            "inbound.PickupDropoff",
            "inbound.SubscriptionConfig",
        };
        public static List<string> OutboundQueues { get; } = new List<string>()
        {
            "outbound.DataStoreValue",
            "outbound.datastore.X (X is any subscribed DataStoreValue)",
            "outbound.Dropoff",
            "outbound.Job",
            "outbound.JobCancel",
            "outbound.JobRequest",
            "outbound.JobSegment",
            "outbound.JobSegmentModify",
            "outbound.Pickup",
            "outbound.PickupDropoff",
            "outbound.Robot",
            "outbound.RobotFault",
            "outbound.SubscriptionConfig",
        };
    }
}
