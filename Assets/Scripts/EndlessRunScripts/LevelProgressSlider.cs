using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressSlider : MonoBehaviour {

	public float sliderValue;
	private Slider levelProgressSlider;
	private float passedBlock;
	private float blockCount;

	// Use this for initialization
	void Start () {
		levelProgressSlider = gameObject.GetComponent<Slider> ();	

		Debug.Log ("blocklenggggg; " + GameConst.instance.blocks.Length);
	}
	
	// Update is called once per frame
	void Update () {
		blockCount = GameConst.instance.blocks.Length;
		passedBlock = Player.instance.lastPassedBlock;
		sliderValue = passedBlock / blockCount;
		levelProgressSlider.value = sliderValue;
	}
}
