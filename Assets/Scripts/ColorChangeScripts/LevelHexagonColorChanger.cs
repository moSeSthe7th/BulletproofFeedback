using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHexagonColorChanger : ColorChanger {
    
    public GameObject[] childs;
	public Color bpColor;


    private void Start()
    {
        int i = 0;
        childs = new GameObject[this.transform.childCount];
        normalColor = new Color[this.transform.childCount];
        bulletProofColor = new Color[this.transform.childCount];

		Color nColor = DataScript.levelModeHexogenColor;
        bpColor = new Color32(67, 102, 117, 255);

        foreach (Renderer m in this.transform.GetComponentsInChildren<Renderer>())
        {
            childs[i] = m.gameObject;
			childs[i].GetComponent<Renderer>().material.SetColor("_Color", DataScript.levelModeHexogenColor);
            normalColor[i] = nColor;
			bulletProofColor[i] = bpColor;
            i++;
        }
    }

    void LateUpdate()
    {
        isChanged = ChangeColor(2, childs, normalColor, bulletProofColor);
    }
}
