using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoTutorial : MonoBehaviour{

    public void GoToTut(){
        SceneManager.LoadScene("TutorialScene",LoadSceneMode.Single);
    }

}
