using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressSlider : MonoBehaviour {

	public float sliderValue;
	private Slider levelProgressSlider;
	private float playerPos;
	private float blockCount;
	private float blockDistance;
	private float totalDistance;

	// Use this for initialization
	void Start () {
		//if(DataScript.)
		levelProgressSlider = gameObject.GetComponent<Slider> ();	

		levelProgressSlider.GetComponentInChildren<Image> ().color = DataScript.levelModeBlockColor;

		Debug.Log ("blocklenggggg; " + GameConst.instance.blocks.Length);
		blockCount = GameConst.instance.blocks.Length;
		blockDistance = GameConst.instance.BlockDistUpdate;
		totalDistance = blockCount * blockDistance;


	}
	
	// Update is called once per frame
	void Update () {
		
		playerPos = Player.instance.transform.position.z - GameConst.instance.blocks[0].transform.position.z;

		if (levelProgressSlider.value != 1) {
			sliderValue = playerPos / totalDistance;
		}

		levelProgressSlider.value = sliderValue;
	}
}
