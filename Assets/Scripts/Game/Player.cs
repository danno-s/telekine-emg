﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

  public float gravity, jumpSpeed;
  public Team team;
  public bool debug;
  public float disabledTime;
  public int invulnFrames;
  private bool alive;
  [SyncVar]
  public float speed;
  private int counter;
  private float timer;
  private Animator animator;
  private Score scoreboard;
  
  // Use this for initialization
  void Start() {
    if(!isLocalPlayer) {
      transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
      return;
    }

    scoreboard = FindObjectOfType<Score>();

    EMGInput.SubscribeToSinglePulse(Jump);
    EMGInput.SubscribeToContinuousPulse(Glide);

    animator = GetComponent<Animator>();
    alive = true;

    speed = jumpSpeed;
  }

  internal void ScorePoints(int points) {
    scoreboard.AddPoints(points);
  }

  internal void EatApple() {
    ScorePoints(150);
  }

  internal void Jump() {
    if(isLocalPlayer && (alive || debug))
      speed = 5;
  }

  internal void Glide() {
    speed = speed < -gravity / 8 ? -gravity / 8 : speed;
  }

  public void Hit() {
    if (counter == 0) {
      alive = false;
      animator.SetBool ("Dead", true); 
    }
  }

  // Update is called once per frame
  void Update () {
    transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(speed / 5), Vector3.forward);
    if(!isLocalPlayer) {
      return;
    }

    if (counter != 0)
      counter--;
    
    var pos = transform.position;
    speed -= gravity * Time.deltaTime;

    if(alive && timer != 0) {
      Jump();
      if(transform.position.y >= -0.5) {
        timer = 0;
        animator.SetBool("Dead", false);
        counter = invulnFrames;
      }
    } else if(alive) {
      if(Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
        Jump();
      }

      if(debug && pos.y < -5) {
        Jump();
      }
    } else if (timer < disabledTime) {
      timer += Time.deltaTime;
    } else {
      alive = true;
    }

    pos.y += speed * Time.deltaTime;

    pos.y = pos.y > -8 ? pos.y : -8;
    transform.position = pos; 
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if(isServer) {
      Utils.NavigateToPrefabRoot(collision.transform).GetComponent<IObstacle>().Execute(this, (BoxCollider2D) collision);
    } else {
      collision.transform.root.GetComponent<IObstacle>().Execute(this, (BoxCollider2D) collision);
    }
  }

  private void OnDestroy() {
    EMGInput.UnsubscribeFromSinglePulse(Jump);
  }

  [ClientRpc]
  public void RpcSaveScore(int level) {
    FindObjectOfType<ScoreManager>().Save(level, FindObjectOfType<Score>().Pop());
  }
}
