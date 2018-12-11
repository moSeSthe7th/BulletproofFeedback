using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomPos
{
    public static int ChooseOne(int a,int b)
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            return a;
        else
            return b;
    }

	public static int RandomPosition(int exRandom, int samenumb)
	{
		int randomIndex = Random.Range(0, 3);
		int random = Random.Range(0, 2);

		if (exRandom == randomIndex && samenumb >= 1)
		{
			if (randomIndex == 0)
			{
				if (random == 0)
					randomIndex += 1;
				else
					randomIndex += 2;
			}
			else if (randomIndex ==  1)
			{
				if (random == 0)
					randomIndex -= 1;
				else
					randomIndex += 1;
			}
			else if (randomIndex == 2)
			{
				if (random == 0)
					randomIndex -= 1;
				else
					randomIndex -= 2;
			}
			return randomIndex;
		}
		else if (exRandom == randomIndex && samenumb < 1)
		{
			return randomIndex;
		}
		else
		{
			return randomIndex;
		}
	}
}
