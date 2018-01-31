using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Class representing an object to be spawned later. It contains when it should spawn and where.
/// </summary>
public class Spawn {
  public GameObject prefab;
  public float distance, height, offset, speed = 5, vSpeed, vThreshold = 16;

  public Spawn(string prefabName, float spawnDistance, float spawnHeight) {
    prefab = Resources.Load(prefabName) as GameObject;
    distance = spawnDistance;
    height = spawnHeight;
  }

  public void SetDistance(float newDistance) {
    distance = newDistance;
  }

  public void SetOffset(float offset) {
    this.offset = offset;
  }

  public void SetSpeed(float speed) {
    this.speed = speed;
  }

  public void SetVSpeed(float vSpeed) {
    this.vSpeed = vSpeed;
  }

  public void SetVThreshold(float threshold) {
    vThreshold = threshold;
  }

  /// <summary>
  /// Create the object and set its initial values.
  /// </summary>
  /// <param name="manager">The transform to attach this object to.</param>
  /// <param name="distance">Offset from the parent's transform to spawn at.</param>
  /// <returns>A List of the spawned objects</returns>
  public virtual List<GameObject> Activate(Transform manager, float distance) {
    var obj = GameObject.Instantiate(prefab, manager);
    var pos = obj.transform.position;
    pos.y = height;
    pos.x += offset + this.distance - distance;
    obj.transform.position = pos;

    var obstacle = obj.GetComponent<IObstacle>();
    obstacle.SetSpeed (speed);
    obstacle.SetVSpeed (vSpeed);
    obstacle.SetVThreshold (vThreshold);

    return new List<GameObject>() { obj };
  }

  /// <summary>
  /// Returns a <see cref="System.String"/> that represents the current <see cref="Spawn"/>.
  /// </summary>
  /// <returns>A <see cref="System.String"/> that represents the current <see cref="Spawn"/>.</returns>
  public override string ToString() {
    string offset = "";
    if(this.offset != 0)
      offset = "+" + this.offset;
    return prefab + "@" + distance + offset;
  }
}