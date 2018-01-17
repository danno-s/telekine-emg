﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

  public List<Sprite> sprites;
  private Team team;

	// Use this for initialization
	void Start () {

	}

  public void SetTeam(Player player) {
    team = player.team;
    GetComponent<Image>().sprite = sprites[(int) team];
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
