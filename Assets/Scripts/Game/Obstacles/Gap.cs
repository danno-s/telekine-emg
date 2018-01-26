using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gap : AbstractObstacle {
  public override void Execute(Player player) {
    if(player.isLocalPlayer && Matches(player)) {
      player.Hit();
    }
  }
}
