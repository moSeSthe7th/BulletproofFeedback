using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHighScore : MonoBehaviour {

	private int levelHighScore;

	// Use this for initialization
	void Start () {
		levelHighScore = PlayerPrefs.GetInt ("LevelHighScore", 0);
		if (GameConst.instance.points > levelHighScore) {
			PlayerPrefs.SetInt ("LevelHighScore", GameConst.instance.points);
			Debug.Log ("levelhighscore = " + GameConst.instance.points);
		} else {
			gameObject.SetActive (false);
			Debug.Log ("levelHHHH" + levelHighScore);
		}
	}
	

}
