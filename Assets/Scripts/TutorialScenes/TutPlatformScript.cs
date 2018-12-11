using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPlatformScript : MonoBehaviour
{

    public static float SetPlatform(GameObject edge, float distance)
    {
        edge.transform.position += new Vector3(0, 0, distance);
        return distance + 20f;
    }

    private void LateUpdate()
    {
        if (TutPlayer.instance.transform.position.z > this.transform.position.z + 40f)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 360f);
        }
    }
}
