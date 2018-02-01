using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
  public Button game, create, find;
  private BirdNetworkManager bnm;

  public void Start() {
    bnm = FindObjectOfType<BirdNetworkManager> ();
    game.onClick.AddListener (StartBroadcast);
    create.onClick.AddListener (CreateGame);
    find.onClick.AddListener (FindGame);
  }

  private void StartBroadcast() {
    bnm.FindMatch();
  }

  private void CreateGame() {
    bnm.CreateGame ();
  }

  private void FindGame() {
    bnm.FindGame ();
  }
}
