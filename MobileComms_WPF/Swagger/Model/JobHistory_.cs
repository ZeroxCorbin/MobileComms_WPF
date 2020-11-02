using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class JobHistory_ {
    /// <summary>
    /// Gets or Sets Namekey
    /// </summary>
    [DataMember(Name="namekey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "namekey")]
    public string Namekey { get; set; }

    /// <summary>
    /// Gets or Sets Upd
    /// </summary>
    [DataMember(Name="upd", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "upd")]
    public Timestamp_ Upd { get; set; }

    /// <summary>
    /// Gets or Sets JobId
    /// </summary>
    [DataMember(Name="jobId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "jobId")]
    public string JobId { get; set; }

    /// <summary>
    /// Gets or Sets JobType
    /// </summary>
    [DataMember(Name="jobType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "jobType")]
    public string JobType { get; set; }

    /// <summary>
    /// Gets or Sets QueuedTimestamp
    /// </summary>
    [DataMember(Name="queuedTimestamp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "queuedTimestamp")]
    public Timestamp_ QueuedTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets CompletedTimestamp
    /// </summary>
    [DataMember(Name="completedTimestamp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "completedTimestamp")]
    public Timestamp_ CompletedTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or Sets LinkedJob
    /// </summary>
    [DataMember(Name="linkedJob", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "linkedJob")]
    public string LinkedJob { get; set; }

    /// <summary>
    /// Gets or Sets FailCount
    /// </summary>
    [DataMember(Name="failCount", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "failCount")]
    public int? FailCount { get; set; }

    /// <summary>
    /// Gets or Sets LastAssignedRobot
    /// </summary>
    [DataMember(Name="lastAssignedRobot", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "lastAssignedRobot")]
    public string LastAssignedRobot { get; set; }

    /// <summary>
    /// Gets or Sets CancelReason
    /// </summary>
    [DataMember(Name="cancelReason", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "cancelReason")]
    public string CancelReason { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class JobHistory_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  Upd: ").Append(Upd).Append("\n");
      sb.Append("  JobId: ").Append(JobId).Append("\n");
      sb.Append("  JobType: ").Append(JobType).Append("\n");
      sb.Append("  QueuedTimestamp: ").Append(QueuedTimestamp).Append("\n");
      sb.Append("  CompletedTimestamp: ").Append(CompletedTimestamp).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  LinkedJob: ").Append(LinkedJob).Append("\n");
      sb.Append("  FailCount: ").Append(FailCount).Append("\n");
      sb.Append("  LastAssignedRobot: ").Append(LastAssignedRobot).Append("\n");
      sb.Append("  CancelReason: ").Append(CancelReason).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
