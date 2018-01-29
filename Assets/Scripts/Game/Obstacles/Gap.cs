using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gap : AbstractObstacle {
  public override void Execute(Player player, BoxCollider2D collider) {
    if(player.isLocalPlayer && Matches(player)) {
      if (collider == scoreArea)
        player.ScorePoints (100);
      else
        player.Hit();
    }
  }
}
