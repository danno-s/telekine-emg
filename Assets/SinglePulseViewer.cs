using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePulseViewer : MonoBehaviour {

  Image image;
  int counter = 0;
  public EMGValueViewer value;

	// Use this for initialization
	void Start () {
    image = GetComponent<Image>();
    EMGInput.SubscribeToSinglePulse(Show);
	}
	
	// Update is called once per frame
	void Update () {
    if(counter > 0) {
      counter--;
      return;
    }

    var col = image.color;
    col.a = 0;
    image.color = col;
	}

  public void Show() {
    counter = 10;
    var col = image.color;
    col.a = 1f;
    image.color = col;
  }

  public void CheckLatency() {
    EMGInput.SubscribeToSinglePulse(Latency);
  }

  public void Latency() {
    value.Callback();
    EMGInput.UnsubscribeFromSinglePulse(Latency);
  }

  private void OnDestroy() {
    EMGInput.UnsubscribeFromSinglePulse(Show);
  }
}
