using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : AbstractObstacle {
  public override void Execute(Player player) {
    if(player.isLocalPlayer && Matches(player)) {
      GetComponent<SpriteRenderer>().enabled = false;
      var ps = GetComponent<ParticleSystem>();
      ps.Play();
      Destroy(gameObject, ps.main.startLifetime.constant);
      player.EatApple();
    }
  }

  new private void Update() {
    base.Update();
  }
}
