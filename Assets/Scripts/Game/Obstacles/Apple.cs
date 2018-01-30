using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : AbstractObstacle {
  public override void Execute(Player player, BoxCollider2D collider) {
    if(Matches(player) && player.Active) {
      GetComponent<SpriteRenderer>().enabled = false;
      var ps = GetComponent<ParticleSystem>();
      ps.Play();
      Destroy(gameObject, ps.main.startLifetime.constant);
      if(player.isLocalPlayer)
        player.ScorePoints(150);
    }
  }

  new private void Update() {
    base.Update();
  }
}
