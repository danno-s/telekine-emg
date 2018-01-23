using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gap : AbstractObstacle {

  public Team team;

  public override void OnStartServer() {
    var pos = transform.position;
    pos.y = Random.Range(-3f, 5f);
    transform.position = pos;
  }

  private void Update() {
    var pos = transform.position;
    pos.x -= speed * Time.deltaTime;
    transform.position = pos;
  }

  public override void Execute(Player player) {
    if(player.isLocalPlayer && player.team == team) {
      player.Hit();
    }
  }
}
