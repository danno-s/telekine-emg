using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour {

  public LANBroadcastService broadcastService;
  public ParticleSystem progressBar;
  private Text text;
  private LANBroadcastService.enuState state;

  private void Start() {
    text = GetComponent<Text>();
    state = broadcastService.State;
  }

  // Update is called once per frame
  void Update () {
    state = broadcastService.State;
    if(state == LANBroadcastService.enuState.NotActive) {
      text.text = "";
      if(progressBar.isPlaying)
        progressBar.Stop();
    } else if(state == LANBroadcastService.enuState.Searching) {
      text.text = "Searching for matches...";
      if(!progressBar.isPlaying)
        progressBar.Play();
    } else {
      text.text = "Match started.";
      if(progressBar.isPlaying)
        progressBar.Stop();
    }
  }
}
