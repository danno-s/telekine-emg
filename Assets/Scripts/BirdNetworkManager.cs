using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class BirdNetworkManager : NetworkManager {
  
  public List<GameObject> playerPrefabs;

  public void JoinServer(string ip) {
    StartClient();
    Network.Connect(ip, networkPort);
  }

  public void StartLANServer() {
    StartHost();
  }

  public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
    Debug.Log("Trying to spawn player. Player list status:");
    foreach(var players in playerPrefabs) {
      Debug.Log(players.name);
    }
    var player = Instantiate(Utils.PopRandomFromList<GameObject>(playerPrefabs));
    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
  }

  public void StartPlaying() {
    GetComponent<LANBroadcastService>().StartSearchBroadCasting(JoinServer, StartLANServer);
  }

  private void OnApplicationQuit() {
    GetComponent<LANBroadcastService>().StopBroadCasting();
  }
}
