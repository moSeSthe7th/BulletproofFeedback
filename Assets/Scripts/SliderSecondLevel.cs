using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSecondLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text> ().text = (GameConst.Level).ToString ();
	}
}
