using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EMGInput : MonoBehaviour {
  public int cooldownFrames, averageLength;
  public delegate void Action();
  new public bool enabled;

  private enum MuscleState { RELAXED, PULSED, HELD };
  
  private static EMGInput instance;
  private static MuscleState state = MuscleState.RELAXED;
  private static AudioSource audioClip;
  private static float intensity = 0f, resting = 0f, active = 0f;
  private static List<float> lastValues = new List<float>();
  private static int max, cooldown;
  private static List<Action> 
      singlePulseActions = new List<Action>(), 
      continuousPulseActions = new List<Action>(),
      relaxedActions = new List<Action>();

  public void Start() {
    if(instance != null) {
      Destroy(gameObject);
      return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);

    if(!IsCalibrated())
      SceneManager.LoadScene("Calibration");
    else {
      resting = PlayerPrefs.GetFloat("resting");
      active = PlayerPrefs.GetFloat("active");
    }

    int min;
    Microphone.GetDeviceCaps(null, out min, out max);

    audioClip = GetComponent<AudioSource>();
    audioClip.clip = Microphone.Start(null, true, 10, max);
    audioClip.loop = true;
    while(Microphone.GetPosition(null) < 0);
    audioClip.Play();
  }

  public static bool IsCalibrated() {
    if(PlayerPrefs.HasKey("resting") && PlayerPrefs.HasKey("active"))
      return true;
    else
      return false;
  }

  public static void Calibrate(float rest, float hold) {
    resting = rest;
    active = hold;
    PlayerPrefs.SetFloat("resting", resting);
    PlayerPrefs.SetFloat("active", active);
    PlayerPrefs.Save();
  }

  public void Update() {
    if(enabled) {
      float[] f = new float[(int) (max * Time.deltaTime)];
      audioClip.GetOutputData(f, 0);
      intensity = 0f;
      foreach(var sample in f)
        intensity += Mathf.Abs(sample);

      intensity /= f.Length;
      lastValues.Add(intensity);

      if(lastValues.Count > averageLength)
        lastValues.RemoveAt(0);
    
      CheckPulseStatus();
    
      cooldown--;
    }
  }

  private void CheckPulseStatus() {
    // relaxed muscle after pulsating
    if((state != MuscleState.RELAXED) && GetIntensity() < active) {
      ExecuteActions(relaxedActions);
      state = MuscleState.RELAXED;
    }

    // hasnt relaxed muscle
    if(state == MuscleState.PULSED) {
      state = MuscleState.HELD;
    }

    // pulsed muscle after being relaxed
    if(state == MuscleState.RELAXED && GetIntensity() > resting && cooldown <= 0) {
      ExecuteActions(singlePulseActions);
      state = MuscleState.PULSED;
      cooldown = cooldownFrames;
    }

    if(state == MuscleState.HELD)
      ExecuteActions(continuousPulseActions);
  }

  public static void SubscribeToSinglePulse(Action action) {
    singlePulseActions.Add(action);
  }

  public static void SubscribeToContinuousPulse(Action action) {
    continuousPulseActions.Add(action);
  }

  public static void SubscribeToRelaxation(Action action) {
    relaxedActions.Add(action);
  }

  public static void UnsubscribeFromSinglePulse(Action action) {
    singlePulseActions.Remove(action);
  }

  public static void UnsubscribeFromContinuousPulse(Action action) {
    continuousPulseActions.Remove(action);
  }

  public static void UnsubscribeFromRelaxation(Action action) {
    relaxedActions.Remove(action);
  }

  private void ExecuteActions(List<Action> actions) {
    for(int i = actions.Count - 1; i >= 0; i--) {
      try {
        actions[i]();
      } catch {
        actions.Remove(actions[i]);
      }
    }
  }

  public static float GetIntensity() {
    float average = 0;
    foreach(var intensity in lastValues) {
      average += intensity;
    }
    average /= (lastValues.Count > 0 ? lastValues.Count : 1);
    return average;
  }

  public static float GetRawIntensity() {
    return intensity;
  }

  public static float GetRelativeIntensity() {
    return GetIntensity () / active;
  }
}