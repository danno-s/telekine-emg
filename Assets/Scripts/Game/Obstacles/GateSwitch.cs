using UnityEngine;
using System.Collections;

public class GateSwitch : AbstractObstacle {
  private Gate gate;

  public override void OnStartClient() {
    base.OnStartClient();

    gate = FindObjectOfType<Gate>();

    gate.Subscribe(this);
  }

  public override void Execute(Player player, BoxCollider2D collider) {
    if (Matches (player) && player.Active) {
      gate.SwitchActivated (this);
      if (player.isLocalPlayer) {
        player.ScorePoints (25);
      }
    }
  }
}
