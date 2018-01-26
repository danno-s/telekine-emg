using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var rect = GetComponent<RectTransform>();
    var dim = rect.parent.GetComponent<RectTransform>().rect;
    var width = dim.size[0];

    rect.offsetMin = new Vector2(32, 32);
    rect.offsetMax = new Vector2(- width / 2 - 32, -32);
	}
}
