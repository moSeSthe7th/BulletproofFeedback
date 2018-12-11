using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutEnergyScript : MonoBehaviour
{

    private Slider energySlider;

    //private Image energyImage;

    private void Start()
    {
        energySlider = GetComponent<Slider>();

        energySlider.gameObject.SetActive(true);

        energySlider.minValue = 0;
        energySlider.maxValue = TutorialManager.instance.energyTime;
        energySlider.value = 33;
    }

}
