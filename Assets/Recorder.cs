using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Recorder : MonoBehaviour {

  public Button button;
  public ProgressBar progressBar;
  private List<float> data = new List<float>();
  private float time = 0, maxTime = 20f;
  private bool recording = false;

	// Update is called once per frame
	void Update () {
    progressBar.UpdateProgress (time / maxTime);
    if (recording) {
      time += Time.deltaTime;
      data.Add (EMGInput.GetIntensity ());

      if (time >= maxTime) {
        Save ();
        Toggle ();
        time = 0;
        data.Clear ();
      }
    }
	}

  private void Save() {
    FileStream dataFile = File.Create (Application.persistentDataPath + "/emgData.txt");

    var str = "";
    for (int i = 0; i < data.Count; i++) {
      str += i + "," + data [i] + System.Environment.NewLine;
    }

    byte[] dataBytes = Encoding.UTF8.GetBytes(str);
    dataFile.Write (dataBytes, 0, dataBytes.Length);
    dataFile.Close ();
  }

  public void Toggle() {
    recording = !recording;

    var colors = button.colors;
    if (recording) {
      colors.normalColor = Color.red * Color.grey;
      colors.disabledColor = Color.red * Color.grey;
      colors.highlightedColor = Color.red * Color.grey;
      colors.pressedColor = Color.red * Color.grey;
    } else {
      colors.normalColor = Color.white;
      colors.disabledColor = Color.white;
      colors.highlightedColor = Color.white;
      colors.pressedColor = Color.white;
    }
    button.colors = colors;
  }
}
