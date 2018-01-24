using System.Collections.Generic;

interface Script {
  bool ShouldSpawn(float distance, LevelScript.ScriptCallback callback);

  List<Spawn> PopSpawns();
}