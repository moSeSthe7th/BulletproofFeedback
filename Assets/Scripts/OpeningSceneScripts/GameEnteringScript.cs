using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IS Vocie on and is ads purchased are checked in here

public class GameEnteringScript : MonoBehaviour {
    
	public Material blockMaterial;
	public Material hexogenMaterial;

	void Start (){
        
		if (DataScript.isGameModeEndless != 1) {
			DataScript.isGameModeEndless = 0;
            DataScript.ThemeIndex = (PlayerPrefs.GetInt("PlayerLevel") - 1) % DataScript.Themes.Count;
			/*float randFlt = (PlayerPrefs.GetInt("PlayerLevel") == 1) ? 0 : Random.Range (0.01f, 1f);
			DataScript.levelModeBlockColor = HueChanger.hueChanger (blockMaterial.GetColor ("_Color"), randFlt);
			DataScript.levelModeHexogenColor = HueChanger.hueChanger (hexogenMaterial.GetColor ("_Color"), randFlt);*/
		}
		/*if (!PlayerPrefs.HasKey ("gameMode")) {
			PlayerPrefs.SetInt ("gameMode", 0);
		}

		if (PlayerPrefs.GetInt ("gameMode") == 0) {
			
			Debug.Log ("random : " + randFlt + " " + " Color : " + DataScript.levelModeHexogenColor);
		} else {
			Debug.LogWarning ("endless");
			DataScript.isGameModeEndless = 1;
		}*/


		if (!PlayerPrefs.HasKey("PlayerLevel"))
		{
			PlayerPrefs.SetInt("PlayerLevel", 1);
		}

        if(PlayerPrefs.HasKey("DidAdsPurchased")){

            DataScript.isAdsPurchased = (PlayerPrefs.GetInt("DidAdsPurchased") == 0) ? false : (PlayerPrefs.GetInt("DidAdsPurchased") == 1) ? true : false;
        }
        else{
            PlayerPrefs.SetInt("DidAdsPurchased", 0);
            DataScript.isAdsPurchased = false;
        }

		int voiceInt = PlayerPrefs.GetInt("voiceToggler");
        Time.timeScale = 2.5f;

		if (voiceInt == 0)
		{
			AudioListener.pause = false;
		}
		else
		{
			AudioListener.pause = true;
		}
	}
	

}
