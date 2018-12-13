using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChangerButton : MonoBehaviour {

	public Button levelsButton;
	private PlayGameScript playGameScript;
	public Text highScoreText;

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
			highScoreText.text = PlayerPrefs.GetInt ("LevelHighScore", 0).ToString ();

		} else {
			DataScript.isGameModeEndless = 1;
			highScoreText.text = PlayerPrefs.GetInt ("highScore", 0).ToString ();
		}
		playGameScript.setPlayButtonText ();
	}
}
