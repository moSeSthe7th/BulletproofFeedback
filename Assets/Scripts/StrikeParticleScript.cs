using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeParticleScript : MonoBehaviour {

	private ParticleSystem strikePS;

	void Start () {
		strikePS = GetComponent<ParticleSystem> ();
	}


	public void setStrikePSPos(Vector3 strikePSPos){
		transform.position = strikePSPos;
		strikePS.Play();
	}
}
