using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowFillScript : Rotatable {

    private int QueNumber;

    private void Start()
    {
        rot = this.transform.eulerAngles;
    }

    private void LateUpdate()
    {
       
        if (Player.instance.transform.position.z > this.transform.position.z + 25f && transform.position.z + (GameConst.instance.Hollows.Capacity) * GameConst.instance.hollowUpdateDist <= GameConst.instance.LastPosOfArray)//&& !GameConst.instance.playerLock)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (GameConst.instance.Hollows.Capacity) * GameConst.instance.hollowUpdateDist);
            //this.transform.Rotate(new Vector3(0, 0, -60f), Space.Self);
        }
    }

    public void SetQueNumber(int num)
    {
        QueNumber = num;
    }

}
