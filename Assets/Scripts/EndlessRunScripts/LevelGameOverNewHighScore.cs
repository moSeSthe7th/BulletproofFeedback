using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// /gereksiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiizzzzzzzzzzzzzzzzzzzzzzzzzzzzz
/// </summary>
public class LevelGameOverNewHighScore : MonoBehaviour {

	private int levelHighScore;
	public Text highScore;
	public Text highScoreText;

	// Use this for initialization
	void Start () {
		levelHighScore = PlayerPrefs.GetInt ("LevelHighScore", 0);
		if (GameConst.instance.points > levelHighScore) {
			PlayerPrefs.SetInt ("LevelHighScore", GameConst.instance.points);
			Debug.Log ("levelhighscore = " + GameConst.instance.points);
			highScore.gameObject.SetActive (false);
			highScoreText.gameObject.SetActive (false);
		} else {
			gameObject.SetActive (false);
			Debug.Log ("levelHHHH" + levelHighScore);
		}
	}
}
