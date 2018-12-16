using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsParentScript : MonoBehaviour {

	private Image[] bullets;

    private void OnEnable()
    {
        foreach(CanvasRenderer t in GetComponentsInChildren<CanvasRenderer>())
        {
            t.GetComponent<Image>().enabled = true;
            t.GetComponent<Image>().color = new Color32(255,255,255,200);
        }
    }

    void Start () 
    {
        bullets = gameObject.GetComponentsInChildren<Image>();
	}

    private void LateUpdate()
    {
        if(Player.instance.bulletHits >= 2)
        {  
            float t = Mathf.PingPong(Time.time * 2, 1);
            bullets[2].color = Color.Lerp(Color.white, Color.red, t);
        }
    }

    public void hitInBulletproofMode(int bulletNo)
    {
        bullets[bulletNo].enabled = false;//gameObject.SetActive (false);
	}
}
