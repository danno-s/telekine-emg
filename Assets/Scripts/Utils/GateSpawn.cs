using System;
using System.Collections.Generic;
using UnityEngine;

internal class GateSpawn : Spawn {
  private List<Spawn> switches;
  private float aperture = 5;

  public GateSpawn(string prefab, float distance, float height, List<Spawn> spawns) : base(prefab, distance, height){
    switches = spawns;
  }

  public override List<GameObject> Activate(Transform manager, float distance) {
    var objects = base.Activate(manager, distance);
    var gate = objects[0];

    var g = gate.GetComponent<Gate>();
    g.maxAperture = aperture;
    g.maxHp = switches.Count;

    for(int i = 0; i < switches.Count; i++) {
      switches [i].SetDistance (this.distance);
      var nextObject = switches[i].Activate(manager, distance);
      objects.AddRange(nextObject);
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