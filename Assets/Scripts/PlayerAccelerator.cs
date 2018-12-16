using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAccelerator{
    

    public static bool PlayerFastEnough(float playerSpeed,float playerSpeedMax)
    {
        if(playerSpeed >= playerSpeedMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PlayerNormalSpeed(float playerSpeed, float playerSpeedNormal)
    {
        if(playerSpeed <= playerSpeedNormal)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public static float GetPlayerNormalSpeed(float playerInitSpeed)
    {
        if(GameConst.instance.gameMode == 0)
        {
            float coefficient = (GameConst.Level != 1) ? (float)(GameConst.Level - 1) / 17 : 0;
            return playerInitSpeed + (int)(35 * (1 - Mathf.Exp(-(coefficient))));
        }
        else if(GameConst.instance.gameMode == 1)
        {
            return playerInitSpeed + ((GameConst.Level - 1) * LevelDesigner.speedUp(0));
        }
        else
        {
            return playerInitSpeed; 
        }
    }
}
