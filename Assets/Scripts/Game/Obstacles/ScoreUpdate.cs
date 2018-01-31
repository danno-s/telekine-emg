using System.Collections;
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
  new private AudioSource audio;
  private bool moving = true, ready = false;
  private float time = 0;
  private int maxScore, highscore, level, lastPoints = 0;

  public override void Execute(Player player, BoxCollider2D collider) {}

  private void Start() {
    score = FindObjectOfType<Score>();
    maxScore = score.Peek();

    text = GetComponent<TextMesh>();
    text.text = "00000000"; // 8 digits

    dimmer = GameObject.Find("Dimmer").GetComponent<MeshRenderer>();

    audio = GetComponent<AudioSource> ();

    highscoreText = transform.GetChild(1).GetComponent<TextMesh>();

    particles = GetComponent<ParticleSystem>(); 

    var manager = FindObjectOfType<ScoreManager>();
    CmdGetLevel();
    manager.Load(level);
    highscore = manager.GetScore(level);
    highscoreText.text = "HIGHSCORE: " + highscore;

    BackgroundMusic.OnScoreEnter ();
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

    var points = (int) (-maxScore / Mathf.Pow(countTime, 4) * Mathf.Pow(((time > countTime ? countTime : time) - countTime), 4) + maxScore);
    points = points > maxScore ? maxScore : points;
    score.TakePoints(points - lastPoints);
    if (points - lastPoints > 0) {
      audio.pitch = 0.2f * (1 - (maxScore - points) / (float) maxScore) + 0.9f;
      audio.Play ();
    }

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

    lastPoints = points;
  }

  public void ContinueMoving() {
    moving = true;
    if(particles.isPlaying)
      particles.Stop();
    BackgroundMusic.OnScoreExit ();
  }
}
