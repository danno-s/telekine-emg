using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gap : NetworkBehaviour, IObstacle {

  public Team team;

  public bool CanBeDestroyed() {
    return transform.position.x < -10;
  }

  public override void OnStartServer() {
    var pos = transform.position;
    pos.y = Random.Range(-3f, 5f);
    transform.position = pos;
  }

  private void Update() {
    var pos = transform.position;
    pos.x -= 5 * Time.deltaTime;
    transform.position = pos;
  }

  public void Execute(Player player) {
    if(player.isLocalPlayer && player.team == team) {
      player.Hit();
    }
  }
}
