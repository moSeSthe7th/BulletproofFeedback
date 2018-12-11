using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBallsScaler : MonoBehaviour {

	//public Button button; 
	//public Image image;

	//float height;
	float width;

	void Start () {

		//height = Camera.main.orthographicSize * 2;
		Time.timeScale = 15f;
		//width = image.GetComponent<RectTransform> ().rect.width;
        gameObject.transform.localScale = Vector3.one * Screen.width / 3f;
	}
}








/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTexScaler : MonoBehaviour {

	float height;
	float width;

	void Start () {
		
		height = Camera.main.orthographicSize * 2;
		width = height * Screen.width/ Screen.height; // basically height * screen aspect ratio
		gameObject.transform.localScale = Vector3.one * height * 10f;
	}
}
*/
