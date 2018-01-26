public interface IObstacle {
  void Execute(Player player);

  void Activate();

  void SetSpeed(float speed);

  bool CanBeDestroyed();

  bool Matches(Player player);
}