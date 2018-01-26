using UnityEngine;
using System;

[Serializable]
public class Highscore {
  public int Score {
    get {
      return _score;
    }
  }

  public int _score = 0;

  public void UpdateScore(int score) {
    if(score > _score)
      _score = score;
  }
}