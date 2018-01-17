using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

  public GameObject heart;
  public int hp;

  private Player player;
  private List<GameObject> hearts;

  // Use this for initialization
  void Start() {
    
	}
	
	public void BindToPlayer(Player player) {
    this.player = player;
    heart.GetComponent<Heart>().SetTeam(player);
    hearts = new List<GameObject>();
    for(int i = 0; i < hp; i++) {
      GameObject hrt = Instantiate(heart, transform);
      var pos = hrt.transform.localPosition;
      pos.x += 40 * i;
      hrt.transform.localPosition = pos;
      hearts.Add(hrt);
    }
  }

  public bool ReduceHP() {
    Image heart = hearts[--hp].GetComponent<Image>();
    Color col = heart.color;
    col.a = 0;
    heart.color = col;
    heart.GetComponent<ParticleSystem>().Play();

    if(hp == 0) {
      player.Die();
    }

    return hp > 0;
  }
}
