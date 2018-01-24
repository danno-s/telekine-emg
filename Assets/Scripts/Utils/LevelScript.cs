using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

public class LevelScript : Script {
  public delegate void ScriptCallback();
  private Stack<Spawn> spawns;

  public LevelScript(string path) {
    spawns = new Stack<Spawn>();
    XElement level = XElement.Load(path);
    IEnumerable<Spawn> objects =
      from obj in level.Descendants("Object")
      let distance = float.Parse(obj.Element("Distance").Value)
      orderby distance descending
      select new Spawn(
          obj.Element("Prefab").Value,
          distance,
          float.Parse(obj.Element("Height").Value)
        );

    foreach(var spawn in objects)
      spawns.Push(spawn);
  }

  public virtual bool ShouldSpawn(float distance, ScriptCallback callback) {
    try {
      return distance >= spawns.Peek().distance;
    } catch {
      callback();
      return false;
    }
  }

  public virtual Spawn PopSpawn() {
    return spawns.Pop();
  }
}