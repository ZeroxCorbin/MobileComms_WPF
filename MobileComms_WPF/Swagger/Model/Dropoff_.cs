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
  public class Dropoff_ {
    /// <summary>
    /// Gets or Sets Namekey
    /// </summary>
    [DataMember(Name="namekey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "namekey")]
    public string Namekey { get; set; }

    /// <summary>
    /// Gets or Sets Robot
    /// </summary>
    [DataMember(Name="robot", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "robot")]
    public string Robot { get; set; }

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
    /// Gets or Sets JobId
    /// </summary>
    [DataMember(Name="jobId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "jobId")]
    public string JobId { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or Sets AssignedJobId
    /// </summary>
    [DataMember(Name="assignedJobId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "assignedJobId")]
    public string AssignedJobId { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Dropoff_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  Robot: ").Append(Robot).Append("\n");
      sb.Append("  Goal: ").Append(Goal).Append("\n");
      sb.Append("  Priority: ").Append(Priority).Append("\n");
      sb.Append("  JobId: ").Append(JobId).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  AssignedJobId: ").Append(AssignedJobId).Append("\n");
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
