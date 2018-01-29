using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class BirdNetworkManager : NetworkManager {
  public EmgNetworkDiscovery networkDiscovery;
  public Text debug;
  public float wait;
  public List<GameObject> playerPrefabs;
  float time;

  private void Start() {
    networkDiscovery.Initialize();
    if(SceneManager.GetActiveScene().name == "Birds") {
      StartHost();
    }
  }

  public void CreateGame() {
    StartHost();
  }

  public void FindGame() {
    StartClient();
  }

  public override void OnStartHost() {
    if(!networkDiscovery.StartAsServer())
      Log("Error enviando mensajes.");
  }

  private void Update() {
    if(networkDiscovery.running && networkDiscovery.isClient)
      time += Time.deltaTime;

    if(time > wait && !networkDiscovery.connected) {
      time = 0f;
      networkDiscovery.StopBroadcast();
      StartHost();
      Log("No se encontraron partidas.\nCreando una...");
    }
  }

  public void Log(string message) {
    if(debug != null)
      debug.text = message;
  }

  public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
    var player = Instantiate(Utils.PopRandomFromList<GameObject>(playerPrefabs));
    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
  }

  public void FindMatch() {
    time = 0;
    Log("Buscando Partidas...");
    if(!networkDiscovery.StartAsClient())
      Debug.Log("Couldn't start listening.");
  }

  public override void OnStopServer() {
    base.OnStopServer();

    StopClient();
  }
}
