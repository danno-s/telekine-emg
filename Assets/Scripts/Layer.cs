using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour {

  public float speed;
  private Sprite sprite;
  private int layer;

  internal void Init(Sprite sprite, int layer, float maxSpeed) {
    this.sprite = sprite;
    this.layer = layer;
    speed = maxSpeed / layer;
    var pos = transform.position;
    pos.z += layer * 5;
    transform.position = pos;
  }

	// Use this for initialization
	void Start () {
    foreach(Transform screen in transform) {
      screen.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
	}
	
	// Update is called once per frame
	void Update () {
    foreach(Transform screen in transform) {
      var position = screen.position;
      position.x -= speed * Time.deltaTime;

      if(position.x <= -17) {
        position.x += 34;
      }

      screen.position = position;
    }
	}

  public void ChangeSpeed(float maxSpeed) {
    speed = maxSpeed / layer;
  }
}
