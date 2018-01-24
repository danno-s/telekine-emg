using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : Script {
  public delegate void ScriptCallback();
  private Stack<List<Spawn>> spawns;

  public LevelScript(string path) {
    spawns = new Stack<List<Spawn>>();
    XElement level = XElement.Load(path);
    var objects =
      from obj in level.Descendants("Object")
      let distance = float.Parse(obj.Element("Distance").Value)
      orderby distance descending
      group obj by distance into grp
      select SpawnFactory.Create(grp);

    foreach(var batch in objects) {
      spawns.Push(batch);
    }
  }

  public virtual bool ShouldSpawn(float distance, ScriptCallback callback) {
    try {
      // all elements in the batch have the same distance
      return distance >= spawns.Peek()[0].distance;
    } catch {
      callback();
      return false;
    }
  }

  public virtual List<Spawn> PopSpawns() {
    return spawns.Pop();
  }
}