using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringBallSetter : MonoBehaviour {

	public BallObject[] balls;
	public int currentBall;

	// Use this for initialization
	void Awake () {
        //this.GetComponent<MeshRenderer>().enabled = false;
		currentBall = PlayerPrefs.GetInt ("SelectedBall", 0);
		changeBallTexture ();
        //this.GetComponent<MeshRenderer>().enabled = true;
	}
	

	public void changeBallTexture(){
		Debug.Log ("changeballtexturea girdi");
		DataScript.ballTexture = balls [currentBall].texture;
		DataScript.isBallTextureChanged = true;
	}
}
