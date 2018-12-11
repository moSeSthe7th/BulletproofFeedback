using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimator : MonoBehaviour {
    private void Start()
    {
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    void LateUpdate () {
		this.transform.Rotate (5, 0, 0);
	}
}
