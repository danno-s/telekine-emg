﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
  public GameObject popDown;
  private int score = 0;
  private Text scoreField;
  private ScoreUpdate update;

  private void Start() {
    scoreField = GetComponent<Text>();
  }

  public void AddPoints(int points) {
    score += points;
    var obj = Instantiate(popDown, GetComponent<RectTransform>()) as GameObject;
    obj.GetComponent<PopDown>().SetScore(points);
  }

  public void SetPoints(int points) {
    score = points;
  }

  public void TakePoints(int pointsTaken) {
    SetPoints (score - pointsTaken);
  }

  public int Pop() {
    try {
      return score;
    } finally {
      score = 0;
    }
  }

  public int Peek() {
    return score;
  }

  public void Update() {
    scoreField.text = score.ToString();
  }
}
