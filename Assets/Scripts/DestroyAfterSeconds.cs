using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public float second;
	
	void OnEnable () 
    {
        Destroy(gameObject, second);
	}
}
