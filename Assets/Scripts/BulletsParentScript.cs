using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsParentScript : MonoBehaviour {

	//public Image bullet1;
	//public Image bullet2;
	//public Image bullet3;
	private Image[] bullets;

	// Use this for initialization
	void Start () {

		bullets = gameObject.GetComponentsInChildren<Image> ();
		Debug.Log ("buletlenggggtttttthhhhhh" + bullets.Length);
	/*	for (int i = 0; i < transform.childCount; i++) {
			bullets [i].gameObject.SetActive (true);
		}*/

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hitInBulletproofMode(int bulletNo){
		bullets [bulletNo].gameObject.SetActive (false);
	}
}
