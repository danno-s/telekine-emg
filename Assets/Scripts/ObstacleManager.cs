using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleManager : NetworkBehaviour {

  public float height;
  public List<GameObject> pipes, switches, gaps;
  public Parallax parallax;

  [SyncVar]
  public float speed;
  private float distance;

  // Use this for initialization
  void Start () {
    parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
  }
	
	// Update is called once per frame
	void Update () {
    // Spawn new obstacles
    if(distance > 10) {
      SpawnObstacle(GetRandomObstacle());
    }

    distance += speed * Time.deltaTime;

    // Move old obstacles
    foreach(Transform child in transform) {
      if(child.GetComponent<IObstacle>().CanBeDestroyed()) {
        Destroy(child.gameObject);
      }
    }
	}

  private GameObject GetRandomObstacle() {
    List<GameObject> obstacleType;
    float r = UnityEngine.Random.value;
    if(r < .5) {
      obstacleType = switches;
    } else if(r < .9) {
      obstacleType = gaps;
    } else {
      obstacleType = pipes;
    }
    return Utils.GetRandomFromList(obstacleType);
  }

  void OnDrawGizmosSelected() {
    Gizmos.DrawLine(transform.position + Vector3.up * height, transform.position - Vector3.up * height);
  }

  void SpawnObstacle(GameObject obstacle) {
    GameObject obj = Instantiate(obstacle, transform);
    NetworkServer.Spawn(obj);
    distance = 0;
  }

  void IncreaseSpeed() {
    speed += 0.1f / speed;
    parallax.UpdateSpeed(speed);
  }
}
