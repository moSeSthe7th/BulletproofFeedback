using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePointScript : MonoBehaviour {
	public Text highScore;

	void Start () {
		//PlayerPrefs.SetInt ("LevelHighScore", 0);
		if (DataScript.isGameModeEndless == 1) {
			highScore.text = PlayerPrefs.GetInt ("highScore", 0).ToString ();
		} else {
			highScore.text = PlayerPrefs.GetInt ("LevelHighScore", 0).ToString ();
		}
	}
	

}
