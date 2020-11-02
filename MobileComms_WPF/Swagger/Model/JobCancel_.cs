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
  public class JobCancel_ {
    /// <summary>
    /// Gets or Sets Namekey
    /// </summary>
    [DataMember(Name="namekey", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "namekey")]
    public string Namekey { get; set; }

    /// <summary>
    /// Gets or Sets CancelType
    /// </summary>
    [DataMember(Name="cancelType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "cancelType")]
    public string CancelType { get; set; }

    /// <summary>
    /// Gets or Sets CancelValue
    /// </summary>
    [DataMember(Name="cancelValue", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "cancelValue")]
    public string CancelValue { get; set; }

    /// <summary>
    /// Gets or Sets CancelReason
    /// </summary>
    [DataMember(Name="cancelReason", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "cancelReason")]
    public string CancelReason { get; set; }

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
      sb.Append("class JobCancel_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  CancelType: ").Append(CancelType).Append("\n");
      sb.Append("  CancelValue: ").Append(CancelValue).Append("\n");
      sb.Append("  CancelReason: ").Append(CancelReason).Append("\n");
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
