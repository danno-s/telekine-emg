using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : AbstractObstacle {
  public float maxAperture, startAperture = 0.15f;
  private List<GateSwitch> switches = new List<GateSwitch>();
  private int maxHp, currentHp;
  private Transform pipeUp, pipeDown;
  private float threshold = float.PositiveInfinity, segment;

  private void Start() {
    pipeUp = transform.GetChild(0);
    pipeDown = transform.GetChild(1);

    maxHp = switches.Count > 0 ? switches.Count : 1;
    currentHp = maxHp;

    segment = (maxAperture - startAperture) / maxHp / 2;
    threshold = pipeDown.position.y + segment;
  }

  public void Subscribe(IObstacle obstacle) {
    var aSwitch = obstacle as GateSwitch;
    switches.Add(aSwitch);
    aSwitch.Link(this);
  }

  public override void Execute(Player player) {
    if(Matches(player))
      player.Hit();
  }

  public void SwitchActivated(GateSwitch gateSwitch) {
    if(switches.Contains(gateSwitch)) {
      switches.Remove(gateSwitch);
      currentHp--;
    }
  }

  new public void Update() {
    base.Update();
    
    /*
     * Pipes are symmetrical with respect to the x axis, so we'll use the downward pipe to calculate the velocities
     * since we'll be dealing with positive numbers.
     */
    if(pipeDown.position.y > threshold) {
      maxHp--;
      threshold += segment;
    }

    var startPoint = pipeDown.position.y;
    var endPoint = pipeDown.position.y + segment * (maxHp - currentHp);
    
    var speed = (endPoint - startPoint) / 2;

    var upPos = pipeUp.position;
    upPos.y -= speed * Time.deltaTime;
    pipeUp.position = upPos;
    
    var downPos = pipeDown.position;
    downPos.y += speed * Time.deltaTime;
    pipeDown.position = downPos;
  }
}
