using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class BirdNetworkManager : NetworkManager {
  
  public NetworkDiscovery networkDiscovery;
  public Text debug;
  public List<GameObject> playerPrefabs;
  bool searching;
  float time;

  private void Start() {
    if(!networkDiscovery.Initialize())
      Log("Port busy.");
  }

  private void Update() {
    if(searching)
      time += Time.deltaTime;

    if(time > 10.0) {
      searching = false;
      time = 0f;
      networkDiscovery.StopBroadcast();
      if(!networkDiscovery.StartAsServer()) {
        Log("Couldn't start broadcasting.");
        return;
      }
      StartHost();
      Log("Couldn't find active matches.\nStarting server...");
    }
  }

  public void Log(string message) {
    debug.text = message;
  }

  public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
    Debug.Log("Trying to spawn player. Player list status:");
    foreach(var players in playerPrefabs) {
      Debug.Log(players.name);
    }
    var player = Instantiate(Utils.PopRandomFromList<GameObject>(playerPrefabs));
    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
  }
  public void FindMatch() {
    searching = true;
    time = 0;
    Log("Searching for matches...");
    if(!networkDiscovery.StartAsClient())
      Log("Couldn't start listening.");
  }
}
