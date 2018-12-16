using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameScript : MonoBehaviour {
     
	private int playerLevel;
	private Text textofButton;
	public Button levelSelectionButton;
    //use played time for endless, use playerlevel for level based

	//bu kısım game entering scripte konulabilir...
	/*
		if (!PlayerPrefs.HasKey("PlayerLevel"))
		{
			PlayerPrefs.SetInt("PlayerLevel", 1);
		}
	*/ //konuldu bile

	void Start(){
		textofButton = gameObject.GetComponentInChildren<Text> ();
		setPlayButtonText ();
	}

    public void playGame() {
		
		if(!PlayerPrefs.HasKey("PlayedTime")){
			PlayerPrefs.SetInt("PlayedTime", 0);
			SceneManager.LoadScene("TutorialScene",LoadSceneMode.Single);
		}
        else
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }

	public void setPlayButtonText(){
		//game mode endless
		playerLevel = PlayerPrefs.GetInt("PlayerLevel",1);
		if (DataScript.isGameModeEndless == 1) {
			textofButton.text = "PLAY: ENDLESS";
			levelSelectionButton.gameObject.SetActive (false);
		}
		//game mode levels
		else {
			textofButton.text = "PLAY: LEVEL " + playerLevel;
			levelSelectionButton.gameObject.SetActive (true);
		}
	}
}
