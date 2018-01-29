using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ObstacleManager : NetworkBehaviour {

  public float height, speed;
  public Parallax parallax;
  public List<string> levels;
  public string betweenLevels;
  public int lastLevel;

  private float distance;
  private LevelScript script, endLevel;

  // Use this for initialization
  void Start () {
    parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
    script = new LevelScript(levels[0]);
    lastLevel = script.level;
    endLevel = new LevelScript(betweenLevels);
  }

	// Update is called once per frame
	void Update () {
    distance += speed * Time.deltaTime;

    // Read script
    if(!script.Over()) {
      if(script.ShouldSpawn(distance))
        SpawnObstacles(script.PopSpawns());
    } else {
      if(transform.childCount == 0)
        ChangeScripts();
    }
	}

  private void ChangeScripts() {
    if(script == endLevel) {
      try {
        script = new LevelScript(levels[0]);
        lastLevel = script.level;
      } catch {
        NetworkServer.Shutdown();
        SceneManager.LoadScene("Main Menu");
      }
    } else {
      levels.RemoveAt(0);
      script = endLevel.Reset();
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
