using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// This class should be used for any obstacle that appears in the scene with the player.
/// It updates the obstacle's position automatically and determines when it should be destroyed.
/// </summary>
public abstract class AbstractObstacle : NetworkBehaviour, IObstacle {
  /// <summary>
  /// The team. Can be Pink, Blue, Yellow or Any (matches any) or None (can't match)
  /// </summary>
  public Team team;

  /// <summary>
  /// To be assigned in the unity editor. A collider that can be used by the obstacle to specify special behaviour
  /// </summary>
  public BoxCollider2D scoreArea;

  /// <summary>
  /// The obstacle's movement speed. Can be assigned in the level XML document with the tag "Speed".
  /// </summary>
  [SyncVar]
  protected float speed;

  abstract public void Execute(Player player, BoxCollider2D collider);

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
