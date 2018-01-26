using UnityEngine;
using System.Collections;

public class GateSwitch : AbstractObstacle {
  private Gate gate;

  public override void Execute(Player player) {
    if(Matches(player))
      gate.SwitchActivated(this);
  }

  public void Link(Gate aGate) {
    gate = aGate;
  }
}
