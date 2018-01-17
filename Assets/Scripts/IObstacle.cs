public interface IObstacle {
  void Execute(Player player);

  bool CanBeDestroyed();
}