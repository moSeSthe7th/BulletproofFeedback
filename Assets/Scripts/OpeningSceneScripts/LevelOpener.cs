using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOpener : MonoBehaviour {
	
	private PlayGameScript playGameScript;
	public Button modeChangerButton;
	public Text modeInfoText;
	public GameObject enteringScroll;

	// Use this for initialization
	void Start () {
		playGameScript = FindObjectOfType (typeof(PlayGameScript)) as PlayGameScript;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void levelOpenerButton(){
		//bu kısım icerideki butonların her birine verilecek.
		/*PlayerPrefs.SetInt ("PlayerLevel", 2);
		playGameScript.setPlayButtonText ();*/
		if (enteringScroll.gameObject.activeInHierarchy) {
			levelClose ();
		} else {
		
			levelOpen ();
		}

	}

	public void levelOpen(){
		modeInfoText.gameObject.SetActive (false);
		modeChangerButton.gameObject.SetActive (false);
		enteringScroll.gameObject.SetActive (true);
	}

	public void levelClose(){
		modeInfoText.gameObject.SetActive (true);
		modeChangerButton.gameObject.SetActive (true);
		enteringScroll.gameObject.SetActive (false);
	}
}
