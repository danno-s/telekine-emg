﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreUpdate : AbstractObstacle {
  public Color normalScoreColor, highscoreColor;
  public float countTime;
  
  private Score score;
  private TextMesh text, highscoreText;
  private ParticleSystem particles;
  private MeshRenderer dimmer;
  private bool moving = true, ready = false;
  private float time = 0;
  private int maxScore, highscore, level;

  public override void Execute(Player player) {}

  private void Start() {
    score = FindObjectOfType<Score>();
    maxScore = score.Peek();

    text = GetComponent<TextMesh>();
    text.text = "00000000"; // 8 digits

    dimmer = GameObject.Find("Dimmer").GetComponent<MeshRenderer>();

    highscoreText = transform.GetChild(1).GetComponent<TextMesh>();

    particles = GetComponent<ParticleSystem>(); 

    var manager = FindObjectOfType<ScoreManager>();
    CmdGetLevel();
    manager.Load(level);
    highscore = manager.GetScore(level);
    highscoreText.text = "HIGHSCORE: " + highscore;
  }

  [Command]
  private void CmdGetLevel() {
    level = FindObjectOfType<ObstacleManager>().lastLevel;
  }

  public override bool CanBeDestroyed() {
    return transform.position.x < -15;
  }

  new public void Update() {
    if(moving) {
      base.Update();
      var color = dimmer.material.color;
      color.a = Mathf.SmoothStep(0.75f, 0, Mathf.Abs(transform.position.x / 10));
      dimmer.material.color = color;
      if(transform.position.x <= 0 && transform.position.x + speed * Time.deltaTime > 0)
        moving = false;
      return;
    }

    time += Time.deltaTime;

    var points = (int) (-maxScore / (countTime * countTime) * Mathf.Pow(((time > countTime ? countTime : time) - countTime), 2) + maxScore);
    points = points > maxScore ? maxScore : points;
    score.SetPoints(maxScore - points);

    var pointStr = points.ToString();
    text.text = "";

    for(int i = 0; i < 8 - pointStr.Length; i++)
      text.text += "0";

    text.text += pointStr;

    Color textColor;
    if(points >= highscore) {
      textColor = highscoreColor;
      highscoreText.text = "HIGHSCORE: " + points;
      if(!particles.isPlaying)
        particles.Play();
    } else {
      textColor = Color.Lerp(Color.black, normalScoreColor, 1f - (highscore - points) / (float) highscore);
    }

    text.color = textColor;

    if(points == maxScore && !ready) {
      Invoke("ContinueMoving", 2);
      FindObjectOfType<ScoreManager>().Save(level, points);
      ready = true;
    }
  }

  public void ContinueMoving() {
    moving = true;
    if(particles.isPlaying)
      particles.Stop();
  }
}
