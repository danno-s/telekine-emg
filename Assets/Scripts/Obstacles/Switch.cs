using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Switch : AbstractObstacle {

  public Team team;
  public float moveSpeed;
  public List<GameObject> pipes;
  private bool active;
  private Pipe linked;

  public override void Execute(Player player) {
    if(isServer && player.team == team) {
      active = true;
    }
  }

  public override bool CanBeDestroyed() {
    return (!active && transform.position.x < -10) || (active && linked == null);
  }

  private void Start() {
    active = false;

    var myPos = transform.position;
    myPos.y = Random.Range(-4.5f, 4.5f);
    transform.position = myPos;
  }

  public override void OnStartServer() {
    linked = Instantiate(Utils.GetRandomFromList(pipes), transform.parent).GetComponent<Pipe>();
    var pos = linked.transform.position;
    pos.x += 6;
    linked.transform.position = pos;
    NetworkServer.Spawn(linked.gameObject);
  }
	
	// Update is called once per frame
	void Update () {
    var myPos = transform.position;
    myPos.x -= speed * Time.deltaTime;
    transform.position = myPos;

    if(!active)
      return;

    var pos = linked.transform.position;
    if(linked is PipeUp) {
      pos.y -= moveSpeed * Time.deltaTime;
    } else {
      pos.y += moveSpeed * Time.deltaTime;
    }
    linked.transform.position = pos;
  }
}
