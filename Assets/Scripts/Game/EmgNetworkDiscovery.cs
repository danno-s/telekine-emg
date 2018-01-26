using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EmgNetworkDiscovery : NetworkDiscovery {

  public bool connected = false;

  public override void OnReceivedBroadcast(string fromAddress, string data) {
    connected = true;
    var bnm = GetComponent<BirdNetworkManager>();
    bnm.Log("Found server. Connecting...");
    bnm.networkAddress = fromAddress;
    bnm.StartClient();
    StopBroadcast();
  }
}
