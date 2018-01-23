using UnityEngine;
using System.Collections;

public class GateSwitch : AbstractObstacle {

  private Gate gate;

  public override void Execute(Player player) {
    gate.SwitchActivated(this);
  }

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }
}
