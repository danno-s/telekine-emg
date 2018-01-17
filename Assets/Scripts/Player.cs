using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

  public float gravity, jumpSpeed;
  public Team team;
  public bool debug;
  public HPBar hpBar;
  private bool alive;

  [SyncVar]
  public float speed;
  private Animator animator;
  
  // Use this for initialization
  void Start() {
    if(!isLocalPlayer) {
      transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
      return;
    }

    animator = GetComponent<Animator>();
    alive = true;
    var canvas = FindObjectOfType<Canvas>();

    hpBar = Instantiate(hpBar, canvas.transform);
    hpBar.BindToPlayer(this);
    speed = jumpSpeed;
  }

  internal void Jump() {
    speed = 5;
  }

  internal void Hit() {
    animator.ResetTrigger("Recovered");
    if(hpBar.ReduceHP()) {
      animator.SetTrigger("Hurt");
    }
  }

  public void Die() {
    alive = false;
    animator.SetTrigger("Dead");
  }
	
	// Update is called once per frame
	void Update () {
    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(speed / 3), Vector3.forward);
    if(!isLocalPlayer) {
      return;
    }

    var pos = transform.position;
    speed -= gravity * Time.deltaTime;

    if(alive && (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)) {
      Jump();
    }

    if(alive && debug && pos.y < -5) {
      Jump();
    }

    pos.y += speed * Time.deltaTime;
    transform.position = pos;
    
    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(speed / 3), Vector3.forward);
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    try {
      collision.GetComponent<IObstacle>().Execute(this);
    } catch {
      collision.transform.parent.GetComponent<IObstacle>().Execute(this);
    }
  }
}
