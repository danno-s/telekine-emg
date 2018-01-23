using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleManager : NetworkBehaviour {

  public float height;
  public Parallax parallax;
  
  private float distance;

  // Use this for initialization
  void Start () {
    parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
  }
	
	// Update is called once per frame
	void Update () {

	}

  void OnDrawGizmosSelected() {
    Gizmos.DrawLine(transform.position + Vector3.up * height, transform.position - Vector3.up * height);
  }

  void SpawnObstacle(GameObject obstacle) {
    GameObject obj = Instantiate(obstacle, transform);
    NetworkServer.Spawn(obj);
    distance = 0;
  }
}
