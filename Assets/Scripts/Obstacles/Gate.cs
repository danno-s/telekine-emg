using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : AbstractObstacle {
  
  private List<GateSwitch> switches;
  private Pipe up, down;

  public override void Execute(Player player) {
    player.Hit();
  }

  public void SwitchActivated(GateSwitch gateSwitch) {
    switches.Remove(gateSwitch);

  }

	void Update () {

    var pos = transform.position;
    pos.x -= speed * Time.deltaTime;
    transform.position = pos;
  }
}
