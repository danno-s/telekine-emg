using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMGValueViewer : MonoBehaviour {

  private Text text;
  private float timer = -1, latency = 0;

	// Use this for initialization
	void Start () {
    text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
    if(timer >= 0)
      timer += Time.deltaTime;
    text.text = "intensity: " + Mathf.Abs(EMGInput.GetIntensity());
    if(latency != 0) {
      text.text += "\nlatency: " + latency + "s";
    }
	}

  public void CheckLatency() {
    timer = 0;
  }

  public void Callback() {
    latency = timer;
    timer = -1;
  }
}
