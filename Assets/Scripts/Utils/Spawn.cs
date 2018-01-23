using UnityEngine;

public class Spawn {
  private GameObject prefab;
  private float distance;
  private Vector3 position;

  public Spawn(string prefabLocation, float spawnDistance, Vector3 position) {
    prefab = Resources.Load(prefabLocation) as GameObject;
    distance = spawnDistance;
    this.position = position;
  }
}