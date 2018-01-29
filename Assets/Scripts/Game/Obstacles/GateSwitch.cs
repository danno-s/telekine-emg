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
    if (Matches (player)) {
      gate.SwitchActivated (this);
      player.ScorePoints (25);
    }
  }
}
