using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

  public List<Sprite> sprites;
  public List<Material> materials;
  private Team team;

	// Use this for initialization
	void Start () {

	}

  public void SetTeam(Player player) {
    team = player.team;
    GetComponent<Image>().sprite = sprites[(int) team];
    GetComponent<ParticleSystem>().GetComponent<Renderer>().material = materials[(int) team];
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
