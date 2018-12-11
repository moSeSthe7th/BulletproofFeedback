using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scrollview => content => anchors => y min = -5 ; y max = 1;
/// </summary>



[System.Serializable]
public class BallObject : ScriptableObject {

	public string ballName = "Ball name here";
	public int cost = 500;
	public bool isOwned = false;

	public Texture texture;

}

