using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressSlider : MonoBehaviour {

    private float sliderValue;
	private Slider levelProgressSlider;
	private float playerPos;
	private float blockCount;
	private float blockDistance;
	private float totalDistance;

	void Start () {
		
		levelProgressSlider = gameObject.GetComponent<Slider> ();

        levelProgressSlider.GetComponentInChildren<Image>().color = Color.Lerp(DataScript.Themes[DataScript.ThemeIndex].BlockColor, Color.white, 0.3f); //DataScript.Themes[DataScript.ThemeIndex].BlockColor;

        levelProgressSlider.value = 1;
		//Debug.Log ("blocklenggggg; " + GameConst.instance.blocks.Length);
		blockCount = GameConst.instance.blocks.Length;
		blockDistance = GameConst.instance.BlockDistUpdate;
		totalDistance = blockCount * blockDistance;

	}
	
	void LateUpdate () {
		
		playerPos = Player.instance.transform.position.z - GameConst.instance.blocks[0].transform.position.z;

		/*if (levelProgressSlider.value < 1) {
            sliderValue = playerPos / totalDistance;
		}*/
        if(levelProgressSlider.value > 0){
            sliderValue = (totalDistance - playerPos ) / totalDistance;
        }

		levelProgressSlider.value = sliderValue;
	}
}
