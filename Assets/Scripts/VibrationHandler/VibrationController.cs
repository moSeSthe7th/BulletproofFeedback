using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationController : MonoBehaviour {

	int vibrationControllerInt;
	bool isVibrationOn;
	public Text vibrationText;

	void Start () {
		vibrationControllerInt = PlayerPrefs.GetInt ("Vibration", 1);
		if (vibrationControllerInt == 1) {
			isVibrationOn = true;
			setButton ();
		} else {
			isVibrationOn = false;
			setButton ();
		}
	}
	


	public void vibrationChanged(){
		if (isVibrationOn) {
			PlayerPrefs.SetInt ("Vibration", 0);
			isVibrationOn = false;
			setButton ();
			Debug.Log ("Vibration off");
		} else {
			PlayerPrefs.SetInt ("Vibration", 1);
			isVibrationOn = true;
			setButton ();
			Debug.Log ("Vibration on");
		}
	}

	private void setButton(){
		
			if (isVibrationOn)
			{
				vibrationText.text = "VIBRATION ON";
			}
			else
			{
				vibrationText.text = "VIBRATION OFF";
			}
		
	}
}
