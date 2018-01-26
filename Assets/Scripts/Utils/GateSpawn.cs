using System;
using System.Collections.Generic;
using UnityEngine;

internal class GateSpawn : Spawn {
  private List<Spawn> switches;
  private float aperture = 5;

  public GateSpawn(string prefab, float distance, float height, List<Spawn> spawns) : base(prefab, distance, height){
    switches = spawns;
  }

  public override List<GameObject> Activate(Transform manager) {
    var objects = base.Activate(manager);
    var gate = objects[0];

    gate.GetComponent<Gate>().maxAperture = aperture;
    for(int i = 0; i < switches.Count; i++) {
      var nextObject = switches[i].Activate(manager);
      objects.AddRange(nextObject);
      gate.GetComponent<Gate>().Subscribe(nextObject[0].GetComponent<IObstacle>());
    }

    return objects;
  }

  internal void SetAperture(float gap) {
    aperture = gap;
  }

  public override string ToString() {
    var str = base.ToString();
    foreach(var swtch in switches)
      str += "\n    ->" + swtch.ToString();
    return str;
  }
}