using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEdgesColorChanger : ColorChanger {
    
    public GameObject[] childs;
	public Color platformEdgesBPColor;
    public Color brightPlatformBPColor;

    private void Start()
    {
        int i = 0;
        childs = new GameObject[this.transform.childCount];
        normalColor = new Color[this.transform.childCount];
        bulletProofColor = new Color[this.transform.childCount];
        //brightPlatformBPColor = new Color(100,116,120,255);
        foreach (Renderer m in this.transform.GetComponentsInChildren<Renderer>())
        {
            childs[i] = m.gameObject;

            if (i == 0)
                childs[i].GetComponent<Renderer>().material.SetColor("_Color", DataScript.Themes[DataScript.ThemeIndex].HexagonColor);
            else if(i== 1)
                childs[i].GetComponent<Renderer>().material.SetColor("_Color", DataScript.Themes[DataScript.ThemeIndex].BrighterRail);
            
            normalColor[i] = childs[i].GetComponent<Renderer>().material.GetColor("_Color");
			bulletProofColor[i] = platformEdgesBPColor;
            i++;
        }
        bulletProofColor[1] = brightPlatformBPColor;
    }

    void LateUpdate()
    {
        isChanged = ChangeColor(this.transform.childCount, childs, normalColor, bulletProofColor);
    }
}
