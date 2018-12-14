using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEdgesColorChanger : ColorChanger {
    
    public GameObject[] childs;
	public Color platformEdgesColor;

    private void Start()
    {
        int i = 0;
        childs = new GameObject[this.transform.childCount];
        normalColor = new Color[this.transform.childCount];
        bulletProofColor = new Color[this.transform.childCount];
        foreach (Renderer m in this.transform.GetComponentsInChildren<Renderer>())
        {
            childs[i] = m.gameObject;
            normalColor[i] = childs[i].GetComponent<Renderer>().material.GetColor("_Color");
			bulletProofColor[i] = platformEdgesColor;
            i++;
        }
		childs[0].GetComponent<Renderer>().material.SetColor("_Color", DataScript.levelModeHexogenColor);
    }

    void LateUpdate()
    {
        isChanged = ChangeColor(1, childs, normalColor, bulletProofColor);
    }
}
