using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class Pipe : AbstractObstacle {
  protected float retractSpeed = 5;
  protected bool retracting = false;
  
  public override void Execute(Player player) {
    if(player.isLocalPlayer && Matches(player)) {
      player.Hit();
    }
  }

  public override void Activate() {
    retracting = true;
  }
}
