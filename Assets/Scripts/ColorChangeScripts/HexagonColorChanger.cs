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
        //bulletProofColor2 = new Color(255,255,115,255);
        foreach (Renderer m in this.transform.GetComponentsInChildren<Renderer>())
        {
            childs[i] = m.gameObject;
            if(i == 0){
                childs[i].GetComponent<Renderer>().material.SetColor("_Color", DataScript.Themes[DataScript.ThemeIndex].HexagonLight);
            }
            else if(i == 1){
                childs[i].GetComponent<Renderer>().material.SetColor("_Color", DataScript.Themes[DataScript.ThemeIndex].HexagonColor);
            }
            normalColor[i] = childs[i].GetComponent<Renderer>().material.GetColor("_Color");
			bulletProofColor[i] = bulletProofColor1;
            i++;
        }
		bulletProofColor[0] = bulletProofColor2;
	}
	

	void LateUpdate () {
        isChanged = ChangeColor(this.transform.childCount, childs, normalColor, bulletProofColor);
	}
}
