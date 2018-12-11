using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotatable  
{
    Vector3 RotateDegree(Vector3 degree);
    //bool TurnedEnough(float now,float goal,int dirr);
}
