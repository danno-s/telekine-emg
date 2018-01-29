using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class Pipe : AbstractObstacle {
  protected float retractSpeed = 5;
  protected bool retracting = false;

  public override void Execute(Player player, BoxCollider2D collider) {
    if(player.isLocalPlayer && Matches(player)) {
      if (collider == scoreArea)
        player.ScorePoints (50);
      else
        player.Hit();
    }
  }

  public override void Activate() {
    retracting = true;
  }
}
