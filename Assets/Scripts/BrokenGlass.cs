using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

	public GameObject brokenObject;
	public float explosionRadius;
	public float power;
	public float upwardsForce;

	void OnCollisionEnter(Collision collision){
        //GameObject brokenBlock = Instantiate (brokenObject,transform.position,Quaternion.Euler(270,180,0),GameObject.FindWithTag("ALLPLATFORM").transform);
        GameObject brokenBlock = ObjectPooler.SharedInstance.GetPooledObject("BrokenBlock", GameObject.FindWithTag("ALLPLATFORM").transform);
        if(brokenBlock != null)
        {
            brokenBlock.transform.position = this.transform.position;
            brokenBlock.transform.rotation = Quaternion.Euler(270, 180, 0);
            brokenBlock.SetActive(true);
            brokenBlock.GetComponent<DestroyAfterSeconds>().enabled = true;
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        Vector3 explosionPos = collision.contacts[0].point;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, explosionRadius);

		foreach (Collider hit in colliders) {
			if (hit.attachedRigidbody) 
            {
				hit.attachedRigidbody.AddExplosionForce (power, explosionPos, explosionRadius, upwardsForce);
			}
		}
	}

}
