using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gate : AbstractObstacle {
  [SyncVar]
  public float maxAperture, startAperture = 0.15f;
  [SyncVar]
  public int maxHp, currentHp;
  private float threshold = float.PositiveInfinity, segment;
  private List<GateSwitch> switches = new List<GateSwitch>();
  private Transform pipeUp, pipeDown;

  public override void OnStartClient() {
    base.OnStartClient();

    pipeUp = transform.GetChild(0);
    pipeDown = transform.GetChild(1);

    currentHp = maxHp;
    
    segment = (maxAperture - startAperture) / maxHp / 2;
    threshold = pipeDown.position.y + segment;
  }

  public void Subscribe(GateSwitch aSwitch) {
    switches.Add(aSwitch);
  }

  public override void Execute(Player player, BoxCollider2D collider) {
    if (player.isLocalPlayer && Matches (player)) {
      if (collider == scoreArea)
        player.ScorePoints (300);
      else
        player.Hit ();
    }
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

    var currentGap = downPos.y - upPos.y - 1f;
    Debug.Log (currentGap);
    var size = scoreArea.size;
    size.y = currentGap;
    scoreArea.size = size;
  }
}
