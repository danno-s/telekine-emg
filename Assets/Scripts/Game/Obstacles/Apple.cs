using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : AbstractObstacle {
  public override void Execute(Player player) {
    if(Matches(player)) {
      GetComponent<SpriteRenderer>().enabled = false;
      var ps = GetComponent<ParticleSystem>();
      ps.Play();
      Destroy(gameObject, ps.main.startLifetime.constant);
      if(player.isLocalPlayer)
        player.EatApple();
    }
  }

  new private void Update() {
    base.Update();
  }
}
