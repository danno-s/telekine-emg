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
  private List<List<GameObject>> obstacles;

  // Use this for initialization
  void Start () {
    obstacles = new List<List<GameObject>>() { pipes, switches, gaps };
    parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
  }
	
	// Update is called once per frame
	void Update () {
    // Spawn new obstacles
    if(distance > 10) {
      SpawnObstacle(Utils.GetRandomFromList(Utils.GetRandomFromList(obstacles)));
    }

    distance += speed * Time.deltaTime;

    // Move old obstacles
    foreach(Transform child in transform) {
      if(child.GetComponent<IObstacle>().CanBeDestroyed()) {
        Destroy(child.gameObject);
      }
    }
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
    speed += 1f / speed;
    parallax.UpdateSpeed(speed);
  }
}
