
internal class NullScript : Script {
  public bool ShouldSpawn(float distance, LevelScript.ScriptCallback callback) { return false; }

  public Spawn PopSpawn() { return null; }
}