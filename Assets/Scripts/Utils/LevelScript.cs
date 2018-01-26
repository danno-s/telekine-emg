using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript {
  public delegate void ScriptCallback();
  private Stack<List<Spawn>> spawns, backup;
  public int level;

  public LevelScript(string filename) {
    spawns = new Stack<List<Spawn>>();
    TextAsset file = (TextAsset) Resources.Load("Level Scripts/" + filename, typeof(TextAsset));
    XElement level = XElement.Parse(file.text);

    this.level = int.Parse(level.Attribute("level").Value);

    var objects =
      from obj in level.Descendants("Object")
      let distance = float.Parse(obj.Element("Distance").Value)
      orderby distance descending
      group obj by distance into grp
      select SpawnFactory.Create(grp);

    foreach(var batch in objects) {
      spawns.Push(batch);
    }

    backup = new Stack<List<Spawn>>(spawns);
  }

  public bool Over() {
    return spawns.Count == 0;
  }

  public virtual bool ShouldSpawn(float distance) {
    try {
      // all elements in the batch have the same distance
      return distance >= spawns.Peek()[0].distance;
    } catch {
      return false;
    }
  }

  public virtual List<Spawn> PopSpawns() {
    return spawns.Pop();
  }

  public LevelScript Reset() {
    spawns = new Stack<List<Spawn>>(backup);
    return this;
  }
}