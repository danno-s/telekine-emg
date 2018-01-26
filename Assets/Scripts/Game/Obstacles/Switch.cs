using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Switch : AbstractObstacle {
  private bool active = false;
  private List<IObstacle> links = new List<IObstacle>();

  public override void Execute(Player player) {
    if(isServer && Matches(player)) {
      foreach(var link in links) {
        link.Activate();
      }
    }
  }

  public override bool CanBeDestroyed() {
    return (!active && transform.position.x < -10) || (active && links.Count == 0);
  }

  internal void Subscribe(IObstacle obstacle) {
    links.Add(obstacle);
  }
}
