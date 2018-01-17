using UnityEngine;

public class PipeUp : Pipe {

  public override void OnStartServer() {
    var pos = transform.position;
    pos.y = Random.Range(-5f, 3f);
    transform.position = pos;
  }
}
