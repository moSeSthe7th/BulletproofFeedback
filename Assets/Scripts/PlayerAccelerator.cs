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
       if(GameConst.Level == 1)
        {
            return playerInitSpeed;
        }
        else
        {
            return playerInitSpeed + ((GameConst.Level - 1) * LevelDesigner.speedUp(0));
        }
    }
}
