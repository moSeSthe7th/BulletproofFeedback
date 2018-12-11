using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertiseTimer : MonoBehaviour {
 
    void Update () 
    {
        this.gameObject.GetComponent<Image>().fillAmount -= Time.unscaledDeltaTime / 5f;
	}
}
