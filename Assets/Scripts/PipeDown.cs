using UnityEngine;

public class PipeDown : Pipe {

  public override void OnStartServer() {
    var pos = transform.position;
    pos.y = Random.Range(-3f, 5f);
    transform.position = pos;
  }
}
