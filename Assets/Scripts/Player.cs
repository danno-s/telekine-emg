using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

  public float gravity, jumpSpeed;
  public Team team;
  public bool debug;
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

    EMGInput.SubscribeToSinglePulse(Jump);
    EMGInput.SubscribeToContinuousPulse(Glide);

    animator = GetComponent<Animator>();
    alive = true;

    speed = jumpSpeed;
  }

  internal void Jump() {
    if(isLocalPlayer && (alive || debug))
      speed = 5;
  }

  internal void Glide() {
    speed = speed < -gravity / 8 ? -gravity / 8 : speed;
  }

  internal void Hit() {
    // TODO: Add animations for dying and respawning.
    animator.ResetTrigger("Recovered");
    animator.SetTrigger("Hurt");
  }

  public void Die() {
    alive = false;
    animator.SetTrigger("Dead");
  }
	
	// Update is called once per frame
	void Update () {
    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(speed / 5), Vector3.forward);
    if(!isLocalPlayer) {
      return;
    }

    var pos = transform.position;
    speed -= gravity * Time.deltaTime;

    if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) {
      Jump();
    }

    if(debug && pos.y < -5) {
      Jump();
    }

    pos.y += speed * Time.deltaTime;
    transform.position = pos; 
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    try {
      collision.GetComponent<IObstacle>().Execute(this);
    } catch {
      collision.transform.parent.GetComponent<IObstacle>().Execute(this);
    }
  }

  private void OnDestroy() {
    EMGInput.UnsubscribeFromSinglePulse(Jump);
  }
}
