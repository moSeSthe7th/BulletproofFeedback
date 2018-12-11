using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

	public GameObject brokenObject;
	public float explosionRadius;
	public float power;
	public float upwardsForce;

	void OnCollisionEnter(Collision collision){
		Instantiate (brokenObject, transform.position, transform.rotation);
		gameObject.SetActive (false);

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, explosionRadius);

		foreach (Collider hit in colliders) {
			if (hit.attachedRigidbody) {
				hit.attachedRigidbody.AddExplosionForce (power, explosionPos, explosionRadius, upwardsForce);
			}
		}
	}


}
