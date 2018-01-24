using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class AbstractObstacle : NetworkBehaviour, IObstacle {
  public Team team;
  private float speed;

  abstract public void Execute(Player player);

  public virtual void Activate() { }

  public void SetSpeed(float speed) {
    this.speed = speed;
  }

  public virtual bool CanBeDestroyed() {
    return transform.position.x < - 10;
  }

  public bool Matches(Player player) {
    return (player.team & team) != 0;
  }

  protected void Update() {
    var myPos = transform.position;
    myPos.x -= speed * Time.deltaTime;
    transform.position = myPos;

    if(CanBeDestroyed()) {
      Destroy(gameObject);
    }
  }
}
