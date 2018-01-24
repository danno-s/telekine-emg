interface Script {
  bool ShouldSpawn(float distance, LevelScript.ScriptCallback callback);

  Spawn PopSpawn();
}