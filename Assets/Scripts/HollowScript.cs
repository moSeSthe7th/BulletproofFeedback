using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowScript : MonoBehaviour {

    public static float SetHollow(GameObject hollow, float distance)
    {
        hollow.transform.position += new Vector3(0, 0, distance);
        return distance + 50f;
    }

    private void LateUpdate()
    {
        if (Player.instance.transform.position.z > this.transform.position.z + 20f)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 750f);
        }
    }

}
