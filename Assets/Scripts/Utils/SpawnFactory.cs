using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
    DefaultParse(obj, out distance, out height);
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
    } else {
      result = new Spawn(prefab, distance, height);
    }

    if(obj.HasAttributes) {
      result.SetOffset(float.Parse(obj.Attribute("offset").Value));
    }

    if(obj.Elements("Speed").Any()) {
      result.SetSpeed(float.Parse(obj.Element("Speed").Value));
    }

    return result;
  }

  private static void DefaultParse(XElement obj, out float distance, out float height, float def = 0) {
    try {
      distance = float.Parse(obj.Element("Distance").Value);
    } catch {
      distance = def;
    }

    try {
      height = float.Parse(obj.Element("Height").Value);
    } catch {
      height = def;
    }
  }
}