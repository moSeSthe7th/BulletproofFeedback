using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBlockScaler : MonoBehaviour {

	//public Button buttonForSBScaler;
	//public Image image;

	//float height;
	float width;
	Vector3 scaler;

	void Start () {

		//height = Camera.main.orthographicSize * 2;
		scaler = new Vector3(3.5f,0.5f,7f);
		//width = image.GetComponent<RectTransform> ().rect.width;
        gameObject.transform.localScale = scaler * Screen.width /8f;
	}
}
