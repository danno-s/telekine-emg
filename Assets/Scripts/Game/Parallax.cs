using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Parallax : NetworkBehaviour {

  [SyncVar]
  public float maxSpeed;
  public GameObject layer;
  public List<Sprite> layers;

	// Use this for initialization
	void Start () {
    int i = 1;

    foreach(Sprite s in layers) {
      GameObject layer = Instantiate(this.layer, transform);
      layer.GetComponent<Layer>().Init(layers[i - 1], i, maxSpeed);
      i++;
    }
	}
	
	public void UpdateSpeed(float speed) {
		foreach(var layer in GetComponentsInChildren<Layer>()) {
      layer.ChangeSpeed(speed);
    }
	}
}
