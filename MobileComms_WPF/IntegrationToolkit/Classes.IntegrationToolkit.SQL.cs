﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.IntegrationToolkit
{
    public static class SQL
    {
        public enum QueryTypes
        {
            SELECT,
            UPDATE,
            INSERT
        }

        public static Dictionary<string, Dictionary<QueryTypes, string>> Views { get; } = new Dictionary<string, Dictionary<QueryTypes, string>>()
        {
            { "data_store_item_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "DataStoreItem_GET" } } },
            { "data_store_value_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "DataStoreValue_GET" } } },
            { "goal_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "" } } },

            { "job_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "" } } },
            { "job_segment_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "JobSegmentMonitoring_GET" } } },
            { "job_history_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "" } } },
            { "job_segment_history_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "" } } },

            { "job_request_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "JobRequest_POST" } } },
            { "job_request_detail_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "" } } },
            { "job_cancel_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "JobCancel_POST" } } },
            { "job_segment_modify_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "JobSegmentModify_POST" } } },

            { "pickup_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "Pickup_POST" } } },
            { "dropoff_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "Dropoff_POST" } } },
            { "pickup_dropoff_view", new Dictionary<QueryTypes, string>() { { QueryTypes.INSERT, "PickupDropoff_POST" } } },

            { "robot_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "Robot_GET" } } },
            { "robot_fault_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "RobotFault_GET" } } },
            { "robot_history_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "Robot_GET" } } },
            { "robot_fault_history_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "RobotFault_GET" } } },

            { "subscription_config_view", new Dictionary<QueryTypes, string>() { { QueryTypes.SELECT, "Subscription_GET" }, { QueryTypes.UPDATE, "Subscription_PUT" } } },
        };
    }
}