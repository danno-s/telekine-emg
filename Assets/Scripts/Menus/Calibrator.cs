using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Calibrator : MonoBehaviour {

  public ProgressBar progressBar;
  public Color calibratingColor, readingColor;

  private Text text;

  private string[] instructions = {
    "Mantener en reposo el músculo durante 5 segundos.",
    "Hacer fuerza con el músculo durante 5 segundos."
  };

  private float[] times = { 0f, 0f, 0f, 0f };
  private float[] maxTimes = { 2.5f, 5f, 2.5f, 5f };
  private float rest = 0, hold = 0;
  private int restN = 0, holdN = 0;

  // Use this for initialization
  void Start () {
    text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
    int stage;
    for(stage = 0; stage < 4; stage++) {
      if(maxTimes[stage] - times[stage] > 0)
        break;
    }

    if(stage > 3) {
      EMGInput.Calibrate(rest / restN, hold / holdN);
      try {
        Destroy(FindObjectOfType<BirdNetworkManager>().gameObject);
      } catch {
        Debug.Log("No network manager to destroy found.");
      }
      SceneManager.LoadScene("Main Menu");
      return;
    }

    times[stage] += Time.deltaTime; 

    text.text = instructions[stage / 2];

    if(stage % 2 == 0) {
      progressBar.UpdateProgress(1 - times[stage] / maxTimes[stage]);
      if(times[stage] - Time.deltaTime == 0) 
        progressBar.SetColor(readingColor);
    } else {
      progressBar.UpdateProgress(times[stage] / maxTimes[stage]);
      if(times[stage] - Time.deltaTime == 0)
        progressBar.SetColor(calibratingColor);
    }

    switch(stage) {
      case 1:
        rest += EMGInput.GetIntensity();
        restN++;
        break;
      case 3:
        hold += EMGInput.GetIntensity();
        holdN++;
        break;
    }
	}
}
