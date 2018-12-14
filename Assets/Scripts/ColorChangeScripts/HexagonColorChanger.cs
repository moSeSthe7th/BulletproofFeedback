using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonColorChanger : ColorChanger {
    
    public GameObject[] childs;
	public Color bulletProofColor1;
	public Color bulletProofColor2;

	void Start () 
    {
        int i = 0;
        childs = new GameObject[this.transform.childCount];
        normalColor = new Color[this.transform.childCount];
        bulletProofColor = new Color[this.transform.childCount];
        foreach (Renderer m in this.transform.GetComponentsInChildren<Renderer>())
        {
            childs[i] = m.gameObject;
			childs[i].GetComponent<Renderer>().material.SetColor("_Color",DataScript.levelModeHexogenColor);
            normalColor[i] = childs[i].GetComponent<Renderer>().material.GetColor("_Color");
			bulletProofColor[i] = bulletProofColor1;
            i++;
        }
		bulletProofColor[0] = bulletProofColor2;

	}
	

	void LateUpdate () {
        isChanged = ChangeColor(2, childs, normalColor, bulletProofColor);
	}
}
