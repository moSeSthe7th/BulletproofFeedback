using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTexScaler : MonoBehaviour {

	//public Button button; 
	//public Image image;

	//float height;
	float width;

	void Start () {
		
		//height = Camera.main.orthographicSize * 2;
		Time.timeScale = 3.25f;
//		width = image.GetComponent<RectTransform> ().rect.width;
        gameObject.transform.localScale = Vector3.one * Screen.width /7f;
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
