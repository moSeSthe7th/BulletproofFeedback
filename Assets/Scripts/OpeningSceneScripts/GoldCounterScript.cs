using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCounterScript : MonoBehaviour {

	public Text goldCounterText;

	// Use this for initialization
	void Start () {
		goldCounterText.text = PlayerPrefs.GetInt("Gold", 0).ToString();
	}

}
