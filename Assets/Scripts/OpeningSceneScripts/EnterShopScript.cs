using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterShopScript : MonoBehaviour {

	public void entertheShop(){
        SceneManager.LoadScene ("ShopScene",LoadSceneMode.Single);
	}
}
