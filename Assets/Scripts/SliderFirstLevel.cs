using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFirstLevel : MonoBehaviour {


	void Start () {
        gameObject.GetComponent<Text> ().text = (GameConst.Level + 1).ToString ();
	}
	

}
