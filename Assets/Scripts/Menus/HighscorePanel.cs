using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscorePanel : MonoBehaviour {
  private Text text;
  private ScoreManager score;

	// Use this for initialization
	void Start () {
    text = GetComponent<Text>();
    score = FindObjectOfType<ScoreManager>();
    int level = 1, levelScore;
    while(true) {
      levelScore = score.GetScore(level);

      if(levelScore == 0)
        break;

      string lvl = "Nivel " + level;
      string scoreString = levelScore.ToString().PadLeft(25 - lvl.Length);

      text.text += lvl + scoreString + "\n";
      level++;
    }
    text.text += "Juegue más para llenar de puntajes!";
	}
}
