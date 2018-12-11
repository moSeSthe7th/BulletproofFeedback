using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPurchaseOpener : MonoBehaviour {

	public GameObject[] BallTextureObjects;
	public GameObject BallTexPurchaseOpener;
	public GameObject GoldPurchaseOpener;
    public GameObject ScrollViewMain;
    public GameObject ScrollViewBalls;

	private bool isBallTextureObjActive;

	// Use this for initialization
	void Start () {
		isBallTextureObjActive = false;
	}
	
	public void openBallTextures(){
		for (int i = 0; i < BallTextureObjects.Length; i++) {
			BallTextureObjects [i].gameObject.SetActive (true);
		}
		isBallTextureObjActive = true;
		BallTexPurchaseOpener.gameObject.SetActive (false);
		GoldPurchaseOpener.gameObject.SetActive (false);
        ScrollViewMain.SetActive(false);
        ScrollViewBalls.SetActive(true);
	}

	public void closeBallTextures(){
		for (int i = 0; i < BallTextureObjects.Length; i++) {
			BallTextureObjects [i].gameObject.SetActive (false);
		}
		isBallTextureObjActive = false;
		BallTexPurchaseOpener.gameObject.SetActive (true);
		GoldPurchaseOpener.gameObject.SetActive (true);
        ScrollViewMain.SetActive(true);
        ScrollViewBalls.SetActive(false);
	}

	public void BallTextureOpener(){
		if (isBallTextureObjActive) {
			closeBallTextures ();
		} else {
			openBallTextures ();}
	}
}
