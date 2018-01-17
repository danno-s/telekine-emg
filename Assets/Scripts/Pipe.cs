using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class Pipe : NetworkBehaviour, IObstacle {
  public Team team;
  
  public void Execute(Player player) {
    if(player.isLocalPlayer && player.team == team) {
      player.Hit();
    }
  }

  public void Update() {
    var pos = transform.position;
    pos.x -= 5 * Time.deltaTime;
    transform.position = pos;
  }

  public bool CanBeDestroyed() {
    return transform.position.x < -10;
  }
}
