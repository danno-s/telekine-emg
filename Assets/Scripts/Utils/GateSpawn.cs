using System.Collections.Generic;
using UnityEngine;

internal class GateSpawn : Spawn {
  private List<Spawn> switches;

  public GateSpawn(string prefab, float distance, float height, List<Spawn> spawns) : base(prefab, distance, height){
    switches = spawns;
  }

  public override List<GameObject> Activate(Transform manager) {
    var objects = base.Activate(manager);
    var gate = objects[0];

    Debug.Log(switches.Count);
    for(int i = 0; i < switches.Count; i++) {
      var nextObject = switches[i].Activate(manager);
      objects.AddRange(nextObject);
      gate.GetComponent<Gate>().Subscribe(nextObject[0].GetComponent<IObstacle>());
    }

    return objects;
  }
}