using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

  public RectTransform background, fill;

  // Use this for initialization
  void Start () {
    fill.localScale = new Vector3(0, 1, 1);
	}
	
  public void UpdateProgress(float percentage) {
    fill.localScale = new Vector3(percentage, 1, 1);
    fill.localPosition = new Vector3(percentage * 150 - 150, 0, 0);
  }

  public void SetColor(Color color) {
    fill.gameObject.GetComponent<Image>().color = color;
  }
}
