using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

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

  public virtual List<GameObject> Activate(Transform manager) {
    var obj = GameObject.Instantiate(prefab, manager);
    var pos = obj.transform.position;
    pos.y = height;
    pos.x += offset;
    obj.transform.position = pos;

    obj.GetComponent<IObstacle>().SetSpeed(speed);

    return new List<GameObject>() { obj };
  }

  public override string ToString() {
    string offset = "";
    if(this.offset != 0)
      offset = "+" + this.offset;
    return prefab + "@" + distance + offset;
  }
}