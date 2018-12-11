using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour, IRotatable  {

    public Vector3 pos;
    public Vector3 rot;

    public enum CurrentLocation { left, middle, right }; //rightUp, middleUp, leftUp, changing };

    public Vector3 Degree120(float amount)
    {
        return new Vector3(0f, 0f, 120f) / amount;
    }
    public Vector3 Degree90(float amount)
    {
        return new Vector3(0f, 0f, 90f) / amount;
    }
    public Vector3 Degree60(float amount)
    {
        return new Vector3(0f, 0f, 60f) / amount;
    }

    public Vector3 RotateDegree(Vector3 degree)
    {
        return degree;
    }

   /* public bool TurnedEnough(float now,float goal,int dirr)
    {
        if (dirr == (int)InputManager.direction.right)
        {
            if (now < goal)
            {
                return false;
            }
            else if (now >= goal)
            {
                return true;
            }
            return false;
        }
        else if (dirr == (int)InputManager.direction.left)
        {
            if (goal < now)
            {
                return false;
            }
            else if(goal >= now)
            {
                return true;
            }
            return false;
        }
        else
            return false;
    }*/
      
}
