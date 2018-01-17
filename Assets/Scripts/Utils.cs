using System;
using System.Collections.Generic;

public class Utils {
  private static Utils instance;
  private static Random rng;
  
  private Utils() {
    rng = new Random();
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
}