using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorChanger : ColorChanger {


	public GameObject[] childs;
	public Color bpColor;

	private void Start()
	{
		int i = 0;
		childs = new GameObject[this.transform.childCount];
		normalColor = new Color[this.transform.childCount];
		bulletProofColor = new Color[this.transform.childCount];
		foreach (Renderer m in this.transform.GetComponentsInChildren<Renderer>())
		{
			
			childs[i] = m.gameObject;
			childs[i].GetComponent<Renderer>().material.SetColor("_Color",DataScript.levelModeBlockColor);
			normalColor[i] = childs[i].GetComponent<Renderer>().material.GetColor("_Color");
			bulletProofColor[i] = bpColor;
			i++;
		}
	}

	void Update()
	{
        isChanged = ChangeColor(this.transform.childCount, childs, normalColor, bulletProofColor);
	}

    public Color GetColor(){
        return (Player.instance.mode == (int)Player.Mode.normal) ? normalColor[0] : bulletProofColor[0];
    }
}
