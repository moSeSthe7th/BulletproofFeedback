using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEntranceScene : MonoBehaviour {

	public void goToEntranceScene(){
        SceneManager.LoadScene ("EnteringScene",LoadSceneMode.Single);
	}
}
