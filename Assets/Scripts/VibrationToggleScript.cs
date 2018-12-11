using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationToggleScript : MonoBehaviour {

	Toggle vibrationToggle;

	// Use this for initialization
	void Start () {
		vibrationToggle = GetComponent<Toggle> ();
		vibrationToggle.isOn = true;
		int vibrationInt = PlayerPrefs.GetInt ("vibrationToggler");
		if (vibrationInt == 0) {
			vibrationToggle.isOn = false;
		} else {
			vibrationToggle.isOn = true;
		}
		vibrationToggle.onValueChanged.AddListener (delegate {
			ToggleValueChanged ();
		});
	}

	void ToggleValueChanged(){
		if (vibrationToggle.isOn) {
			PlayerPrefs.SetInt ("vibrationToggler", 1);
			//AudioListener.pause = false;
		} else {
			PlayerPrefs.SetInt ("vibrationToggler", 0);
			//AudioListener.pause = true;
		}
	}
}