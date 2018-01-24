﻿using System.Collections.Generic;
using UnityEngine;

internal class SwitchSpawn : Spawn {
  private List<Spawn> linkedObjects;

  public SwitchSpawn(string prefab, float distance, float height, List<Spawn> spawns) : base(prefab, distance, height) {
    linkedObjects = spawns;
  }

  public override List<GameObject> Activate(Transform manager) {
    var objects = base.Activate(manager);
    var mySwitch = objects[0];

    for(int i = 0; i < linkedObjects.Count; i++) {
      var nextObject = linkedObjects[i].Activate(manager);
      objects.AddRange(nextObject);
      mySwitch.GetComponent<Switch>().Subscribe(nextObject[0].GetComponent<IObstacle>());
    }

    return objects;
  }

  public override string ToString() {
    var str = base.ToString();
    foreach(var spawn in linkedObjects)
      str += "\n    -> " + spawn.ToString();
    return str;
  }
}