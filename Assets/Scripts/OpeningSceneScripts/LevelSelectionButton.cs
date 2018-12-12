using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour {
	private int levelofButton;
	private PlayGameScript playGameScript;
	//private GameObject scrollViewObj;
	private int maxLevel;
	private Button thisButton;

	private LevelOpener levelOpener;


	// Use this for initialization
	void Start () {
		thisButton = gameObject.GetComponent<Button> ();

		maxLevel = PlayerPrefs.GetInt ("MaxLevel", 1);
		playGameScript = FindObjectOfType (typeof(PlayGameScript)) as PlayGameScript;
		levelOpener = FindObjectOfType (typeof(LevelOpener)) as LevelOpener;
		levelofButton = int.Parse (gameObject.GetComponentInChildren<Text> ().text);
		//scrollViewObj = GameObject.FindWithTag("EnteringSceneScroll");
		if (maxLevel < levelofButton) {
			gameObject.GetComponent<Button> ().interactable = false;
			gameObject.GetComponent<Image> ().color = Color.black;
			gameObject.GetComponentInChildren<Text> ().color = Color.black;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectLevel(){
		
		PlayerPrefs.SetInt ("PlayerLevel", levelofButton);
		playGameScript.setPlayButtonText ();
		//scrollViewObj.gameObject.SetActive (false);
		levelOpener.levelClose();

	}
}
