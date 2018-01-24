﻿using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleManager : NetworkBehaviour {

  public float height, speed;
  public Parallax parallax;

  private float distance;
  public List<string> levels;
  private Script script;

  // Use this for initialization
  void Start () {
    parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
    script = new LevelScript("Assets/Scenes/Level Scripts/" + levels[0]);
  }
	
	// Update is called once per frame
	void Update () {
    distance += speed * Time.deltaTime;

    // Read script
    if(script.ShouldSpawn(distance, ChangeScripts)) {
      SpawnObstacles(script.PopSpawns());
    }
	}

  private void ChangeScripts() {
    levels.RemoveAt(0);
    try {
      script = new LevelScript("Assets/Scenes/Level Scripts/" + levels[0]);
    } catch {
      script = new NullScript();
    }

    distance = 0;
  }

  void OnDrawGizmosSelected() {
    Gizmos.DrawLine(transform.position + Vector3.up * height, transform.position - Vector3.up * height);
  }

  void SpawnObstacles(List<Spawn> batch) {
    foreach(var spawn in batch) {
      List<GameObject> objects = spawn.Activate(transform);
      foreach(var obj in objects) {
        NetworkServer.Spawn(obj);
      }
    }
  }
}
