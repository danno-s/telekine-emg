using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : AbstractObstacle {
  public override void Execute (Player player, BoxCollider2D collider) {
    if (player.isLocalPlayer) {
      player.Jump ();
      player.Hit ();
    }
  }

  new private void Update() {}
}
