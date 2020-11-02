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
  public class DataStoreItem_ {
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
    /// Gets or Sets ItemId
    /// </summary>
    [DataMember(Name="itemId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "itemId")]
    public int? ItemId { get; set; }

    /// <summary>
    /// Gets or Sets Source
    /// </summary>
    [DataMember(Name="source", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "source")]
    public string Source { get; set; }

    /// <summary>
    /// Gets or Sets Category
    /// </summary>
    [DataMember(Name="category", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }

    /// <summary>
    /// Gets or Sets GroupName
    /// </summary>
    [DataMember(Name="groupName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "groupName")]
    public string GroupName { get; set; }

    /// <summary>
    /// Gets or Sets GroupDescr
    /// </summary>
    [DataMember(Name="groupDescr", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "groupDescr")]
    public string GroupDescr { get; set; }

    /// <summary>
    /// Gets or Sets DisplayName
    /// </summary>
    [DataMember(Name="displayName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "displayName")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or Sets ItemName
    /// </summary>
    [DataMember(Name="itemName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "itemName")]
    public string ItemName { get; set; }

    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or Sets Description
    /// </summary>
    [DataMember(Name="description", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DataStoreItem_ {\n");
      sb.Append("  Namekey: ").Append(Namekey).Append("\n");
      sb.Append("  Upd: ").Append(Upd).Append("\n");
      sb.Append("  ItemId: ").Append(ItemId).Append("\n");
      sb.Append("  Source: ").Append(Source).Append("\n");
      sb.Append("  Category: ").Append(Category).Append("\n");
      sb.Append("  GroupName: ").Append(GroupName).Append("\n");
      sb.Append("  GroupDescr: ").Append(GroupDescr).Append("\n");
      sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
      sb.Append("  ItemName: ").Append(ItemName).Append("\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  Description: ").Append(Description).Append("\n");
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
