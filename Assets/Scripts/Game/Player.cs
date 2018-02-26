using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
  public bool Active {
    get {
      return alive && timer == 0;
    }
  }

  public float gravity, jumpSpeed;
  public Team team;
  public float disabledTime;
  public int invulnFrames;
  private bool alive, emg;
  [SyncVar]
  public float speed;
  private int counter;
  private float timer;
  private Animator animator;
  private Score scoreboard;
  new private AudioSource audio;

  // Use this for initialization
  void Start() {
    if(!isLocalPlayer) {
      transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
      return;
    }

    scoreboard = FindObjectOfType<Score>();

    animator = GetComponent<Animator>();
    audio = GetComponent<AudioSource> ();
    alive = true;

    try {
      emg = FindObjectOfType<EMGInput>().enabled;
    } catch {
      emg = false;
    }

    speed = 0;
  }

  internal void ScorePoints(int points) {
    scoreboard.AddPoints(points);
  }

  internal void Jump() {
    if (isLocalPlayer && alive) {
      speed = jumpSpeed;
      audio.Play ();
    }
  }

  internal void Glide() {
    if(isLocalPlayer && alive)
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

    if(alive && timer != 0) {
      Jump ();
      if(transform.position.y >= -0.5) {
        timer = 0;
        animator.SetBool("Dead", false);
        counter = invulnFrames;
      }
    } else if(alive) {
      #if UNITY_EDITOR || UNITY_EDITOR_64
      if(Input.GetKey(KeyCode.UpArrow)) {
        pos.y += jumpSpeed * Time.deltaTime;
      } else if(Input.GetKey(KeyCode.DownArrow)) {
        pos.y -= jumpSpeed * Time.deltaTime;
      }
      #endif
      if (emg) {
        var target = EMGInput.GetRelativeIntensity () * 10f - 5f;
        var speed = (target - pos.y) / 0.1f;
        pos.y += speed * Time.deltaTime; 
      }
    } else if (timer < disabledTime) {
      speed -= gravity * Time.deltaTime;
      timer += Time.deltaTime;
    } else {
      alive = true;
      speed = 0;
    }

    pos.y += speed * Time.deltaTime;

    if (pos.y > 5) {
      speed = 0;
      pos.y = 5;
    }
      
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
