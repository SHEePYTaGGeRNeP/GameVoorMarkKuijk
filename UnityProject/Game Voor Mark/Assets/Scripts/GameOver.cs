using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start ()
    {
        scoreText.text = "You archived " + GameManager.INSTANCE.score + " cards.";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
