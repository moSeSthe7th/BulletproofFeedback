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
			normalColor[i] = childs[i].GetComponent<Renderer>().material.GetColor("_Color");
			bulletProofColor[i] = bpColor;
			i++;
		}
	}

	void LateUpdate()
	{
		isChanged = ChangeColor(2, childs, normalColor, bulletProofColor);
	}

    public Color GetColor(){
        return (Player.instance.mode == (int)Player.Mode.normal) ? normalColor[0] : bulletProofColor[0];
    }
}
