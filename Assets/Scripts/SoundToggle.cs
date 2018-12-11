using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundToggle : MonoBehaviour
{
	//Toggle vibrationToggle;
	bool isSoundOn;
	public Text soundText;

	void Start()
	{

		//vibrationToggle = GetComponent<Toggle>();
		//vibrationToggle.isOn = true;
		int voiceInt = PlayerPrefs.GetInt("voiceToggler");
		if (voiceInt == 0)
		{
			isSoundOn = true;
			setButton ();
			//vibrationToggle.isOn = false;
		}
		else
		{
			isSoundOn = false;
			setButton ();
			//vibrationToggle.isOn = true;
		}


		/*ToggleValueChanged();
		vibrationToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });*/
	}


	public void soundChanged(){
		if (isSoundOn) {
			PlayerPrefs.SetInt ("voiceToggler", 1);
			AudioListener.pause = true;
			Debug.Log ("audio off");
			isSoundOn = false;
			setButton ();
		} else {
			PlayerPrefs.SetInt("voiceToggler", 0);
			AudioListener.pause = false;
			Debug.Log("audio on");
			isSoundOn = true;
			setButton ();
		}
	}

	private void setButton(){
       
            if (isSoundOn)
            {
                soundText.text = "SOUND ON";
            }
            else
            {
                soundText.text = "SOUND OFF";
            }
        
	}

	/*void ToggleValueChanged()
	{
		if (vibrationToggle.isOn)
		{
			PlayerPrefs.SetInt("voiceToggler", 1);
			AudioListener.pause = true;
			Debug.Log("audio off");
		}
		else
		{
			PlayerPrefs.SetInt("voiceToggler", 0);
			AudioListener.pause = false;
			Debug.Log("audio on");
		}
	}*/
}