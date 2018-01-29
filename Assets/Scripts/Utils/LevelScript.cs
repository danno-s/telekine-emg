using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a level with a structure that stores batches of <see cref="Spawn"/> grouped by their spawn times.
/// Able to be reset to it's initial state.
/// </summary>
public class LevelScript {
  public delegate void ScriptCallback();
  private Stack<List<Spawn>> spawns, backup;
  public int level;

  /// <summary>
  /// Initializes a new instance of the <see cref="LevelScript"/> class by reading and XML file representing the level.
  /// </summary>
  /// <param name="filename">Filename.</param>
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
    
  /// <summary>
  /// If the script is over.
  /// </summary>
  /// <returns><c>true</c> if there are no more objects in the stack, otherwise, <c>false</c></returns>
  public bool Over() {
    return spawns.Count == 0;
  }

  /// <summary>
  /// Should the next batch be spawned.
  /// </summary>
  /// <returns><c>true</c>, if the next batch should be spawned, <c>false</c> otherwise.</returns>
  /// <param name="distance"></param>How far along the level the game is.</param>
  public virtual bool ShouldSpawn(float distance) {
    try {
      // all elements in the batch have the same distance
      return distance >= spawns.Peek()[0].distance;
    } catch {
      return false;
    }
  }

  /// <summary>
  /// Returns all the spawns in the current batch and prepares the next batch.
  /// </summary>
  /// <returns>The spawns.</returns>
  public virtual List<Spawn> PopSpawns() {
    return spawns.Pop();
  }

  /// <summary>
  /// Returns the script to it's initial state.
  /// </summary>
  public LevelScript Reset() {
    spawns = new Stack<List<Spawn>>(backup);
    return this;
  }
}