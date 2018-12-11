using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IS Vocie on and is ads purchased are checked in here

public class GameEnteringScript : MonoBehaviour {
    
	void Start (){

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
