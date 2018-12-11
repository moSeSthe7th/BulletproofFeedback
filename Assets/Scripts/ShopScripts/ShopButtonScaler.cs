using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScaler : MonoBehaviour {

	public Image image;

	float width;
	Vector3 scaler;

	void Start () {

		//height = Camera.main.orthographicSize * 2;
		width = image.GetComponent<RectTransform> ().rect.width;
		scaler = new Vector3(1f,.4f,1/(width * 50));

		gameObject.transform.localScale = scaler * width / 300f ;
	}
}
