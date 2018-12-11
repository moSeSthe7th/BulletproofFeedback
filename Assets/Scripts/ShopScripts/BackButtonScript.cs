using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackButtonScript : MonoBehaviour {

	public GameObject IAPPurchaseOpener;
	public GameObject BallTexPurchaseOpener;
	private OpenerScripts openerScript;
	private BallPurchaseOpener ballPurchaseOpener;

	void Start(){
		openerScript = FindObjectOfType (typeof(OpenerScripts)) as OpenerScripts;
		ballPurchaseOpener = FindObjectOfType (typeof(BallPurchaseOpener)) as BallPurchaseOpener;
	}
	
	public void goToEntranceScene(){
        SceneManager.LoadScene("EnteringScene",LoadSceneMode.Single);
	}

	public void onPressedBackButton(){
		if (!IAPPurchaseOpener.activeSelf && !BallTexPurchaseOpener.activeSelf) {
			ballPurchaseOpener.closeBallTextures ();
            openerScript.closeIAPButtons();
		} else if (!IAPPurchaseOpener.activeSelf) {
			openerScript.closeIAPButtons ();
		} else if (!BallTexPurchaseOpener.activeSelf) {
			ballPurchaseOpener.closeBallTextures ();
		} else {
			goToEntranceScene ();
		}
	}

}
