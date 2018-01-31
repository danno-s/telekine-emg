using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Switch : AbstractObstacle {
  private bool active = false;
  private List<IObstacle> links = new List<IObstacle>();

  public override void Execute(Player player, BoxCollider2D collider) {
    if(isServer && Matches(player) && player.Active) {
      var aS = GetComponent<AudioSource> ();
      aS.pitch = 0.95f + Random.value * 0.1f;
      aS.Play ();
      foreach(var link in links) {
        link.Activate();
      }
      player.ScorePoints (50);
    }
  }

  public override bool CanBeDestroyed() {
    return (!active && transform.position.x < -10) || (active && links.Count == 0);
  }

  internal void Subscribe(IObstacle obstacle) {
    links.Add(obstacle);
  }
}
