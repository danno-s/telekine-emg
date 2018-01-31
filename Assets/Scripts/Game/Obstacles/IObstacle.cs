using UnityEngine;

/// <summary>
/// Minimal Behaviour an obstacle should implement.
/// </summary>
public interface IObstacle {
  /// <summary>
  /// Execute the obstacle's action based on the specified player and collider.
  /// </summary>
  /// <param name="player">The player that triggered the method.</param>
  /// <param name="collider">The collider that was triggered by the player. To be compared with the scoreArea.</param>
  void Execute(Player player, BoxCollider2D collider);

  /// <summary>
  /// Special method for interaction with a <see cref="Switch"/>.
  /// </summary>
  void Activate();

  /// <summary>
  /// Sets the speed. Intended for use from a <see cref="Spawner"/> .
  /// </summary>
  /// <param name="speed">Target speed.</param>
  void SetSpeed(float speed);

  /// <summary>
  /// Sets the vertical speed. Intended for use from a <see cref="Spawner"/> .
  /// </summary>
  /// <param name="speed">Target speed.</param>
  void SetVSpeed(float vSpeed);

  /// <summary>
  /// Sets the point from which the object should start moving vertically (measured from the player's position to the right).
  /// Intended for use from a <see cref="Spawner"/> .
  /// </summary>
  /// <param name="speed">Threshold.</param>
  void SetVThreshold(float vSpeed);

  /// <summary>
  /// Determines whether this instance can be destroyed.
  /// </summary>
  /// <returns><c>true</c> if this instance can be destroyed; otherwise, <c>false</c>.</returns>
  bool CanBeDestroyed();

  /// <summary>
  /// Matches the specified player.
  /// </summary>
  /// <param name="player">A given Player.</param>
  /// <returns><c>true</c> if the player and the obstacle are on the same team, otherwise, <c>false</c>.</returns>
  bool Matches(Player player);
}