using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Class representing an object to be spawned later. It contains when it should spawn and where.
/// </summary>
public class Spawn {
  public GameObject prefab;
  public float distance, height, offset, speed = 5;

  public Spawn(string prefabName, float spawnDistance, float spawnHeight) {
    prefab = Resources.Load(prefabName) as GameObject;
    distance = spawnDistance;
    height = spawnHeight;
  }

  public void SetOffset(float offset) {
    this.offset = offset;
  }

  public void SetSpeed(float speed) {
    this.speed = speed;
  }

  /// <summary>
  /// Create the object and set its initial values.
  /// </summary>
  /// <param name="manager">Manager.</param>
  /// <returns>A List of the spawned objects</returns>
  public virtual List<GameObject> Activate(Transform manager) {
    var obj = GameObject.Instantiate(prefab, manager);
    var pos = obj.transform.position;
    pos.y = height;
    pos.x += offset;
    obj.transform.position = pos;

    obj.GetComponent<IObstacle>().SetSpeed(speed);

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