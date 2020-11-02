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
  public class RobotFault_ {
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
    /// Gets or Sets Robot
    /// </summary>
    [DataMember(Name="robot", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "robot")]
    public string Robot { get; set; }

    /// <summary>
    /// Gets or Sets Active
    /// </summary>
    [DataMember(Name="active", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "active")]
    public bool? Active { get; set; }

    /// <summary>
    /// Gets or Sets BlockDriving
    /// </summary>
    [DataMember(Name="blockDriving", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "blockDriving")]
    public bool? BlockDriving { get; set; }

    /// <summary>
    /// Gets or Sets Driving
    /// </summary>
    [DataMember(Name="driving", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "driving")]
    public bool? Driving { get; set; }

    /// <summary>
    /// Gets or Sets Critical
    /// </summary>
    [DataMember(Name="critical", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "critical")]
    public bool? Critical { get; set; }

    /// <summary>
    /// Gets or Sets ClearedOnGo
    /// </summary>
    [DataMember(Name="clearedOnGo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "clearedOnGo")]
    public bool? ClearedOnGo { get; set; }

    /// <summary>
    /// Gets or Sets ClearedOnAck
    /// </summary>
    [DataMember(Name="clearedOnAck", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "clearedOnAck")]
    public bool? ClearedOnAck { get; set; }

    /// <summary>
    /// Gets or Sets Application
    /// </summary>
    [DataMember(Name="application", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "application")]
    public bool? Application { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or Sets ShortDescription
    /// </summary>
    [DataMember(Name="shortDescription", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "shortDescription")]
    public string ShortDescription { get; set; }

    /// <summary>
    /// Gets or Sets LongDescription
    /// </summary>
    [DataMember(Name="longDescription", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "longDescription")]
    public string LongDescription { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class RobotFault_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  Upd: ").Append(Upd).Append("\n");
      sb.Append("  Robot: ").Append(Robot).Append("\n");
      sb.Append("  Active: ").Append(Active).Append("\n");
      sb.Append("  BlockDriving: ").Append(BlockDriving).Append("\n");
      sb.Append("  Driving: ").Append(Driving).Append("\n");
      sb.Append("  Critical: ").Append(Critical).Append("\n");
      sb.Append("  ClearedOnGo: ").Append(ClearedOnGo).Append("\n");
      sb.Append("  ClearedOnAck: ").Append(ClearedOnAck).Append("\n");
      sb.Append("  Application: ").Append(Application).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  ShortDescription: ").Append(ShortDescription).Append("\n");
      sb.Append("  LongDescription: ").Append(LongDescription).Append("\n");
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
