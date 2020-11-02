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
  public class JobSegmentHistory_ {
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
    /// Gets or Sets Seq
    /// </summary>
    [DataMember(Name="seq", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "seq")]
    public int? Seq { get; set; }

    /// <summary>
    /// Gets or Sets SegmentId
    /// </summary>
    [DataMember(Name="segmentId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "segmentId")]
    public string SegmentId { get; set; }

    /// <summary>
    /// Gets or Sets SegmentType
    /// </summary>
    [DataMember(Name="segmentType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "segmentType")]
    public string SegmentType { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or Sets SubStatus
    /// </summary>
    [DataMember(Name="subStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subStatus")]
    public string SubStatus { get; set; }

    /// <summary>
    /// Gets or Sets Job
    /// </summary>
    [DataMember(Name="job", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "job")]
    public string Job { get; set; }

    /// <summary>
    /// Gets or Sets Robot
    /// </summary>
    [DataMember(Name="robot", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "robot")]
    public string Robot { get; set; }

    /// <summary>
    /// Gets or Sets LinkedJobSegment
    /// </summary>
    [DataMember(Name="linkedJobSegment", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "linkedJobSegment")]
    public string LinkedJobSegment { get; set; }

    /// <summary>
    /// Gets or Sets GoalName
    /// </summary>
    [DataMember(Name="goalName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "goalName")]
    public string GoalName { get; set; }

    /// <summary>
    /// Gets or Sets Priority
    /// </summary>
    [DataMember(Name="priority", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// Gets or Sets CompletedTimestamp
    /// </summary>
    [DataMember(Name="completedTimestamp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "completedTimestamp")]
    public Timestamp_ CompletedTimestamp { get; set; }

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
      sb.Append("class JobSegmentHistory_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  Upd: ").Append(Upd).Append("\n");
      sb.Append("  Seq: ").Append(Seq).Append("\n");
      sb.Append("  SegmentId: ").Append(SegmentId).Append("\n");
      sb.Append("  SegmentType: ").Append(SegmentType).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  SubStatus: ").Append(SubStatus).Append("\n");
      sb.Append("  Job: ").Append(Job).Append("\n");
      sb.Append("  Robot: ").Append(Robot).Append("\n");
      sb.Append("  LinkedJobSegment: ").Append(LinkedJobSegment).Append("\n");
      sb.Append("  GoalName: ").Append(GoalName).Append("\n");
      sb.Append("  Priority: ").Append(Priority).Append("\n");
      sb.Append("  CompletedTimestamp: ").Append(CompletedTimestamp).Append("\n");
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
