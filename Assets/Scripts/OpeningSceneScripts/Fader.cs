using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

	private Text texttoFade;
	public float speed = 1.0f;
	private float time = 0f;
	private Color textColor;

	// Use this for initialization
	void Start () {
		texttoFade = GetComponent<Text> ();
		textColor = texttoFade.color;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime * speed;
		textColor.a = Mathf.PingPong (time, 0.5f);
		texttoFade.color = textColor;

	}


}
