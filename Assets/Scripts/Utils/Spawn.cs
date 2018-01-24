using UnityEngine;

public class Spawn {
  public GameObject prefab;
  public float distance, height;

  public Spawn(string prefabName, float spawnDistance, float spawnHeight) {
    prefab = Resources.Load(prefabName) as GameObject;
    distance = spawnDistance;
    height = spawnHeight;
  }
}