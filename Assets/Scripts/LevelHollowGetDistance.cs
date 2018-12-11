using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHollowGetDistance 
{

    //static float dist;

    public static float SetHollow(GameObject hollow, float distance)
    {
        hollow.transform.position += new Vector3(0, 0, distance);
        return distance + 10f;
    }

    public static float GetDistance(Transform t)
    {
        return t.GetChild(t.childCount - 1).transform.position.z - t.GetChild(0).transform.position.z;
    }

    public static float GetLastHollowPos(Transform t)
    {
        return t.GetChild(t.childCount - 1).transform.position.z; 
    }


   /* public static float GetDistance()
    {
        return dist;
    }*/
	
}
