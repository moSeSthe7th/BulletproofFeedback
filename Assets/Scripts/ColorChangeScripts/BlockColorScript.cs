using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorScript : MonoBehaviour {

	Material blockMat;
	Color blockColor;
	public float changeAmount;
	public float waitTimer;

	// Use this for initialization
	void Start () {
		blockMat = gameObject.GetComponent<Renderer> ().sharedMaterial;
		blockColor = blockMat.color;
		StartCoroutine (WaitAndChangeColor (changeAmount, waitTimer));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Color hueChanger(Color colortoChange, float hueIncrease){
		float H, S, V;

		Color.RGBToHSV (colortoChange, out H, out S, out V);
		H = H + hueIncrease;
		return Color.HSVToRGB (H, S, V);
	}

	private  IEnumerator WaitAndChangeColor(float hueIncreaseEnum, float waitTime){


		while (true) {
			Color newColor;
			blockColor = blockMat.color;
			newColor = hueChanger (blockColor, hueIncreaseEnum);
			yield return new WaitForSeconds (waitTime);
			blockMat.color = newColor;
		}
	}
}
