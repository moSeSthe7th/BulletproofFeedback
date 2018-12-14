using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChangerButton : MonoBehaviour {

	public Button levelsButton;
	private PlayGameScript playGameScript;
	public Text highScoreText;
	public Material blockMaterial;
	public Material hexogenMaterial;

	void Start() {
		playGameScript = FindObjectOfType (typeof(PlayGameScript)) as PlayGameScript;
	}

	public void changeModeText(Text modeText){
		if (DataScript.isGameModeEndless == 1) {
			float randFlt = Random.Range (0.01f, 1f);
			DataScript.isGameModeEndless = 0;
			highScoreText.text = PlayerPrefs.GetInt ("LevelHighScore", 0).ToString ();
			//PlayerPrefs.SetInt ("gameMode", 0);
			DataScript.levelModeBlockColor = HueChanger.hueChanger (blockMaterial.GetColor ("_Color"), randFlt);
			DataScript.levelModeHexogenColor = HueChanger.hueChanger (hexogenMaterial.GetColor ("_Color"), randFlt);

		} else {
			//PlayerPrefs.SetInt ("gameMode", 1);
			DataScript.isGameModeEndless = 1;
			DataScript.levelModeBlockColor = blockMaterial.GetColor("_Color");
			DataScript.levelModeHexogenColor = hexogenMaterial.GetColor ("_Color");
			highScoreText.text = PlayerPrefs.GetInt ("highScore", 0).ToString ();

		}
		playGameScript.setPlayButtonText ();
	}
}
