using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutHollowFillScript : Rotatable
{

    private int QueNumber;

    private void Start()
    {
        rot = this.transform.eulerAngles;
    }

    private void LateUpdate()
    {

        if (TutPlayer.instance.transform.position.z > this.transform.position.z + 25f)//&& !TutorialManager.instance.playerLock)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (TutorialManager.instance.Hollows.Capacity) * TutorialManager.instance.hollowUpdateDist);
            //this.transform.Rotate(new Vector3(0, 0, -60f), Space.Self);
        }
    }

    public void SetQueNumber(int num)
    {
        QueNumber = num;
    }

}
