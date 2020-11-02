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
  public class JobSegmentModify_ {
    /// <summary>
    /// Gets or Sets Namekey
    /// </summary>
    [DataMember(Name="namekey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "namekey")]
    public string Namekey { get; set; }

    /// <summary>
    /// Gets or Sets SegmentId
    /// </summary>
    [DataMember(Name="segmentId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "segmentId")]
    public string SegmentId { get; set; }

    /// <summary>
    /// Gets or Sets SegmentNamekey
    /// </summary>
    [DataMember(Name="segmentNamekey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "segmentNamekey")]
    public string SegmentNamekey { get; set; }

    /// <summary>
    /// Gets or Sets Priority
    /// </summary>
    [DataMember(Name="priority", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// Gets or Sets Goal
    /// </summary>
    [DataMember(Name="goal", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "goal")]
    public string Goal { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class JobSegmentModify_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  SegmentId: ").Append(SegmentId).Append("\n");
      sb.Append("  SegmentNamekey: ").Append(SegmentNamekey).Append("\n");
      sb.Append("  Priority: ").Append(Priority).Append("\n");
      sb.Append("  Goal: ").Append(Goal).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
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
