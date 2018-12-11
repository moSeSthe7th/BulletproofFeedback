using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDesigner
{
	public static int levelUp(int lvl)
	{
		return lvl = lvl + 1;
	}

	//Callback methods
	public static Vector3 blockShrinker(Vector3 scale)
	{
		return scale - new Vector3(0.5f, 0f, 0.5f);
	}

	public static float blockDistDown(float blockDist)
	{
		return blockDist - 2f;
	}

	public static float speedUp(float speed)
	{
        if (speed < Player.instance.initialSpeed + 35f)
            return speed + 2f;
        else
            return speed;
	}
	public static int changeBlockLimit(int blockLimit)
	{
		return blockLimit + 1;
	}

	public static float ChangeShootInterval(float interval)
	{
		if (interval <= 2.8f)
			return interval + .2f;
		else
			return interval;
	}
	public static float changeShootRand(float rand)
	{
		if (rand < 0)
		{
			return rand - 2f;
		}
		else if (rand > 0)
		{
			return rand + 2f;
		}
		return rand;
	}

}
