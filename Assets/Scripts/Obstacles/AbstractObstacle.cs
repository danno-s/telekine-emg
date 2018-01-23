using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class AbstractObstacle : NetworkBehaviour, IObstacle {
  public float speed;

  abstract public void Execute(Player player);

  public virtual bool CanBeDestroyed() {
    return transform.position.x < - 10;
  }
}
