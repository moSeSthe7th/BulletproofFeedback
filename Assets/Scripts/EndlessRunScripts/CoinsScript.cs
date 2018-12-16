using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript{

    /*private static float SetCoin(GameObject coin, float distance)
    {
        coin.transform.position = new Vector3(0, 0, distance);
        return distance + 10f;
    }*/

    public static void IncreaseAndActivateCoins(GameObject Coins)
    {
        //int coinAmount = Coins.transform.childCount;
        Coins.transform.localPosition = new Vector3(0f, 0f, GameConst.instance.LastPosOfArray + 120f);
        foreach (Transform c in Coins.GetComponentInChildren<Transform>())
        {
            if (!c.gameObject.activeInHierarchy)
            {
                c.gameObject.SetActive(true);
            }
        }
        if (GameConst.Level % 2 == 0 && GameConst.instance.gameMode == 1)
        {
            ObjectPooler.SharedInstance.GetPooledObject("Coin",Coins.transform);
        }
        InitiliazeCoins(Coins);
        LevelCoins(Coins);
    }

    static void InitiliazeCoins(GameObject Coins)
    {
        float coinDist = 0f;
        for (int i = 0; i < Coins.transform.childCount ; i++)
        {
            Coins.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(0, 0, coinDist);
            coinDist += 33f;
        }
        //CoinsSetter(Coins);
    }

    static void LevelCoins(GameObject Coins)
    {
        int rand = Random.Range(0, 3);
        int r = (rand == 0) ? r = RandomPos.ChooseOne(1, 2) : (rand == 1) ? r = RandomPos.ChooseOne(0, 2) : r = RandomPos.ChooseOne(0, 1);
        for (int i = 0; i < Coins.transform.childCount; i++)
        {
            
            if(i <= Coins.transform.childCount / 2)
            {
                Coins.transform.GetChild(i).transform.localPosition = new Vector3(PlatformTurnScript.instance.CoinAndBulletPos[rand].x,PlatformTurnScript.instance.CoinAndBulletPos[rand].y, Coins.transform.GetChild(i).transform.localPosition.z);
            }
            else if(i < Coins.transform.childCount)
            {
                Coins.transform.GetChild(i).transform.localPosition = new Vector3(PlatformTurnScript.instance.CoinAndBulletPos[r].x,PlatformTurnScript.instance.CoinAndBulletPos[r].y, Coins.transform.GetChild(i).transform.localPosition.z + 60f);
            }
            else{
                Debug.LogError("Coin atarken hata!!!!");
            }
        }
    }

  /*  static void CoinsSetter(GameObject Coins,float posX)
    {
        int randCoinPos = Random.Range(0, 3);
       
        if (randCoinPos == 0)
        {
            Coins.transform.position = new Vector3(PlatformTurnScript.instance.BlockPos[randCoinPos].x + 6.3f, PlatformTurnScript.instance.BlockPos[randCoinPos].y + 3.5f, GameConst.instance.LastPosOfArray + 90f);
            float t = RandomPos.ChooseOne(0, posX);
            for (int i = 0; i < Coins.transform.childCount / 3; i++)
            {   
                if(i > Coins.transform.childCount / 3)
                {
                    Coins.transform.GetChild(i).transform.position = new Vector3(PlatformTurnScript.instance.CoinPos[])
                }

                if(t == 0)
                {
                    Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position = new Vector3(t, Player.instance.transform.position.y, Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position.z + 25f);
                }
                else
                    Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position = new Vector3(t, -1.5f, Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position.z + 25f);
            }
            if (t == 0)
            {
                float t2 = RandomPos.ChooseOne(-1 * posX, posX);
                for (int i = 0; i < (Coins.transform.childCount / 3); i++)
                {
                    Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, -1.5f,Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                }
            }
            else if (t == posX)
            {
                float t2 = RandomPos.ChooseOne(-1 * posX, 0);
                for (int i = 0; i < (Coins.transform.childCount / 3); i++)
                {
                    if(t2 == 0)
                    {
                        Coins.transform.GetChild((Coins.transform.childCount / 3) * 2 + i).transform.position = new Vector3(t2, Player.instance.transform.position.y, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                    }
                    else
                        Coins.transform.GetChild((Coins.transform.childCount / 3) * 2 + i).transform.position = new Vector3(t2, -1.5f, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                }
            }
        }
        else if (randCoinPos == 1)
        {
            float t = RandomPos.ChooseOne(-1 * posX, posX);
            for (int i = 0; i < Coins.transform.childCount / 3; i++)
            {
                Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position =new Vector3(t, -1.5f,Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position.z + 25f);
            }
            if (t == -1 * posX)
            {
                float t2 = RandomPos.ChooseOne(0, posX);
                for (int i = 0; i < (Coins.transform.childCount / 3); i++)
                {
                    if (t2 == 0)
                    {
                        Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, Player.instance.transform.position.y, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                    }
                    else
                        Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, -1.5f, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                }
            }
            else if (t == posX)
            {
                float t2 = RandomPos.ChooseOne(0, -1 * posX);
                for (int i = 0; i < (Coins.transform.childCount / 3); i++)
                {
                    if (t2 == 0)
                    {
                        Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, Player.instance.transform.position.y, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                    }
                    else
                        Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, -1.5f, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                }
            }
        }
        else if (randCoinPos == 2)
        {
            float t = RandomPos.ChooseOne(-1 * posX, 0);
            for (int i = 0; i < Coins.transform.childCount / 3; i++)
            {
                if (t == 0)
                {
                    Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position = new Vector3(t, Player.instance.transform.position.y, Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position.z + 25f);
                }
                else
                    Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position = new Vector3(t, -1.5f, Coins.transform.GetChild((Coins.transform.childCount / 3) + i).transform.position.z + 25f);
            }
            if (t == -1 * posX)
            {
                float t2 = RandomPos.ChooseOne(0, posX);
                for (int i = 0; i < (Coins.transform.childCount / 3); i++)
                {
                    if (t2 == 0)
                    {
                        Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, Player.instance.transform.position.y, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                    }
                    else
                        Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position = new Vector3(t2, -1.5f, Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                }
            }
            else if (t == 0)
            {
                float t2 = RandomPos.ChooseOne(-1 * posX, posX);
                for (int i = 0; i < (Coins.transform.childCount / 3); i++)
                {
                    Coins.transform.GetChild((Coins.transform.childCount / 3) * 2 + i).transform.position = new Vector3(t2, -1.5f,Coins.transform.GetChild(((Coins.transform.childCount / 3) * 2) + i).transform.position.z + 50f);
                }
            }
        }
    }*/

}
