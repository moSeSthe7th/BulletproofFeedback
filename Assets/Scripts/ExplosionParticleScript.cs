using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleScript : MonoBehaviour
{
	//public GameObject ezObject;
    private ParticleSystem exlposion;

	/*float x;
	float y;
	float z;*/

    private void Start()
    {
        exlposion = GetComponent<ParticleSystem>();

    }

    public void PlayExplosion(Vector3 pos)
    {
		/*x = pos.x + 2;
		y = pos.y + 5;
		Vector3 ezPos = new Vector3 (x, y, pos.z);
		ezObject.transform.position = ezPos;*/
        this.transform.position = pos;
        exlposion.Play();
    }
    public void StopExplosion(){
        exlposion.Stop();
    }


}
