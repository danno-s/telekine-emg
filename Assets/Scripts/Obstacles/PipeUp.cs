using UnityEngine;

public class PipeUp : Pipe {
  new public void Update() {
    base.Update();

    if(retracting) {
      var pos = transform.position;
      pos.y -= retractSpeed * Time.deltaTime;
      transform.position = pos;
    }
  }
}
