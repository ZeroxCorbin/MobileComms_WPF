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
  public class JobRequestDetail_ {
    /// <summary>
    /// Gets or Sets Goal
    /// </summary>
    [DataMember(Name="goal", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "goal")]
    public string Goal { get; set; }

    /// <summary>
    /// Gets or Sets Priority
    /// </summary>
    [DataMember(Name="priority", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// Gets or Sets SegmentType
    /// </summary>
    [DataMember(Name="segmentType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "segmentType")]
    public string SegmentType { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class JobRequestDetail_ {\n");
      sb.Append("  Goal: ").Append(Goal).Append("\n");
      sb.Append("  Priority: ").Append(Priority).Append("\n");
      sb.Append("  SegmentType: ").Append(SegmentType).Append("\n");
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
