using System.Collections.Generic;

internal class NullScript : Script {
  public bool ShouldSpawn(float distance, LevelScript.ScriptCallback callback) { return false; }

  public List<Spawn> PopSpawns() { return new List<Spawn>(); }
}