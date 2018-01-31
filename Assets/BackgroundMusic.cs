using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour {
  private static AudioMixer mixer;

	// Use this for initialization
	void Start () {
    mixer = GetComponent<AudioSource> ().outputAudioMixerGroup.audioMixer;
	}
	
  public static void OnScoreEnter() {
    mixer.FindSnapshot ("Scoring").TransitionTo(2.5f);
  }

  public static void OnScoreExit() {
    mixer.FindSnapshot ("Gameplay").TransitionTo(2.5f);
  }
}
