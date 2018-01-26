using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public void EnablePlayerButtons() {
    GetComponent<Animator>().SetBool("Enabled", true);
  }

  public void DisablePlayerButtons() {
    GetComponent<Animator>().SetBool("Enabled", false);
  }
}
