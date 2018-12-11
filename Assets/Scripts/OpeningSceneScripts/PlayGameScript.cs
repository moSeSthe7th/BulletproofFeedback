using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour {

    public void playGame() {
        if(!PlayerPrefs.HasKey("PlayedTime")){
            PlayerPrefs.SetInt("PlayedTime", 0);
            SceneManager.LoadScene("TutorialScene",LoadSceneMode.Single);
        }
        else{
            SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
        }
    }
}
