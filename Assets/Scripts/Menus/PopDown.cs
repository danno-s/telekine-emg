using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopDown : MonoBehaviour {
  public float slideSpeed;
  private int score;
  private Text scoreField;
  private RectTransform rectTransform;
  private float time;

	// Use this for initialization
	void Start () {
    scoreField = GetComponent<Text>();
    rectTransform = GetComponent<RectTransform>();
    time = 0;
	}

  public void SetScore(int score) {
    this.score = score;
  }
	
	// Update is called once per frame
	void Update () {
    scoreField.text = "+" + score.ToString();
    time += Time.deltaTime;

    var col = scoreField.color;
    col.a = -Mathf.Pow(time * 2, 2) + 1;
    scoreField.color = col;

    if(col.a < 0) {
      Destroy(gameObject);
    }

    var pos = rectTransform.anchoredPosition;
    pos.y -= slideSpeed * Time.deltaTime;
    rectTransform.anchoredPosition = pos;
	}
}
