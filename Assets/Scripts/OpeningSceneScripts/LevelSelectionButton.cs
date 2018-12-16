using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour {
	private int levelofButton;
	private PlayGameScript playGameScript;
	//private GameObject scrollViewObj;
	private int maxLevel;
	//private Button thisButton;

	private LevelOpener levelOpener;


	void Start () {
		//thisButton = gameObject.GetComponent<Button> ();
		maxLevel = PlayerPrefs.GetInt ("MaxLevel", 1);
		playGameScript = FindObjectOfType (typeof(PlayGameScript)) as PlayGameScript;
		levelOpener = FindObjectOfType (typeof(LevelOpener)) as LevelOpener;
		levelofButton = int.Parse (gameObject.GetComponentInChildren<Text> ().text);
		if (maxLevel < levelofButton) {
			gameObject.GetComponent<Button> ().interactable = false;
			gameObject.GetComponent<Image> ().color = Color.black;
			gameObject.GetComponentInChildren<Text> ().color = Color.black;
		}
	}

	public void selectLevel(){
        if(PlayerPrefs.GetInt("PlayerLevel") != levelofButton)
        {
            DataScript.isSessionEnded = true;
            DataScript.pointHolder = 0;
        }
		PlayerPrefs.SetInt ("PlayerLevel", levelofButton);
        DataScript.ThemeIndex = PlayerPrefs.GetInt("PlayerLevel") - 1;
		playGameScript.setPlayButtonText ();
		levelOpener.levelClose();

	}
}
