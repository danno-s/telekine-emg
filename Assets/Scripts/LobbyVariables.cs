using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LobbyVariables : NetworkBehaviour {
  public void CreateGame(string game) {
    var nm = GetComponent<NetworkManager>();
    nm.onlineScene = game;
    nm.StartHost();
  }
}
