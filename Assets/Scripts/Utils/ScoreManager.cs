using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System;

public class ScoreManager : MonoBehaviour {
  private Dictionary<int, Highscore> levelScores = new Dictionary<int, Highscore>();

  public int GetScore(int level) {
    Load(level);
    try {
      return levelScores[level].Score;
    } catch {
      return 0;
    }
  }

  public void Save(int level, int score) {
    Load(level);
    try {
      levelScores[level].UpdateScore(score);
    } catch {
      levelScores.Add(level, new Highscore());
    }

    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.persistentDataPath + "/level" + level + "highscore.dat");

    bf.Serialize(file, levelScores[level]);
    file.Close();
  }

  public void Load(int level) {
    if(File.Exists(Application.persistentDataPath + "/level" + level + "highscore.dat")) {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "/level" + level + "highscore.dat", FileMode.Open);
      levelScores[level] = (Highscore) bf.Deserialize(file);
      file.Close();
    }
  }
}
