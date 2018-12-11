using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreater
{
    public static int exRand = 3;
    public static bool beforeActivated = false;

    public static void SetDistance(GameObject block)
    {
        block.transform.position = new Vector3(0f, 0f, GameConst.instance.İnitBlockDist);
        GameConst.instance.LastPosOfArray = GameConst.instance.İnitBlockDist;
        GameConst.instance.İnitBlockDist += GameConst.instance.BlockDistUpdate;
    }

    public static int CreateBlock(GameObject pooledBlock,int doubleBlockNum)
    {
        int tempEx = exRand;
        exRand = RandomPos.RandomPosition(exRand, GameConst.instance.SameLineBlock);
        if (tempEx == exRand)
        {
            GameConst.instance.SameLineBlock += 1;
        }
        else
        {
            GameConst.instance.SameLineBlock = 0;
        }
        Random.InitState(Random.Range(0,100));
        int r =(doubleBlockNum == 0) ? 0 :(!beforeActivated) ? Random.Range(0, GameConst.Level + 1) :(beforeActivated) ? Random.Range(0,GameConst.Level + 3): 1;
        if(doubleBlockNum <= (GameConst.instance.blocks.Length * 3) / (GameConst.Level ) && r < (GameConst.Level / 3f))
        {
            beforeActivated = true;
            pooledBlock.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            pooledBlock.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
            doubleBlockNum++;
        }
        else if(pooledBlock.transform.GetChild(1).gameObject.activeSelf)
        {
            beforeActivated = false;
            pooledBlock.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            pooledBlock.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        }

        if (pooledBlock != null)
        {
            if(!pooledBlock.activeInHierarchy)
            {
                pooledBlock.SetActive(true); 
            }
            tempEx = exRand;
            int i = 0;
            pooledBlock.transform.localEulerAngles = Vector3.zero;
            pooledBlock.GetComponent<BlockScript>().OnEnable();
            pooledBlock.gameObject.GetComponent<BlockScript>().childPos = new int[pooledBlock.transform.childCount];
            foreach(MeshFilter b in pooledBlock.GetComponentsInChildren<MeshFilter>())
            {
                pooledBlock.gameObject.GetComponent<BlockScript>().childPos[i] = tempEx;
                b.gameObject.transform.localPosition = PlatformTurnScript.instance.BlockPos[tempEx];
                b.gameObject.transform.localRotation = Quaternion.Euler(PlatformTurnScript.instance.BlockRot[tempEx]);
                tempEx = (tempEx == 0) ? tempEx = RandomPos.ChooseOne(1, 2) : (tempEx == 1) ? tempEx = RandomPos.ChooseOne(0, 2) : tempEx = RandomPos.ChooseOne(0, 1);
                i++;
            }
        }
        return doubleBlockNum;
    }
}
