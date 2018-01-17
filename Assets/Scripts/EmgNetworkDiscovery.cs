using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EmgNetworkDiscovery : NetworkDiscovery {
  public override void OnReceivedBroadcast(string fromAddress, string data) {
    var bnm = GetComponent<BirdNetworkManager>();
    bnm.Log("Found server. Connecting...");
    bnm.networkAddress = fromAddress;
    bnm.StartClient();
  }
}
