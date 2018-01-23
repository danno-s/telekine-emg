using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuousPulseViewer : MonoBehaviour {

  Image image;

  // Use this for initialization
  void Start() {
    image = GetComponent<Image>();
    EMGInput.SubscribeToContinuousPulse(Show);
    EMGInput.SubscribeToRelaxation(Hide);
  }

  public void Show() {
    var col = image.color;
    col.a = 1.0f;
    image.color = col;
  }

  public void Hide() {
    var col = image.color;
    col.a = 0.0f;
    image.color = col;
  }

  private void OnDestroy() {
    EMGInput.UnsubscribeFromContinuousPulse(Show);
    EMGInput.UnsubscribeFromRelaxation(Hide);
  }
}
