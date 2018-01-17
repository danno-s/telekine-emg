using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class BirdNetworkManager : NetworkManager {
  public EmgNetworkDiscovery networkDiscovery;
  public Text debug;
  public List<GameObject> playerPrefabs;
  float time;

  private void Start() {
    networkDiscovery.Initialize();
  }

  public override void OnStartHost() {
    if(!networkDiscovery.StartAsServer())
      Log("Couldn't start broadcasting.");
  }

  private void Update() {
    if(networkDiscovery.running && networkDiscovery.isClient)
      time += Time.deltaTime;

    if(time > 1.0 && !networkDiscovery.connected) {
      time = 0f;
      networkDiscovery.StopBroadcast();
      StartHost();
      Log("Couldn't find active matches.\nStarting server...");
    }
  }

  public void Log(string message) {
    debug.text = message;
  }

  public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
    var player = Instantiate(Utils.PopRandomFromList<GameObject>(playerPrefabs));
    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
  }

  public void FindMatch() {
    time = 0;
    Log("Searching for matches...");
    if(!networkDiscovery.StartAsClient())
      Log("Couldn't start listening.");
  }
}
