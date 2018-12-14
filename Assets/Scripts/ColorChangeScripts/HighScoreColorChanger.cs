using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreColorChanger : MonoBehaviour {

	Text newHighScoreText;
	Color bgColor;
	public float changeAmount;
	public float waitTimer;


	// Use this for initialization
	void Start () {

		newHighScoreText = GetComponent<Text> ();
		bgColor = newHighScoreText.color;
		StartCoroutine (WaitAndChangeColor (changeAmount, waitTimer));
	}


	/*void LateUpdate(){
		Color newColor;

		bgColor = bgSpriteRenderer.color;
		newColor = hueChanger (bgColor, 0.1f);
		bgSpriteRenderer.color = newColor;
	}*/

	private Color hueChanger(Color colortoChange, float hueIncrease){
		float H, S, V;

		Color.RGBToHSV (colortoChange, out H, out S, out V);
		H = H + hueIncrease;
		return Color.HSVToRGB (H, S, V);
	}

	private  IEnumerator WaitAndChangeColor(float hueIncreaseEnum, float waitTime){

		while (true) {
			Color newColor;
			bgColor = newHighScoreText.color;
			newColor = hueChanger (bgColor, hueIncreaseEnum);
			yield return new WaitForSeconds (waitTime);
			newHighScoreText.color = newColor;
		}
	}
}
//