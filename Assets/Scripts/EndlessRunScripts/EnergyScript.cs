using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour {

	private Slider energySlider;

	//private Image energyImage;
	private Color red;
    private Color green;
	private Color purple;
	private float t; // to change color between 0 and 1, 0 means purple 1 means red
	private bool onAlarm;

    private Image energyImage;

	private void Start()
	{
		energySlider = GetComponent<Slider>();

		energySlider.gameObject.SetActive(true);

		energySlider.minValue = 0;
		energySlider.maxValue = GameConst.instance.energyTime;
		energySlider.value = Player.instance.energyTot;
        energyImage = energySlider.transform.Find("Fill Area").GetComponentInChildren<Image>();

		red = Color.red;
        green = new Color32(30, 220, 30, 255);
		purple = energyImage.color;
		onAlarm = false;
		t = 0;
	}

	void Update () 
	{
		if(GameConst.instance.isGameOn)
		{
			if (GameConst.instance.isLevel)
			{
				energySlider.value = Player.instance.energyTot;
			}
            if (Player.instance.energyTot <= (GameConst.instance.energyTime / 3f) + 10f)
			{
				t = Mathf.PingPong(Time.time, 1);
                energyImage.color = Color.Lerp(purple, red, t);
				if (onAlarm == false)
				{
					onAlarm = true;
				}
			}
            else { 
                energyImage.color = Color.Lerp(purple, green, Player.instance.energyTot / (GameConst.instance.energyTime * 2));
            }
			if (Player.instance.energyTot > GameConst.instance.energyTime / 4f && onAlarm)
			{
                energyImage.color = purple;
				onAlarm = false;
			}
		}	

	}
}
