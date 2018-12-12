using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChangerButton : MonoBehaviour {

	public Button levelsButton;
	private PlayGameScript playGameScript;

	// Use this for initialization
	void Start() {
		playGameScript = FindObjectOfType (typeof(PlayGameScript)) as PlayGameScript;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeModeText(Text modeText){
		if (DataScript.isGameModeEndless == 1) {
			DataScript.isGameModeEndless = 0;
		} else {
			DataScript.isGameModeEndless = 1;
		}
		playGameScript.setPlayButtonText ();
	}
}
