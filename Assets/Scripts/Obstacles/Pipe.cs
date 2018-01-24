using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Pipe : AbstractObstacle {
  public Team team;
  
  public override void Execute(Player player) {
    if(player.isLocalPlayer && player.team == team) {
      player.Hit();
    }
  }

  public void Update() {
    var pos = transform.position;
    pos.x -= speed * Time.deltaTime;
    transform.position = pos;
  }
}
