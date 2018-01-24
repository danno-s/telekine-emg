using System;
using System.Collections.Generic;
using UnityEngine;

public class Utils {
  private static Utils instance;
  private static System.Random rng;
  
  private Utils() {
    rng = new System.Random();
  }

  public static T GetRandomFromList<T>(List<T> list) {
    if(instance == null)
      instance = new Utils();
    return list[rng.Next(list.Count)];
  }

  public static T PopRandomFromList<T>(List<T> list) {
    if(instance == null)
      instance = new Utils();
    int index = rng.Next(list.Count);
    var element = list[index];
    list.RemoveAt(index);
    return element;
  }

  public static Transform NavigateToPrefabRoot(Transform child) {
    Transform current = child;
    if(current.parent == null || current.parent == current.root)
      return current;
    return NavigateToPrefabRoot(current.parent);
  }
}