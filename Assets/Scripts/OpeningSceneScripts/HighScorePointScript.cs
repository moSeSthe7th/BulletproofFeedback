﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePointScript : MonoBehaviour {
	public Text highScore;

	void Start () {
		highScore.text = PlayerPrefs.GetInt("highScore",0).ToString() ;
	}
	

}
