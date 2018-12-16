using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour {

	/*public BallObject[] balls;
	public int currentBall;*/
	//public Texture m_NewTex;
	Renderer m_Renderer;

	void Start () {
		//changeTexture ();
        //this.GetComponent<MeshRenderer>().enabled = false;
		//if(DataScript.isBallTextureChanged)
        //{
	    changeTexture ();
		//}
        this.GetComponent<MeshRenderer>().enabled = true;
	}

	public void changeTexture(){
		//Debug.Log ("texture changed");
		m_Renderer = GetComponent<Renderer> ();
		m_Renderer.sharedMaterial.SetTexture ("_MainTex", DataScript.ballTexture);
		DataScript.isBallTextureChanged = false;
	}
}
