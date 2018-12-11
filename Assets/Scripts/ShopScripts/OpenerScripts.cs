using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenerScripts : MonoBehaviour {

	public GameObject IAP100GoldObj;
	public GameObject IAP500GoldObj;
	public GameObject IAP1000GoldObj;
	public GameObject IAPPurchaseOpener;
	public GameObject BallTexPurchaseOpener;
	private bool isIAPButtonsActive;
    public GameObject ScrollViewMain;
    public GameObject ScrollViewCoins;


	// Use this for initialization
	void Start () {
		isIAPButtonsActive = false;
	}
	


	public void openIAPButtons(){
		IAP100GoldObj.gameObject.SetActive (true);
		IAP500GoldObj.gameObject.SetActive (true);
		IAP1000GoldObj.gameObject.SetActive (true);
		IAPPurchaseOpener.gameObject.SetActive (false);
		BallTexPurchaseOpener.gameObject.SetActive (false);
		isIAPButtonsActive = true;
        ScrollViewCoins.SetActive(true);
        ScrollViewMain.SetActive(false);
	}

	public void closeIAPButtons(){
		IAP100GoldObj.gameObject.SetActive (false);
		IAP500GoldObj.gameObject.SetActive (false);
		IAP1000GoldObj.gameObject.SetActive (false);
		IAPPurchaseOpener.gameObject.SetActive (true);
		BallTexPurchaseOpener.gameObject.SetActive (true);
		isIAPButtonsActive = false;
        ScrollViewCoins.SetActive(false);
        ScrollViewMain.SetActive(true);
	}

	public void IAPButtonOpener(){
		if (isIAPButtonsActive) {
			closeIAPButtons ();
		} else {
			openIAPButtons ();
		}
	
	}
}
