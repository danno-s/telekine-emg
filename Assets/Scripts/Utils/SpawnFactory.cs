using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class intended to create spawn objects from their XML description.
/// </summary>
internal class SpawnFactory {
  public static List<Spawn> Create(IEnumerable<XElement> group) {
    var list = new List<Spawn>();
    foreach(var element in group) {
      list.Add(Create(element));
    }
    return list;
  }

  public static Spawn Create(XElement obj) {
    Spawn result;
    float distance, height;

    ParseField(obj, "Distance", out distance);
    ParseField(obj, "Height", out height);

    string prefab = obj.Element("Prefab").Value;

    if(prefab.StartsWith("Switch")) {
      var links =
        from target in obj.Descendants("Link")
        select Create(target);
      result = new SwitchSpawn(prefab, distance, height, links.ToList());
    } else if(prefab == "Gate") {
      var switches =
        from target in obj.Descendants("Switch")
        select Create(target);
      result = new GateSpawn(prefab, distance, height, switches.ToList());
      ((GateSpawn) result).SetAperture(float.Parse(obj.Element("Aperture").Value));
    } else {
      result = new Spawn(prefab, distance, height);
    }

    if(obj.Elements("Speed").Any())
      result.SetSpeed(float.Parse(obj.Element("Speed").Value));

    if(obj.HasAttributes)
      result.SetOffset(float.Parse(obj.Attribute("offset").Value));

    if(obj.Elements("VSpeed").Any()){
      result.SetVSpeed(float.Parse(obj.Element("VSpeed").Value));
      if(obj.Elements("VThreshold").Any())
        result.SetVThreshold(float.Parse(obj.Element("VThreshold").Value));
    }
    
    
    return result;
  }

  private static void ParseField(XElement obj, string field, out float value, float def = 0) {
    try {
      value = float.Parse(obj.Element(field).Value);
    } catch {
      value = def;
    }
  }
}