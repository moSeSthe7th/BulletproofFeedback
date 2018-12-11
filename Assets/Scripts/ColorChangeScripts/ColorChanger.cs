using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    protected bool isChanged;
    private float tilt;
    public Color[] normalColor;
    public Color[] bulletProofColor;


    private void Start()
    {
        isChanged = false;
        tilt = 0;
    }

    public bool ChangeColor(int num,GameObject[] childs,Color[] normalColor,Color[] bulletProofColor)
    {
        if (Player.instance.mode == (int)Player.Mode.bulletProof && !isChanged)
        {
            tilt = Mathf.MoveTowards(tilt, 1f, 0.05f);
            //Debug.Log(t);
            //mainCamera.backgroundColor = Color.Lerp(lightColor, darkColor, t);
            for (int i = 0; i < num;i++)
            {
                childs[i].GetComponent<Renderer>().material.color = Color.Lerp(normalColor[i], bulletProofColor[i], tilt);
            }
            if (tilt >= 1f)
            {
                tilt = 0;
                isChanged = true;
            }
        }
        else if (Player.instance.mode == (int)Player.Mode.normal && isChanged)
        {
            tilt = Mathf.MoveTowards(tilt, 1f, 0.05f);
            //mainCamera.backgroundColor = Color.Lerp(darkColor, lightColor, t);
            //mainCamera.backgroundColor = Color.Lerp(lightColor, darkColor, t);
            for (int i = 0; i < num; i++)
            {
                childs[i].GetComponent<Renderer>().material.color = Color.Lerp(bulletProofColor[i], normalColor[i], tilt);
            }
            if (tilt >= 1f)
            {
                tilt = 0;
                isChanged = false;
            }
        }
        return isChanged;
    }

}
