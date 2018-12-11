using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	//public TextureChanger textureChanger;
	public int ballNumber;
	public ShopCanvasScript shopCanvasScript;
	private bool[] isBallOwnedBool;
    private List<ShopButton> shopButtons;

	//private UIScript uIScript;

	//public Text name;
	public Text buttonText;

	// Use this for initialization
	void Start () {
        //textureChanger = FindObjectOfType (typeof(TextureChanger)) as TextureChanger;
        //uIScript = FindObjectOfType(typeof(UIScript)) as UIScript;
        //shopButtons = FindObjectsOfType(typeof(ShopButton)) as ShopButton;
		setButton ();
	}

    private void LateUpdate()
    {
        setButton();
    }

    void setButton(){
        if (shopCanvasScript.balls [ballNumber].isOwned && PlayerPrefs.GetInt("SelectedBall") != ballNumber) 
        {
            buttonText.text = "SELECT";
		}
        else if(shopCanvasScript.balls[ballNumber].isOwned && PlayerPrefs.GetInt("SelectedBall") == ballNumber)
        {
            buttonText.text = "SELECTED";
        }
        else {
			string costString = shopCanvasScript.balls [ballNumber].cost.ToString ();
			buttonText.text = costString;
		}

	}

	public void onClick(){
		int goldAmount = PlayerPrefs.GetInt ("Gold", 0);
		if (shopCanvasScript.balls [ballNumber].isOwned) {
			//shopCanvasScript.currentBall = ballNumber;
			//shopCanvasScript.changeBallTexture ();
			PlayerPrefs.SetInt ("SelectedBall", ballNumber);
		} else if (!shopCanvasScript.balls [ballNumber].isOwned && goldAmount >= shopCanvasScript.balls [ballNumber].cost) {
			isBallOwnedBool = PlayerPrefsX.GetBoolArray ("isBallOwnedArray",false,14);
			shopCanvasScript.balls [ballNumber].isOwned = true;
			goldAmount -= shopCanvasScript.balls [ballNumber].cost;
			PlayerPrefs.SetInt ("Gold", goldAmount);
			shopCanvasScript.setGoldText ();
			isBallOwnedBool [ballNumber] = true;
			PlayerPrefsX.SetBoolArray ("isBallOwnedArray", isBallOwnedBool);
            PlayerPrefs.SetInt("SelectedBall", ballNumber);
		}
		setButton ();

	}


}
