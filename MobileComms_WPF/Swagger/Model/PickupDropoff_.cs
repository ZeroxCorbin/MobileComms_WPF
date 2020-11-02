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
  public class PickupDropoff_ {
    /// <summary>
    /// Gets or Sets Namekey
    /// </summary>
    [DataMember(Name="namekey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "namekey")]
    public string Namekey { get; set; }

    /// <summary>
    /// Gets or Sets PickupGoal
    /// </summary>
    [DataMember(Name="pickupGoal", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "pickupGoal")]
    public string PickupGoal { get; set; }

    /// <summary>
    /// Gets or Sets PickupPriority
    /// </summary>
    [DataMember(Name="pickupPriority", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "pickupPriority")]
    public int? PickupPriority { get; set; }

    /// <summary>
    /// Gets or Sets DropoffGoal
    /// </summary>
    [DataMember(Name="dropoffGoal", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dropoffGoal")]
    public string DropoffGoal { get; set; }

    /// <summary>
    /// Gets or Sets DropoffPriority
    /// </summary>
    [DataMember(Name="dropoffPriority", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dropoffPriority")]
    public int? DropoffPriority { get; set; }

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
      sb.Append("class PickupDropoff_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  PickupGoal: ").Append(PickupGoal).Append("\n");
      sb.Append("  PickupPriority: ").Append(PickupPriority).Append("\n");
      sb.Append("  DropoffGoal: ").Append(DropoffGoal).Append("\n");
      sb.Append("  DropoffPriority: ").Append(DropoffPriority).Append("\n");
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
