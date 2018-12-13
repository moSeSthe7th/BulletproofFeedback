using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockScript : Rotatable
{
    public bool didCollided;

    public int[] childPos;

    private int shiftDecider;
    private float shiftProbability;
    private bool willShift;
    private bool isIncreased;
    private int tempRotCounter;
    private bool ShiftLeft;
    private bool ShiftRight;

    private void Start()
    {
        //shiftProbability = 0;
        isIncreased = false;
    }

    public void OnEnable()
    {
        didCollided = false;
        ShouldShift();
        ShiftLeft = false;
        ShiftRight = false;
        tempRotCounter = 0;
    }

    private void LateUpdate()
    {
        if (GameConst.instance.isGameOn)
        {
            if (Player.instance.mode == (int)Player.Mode.bulletProof && !isIncreased)
            {
                BulletModeExitOrEnter(true);
            }
            else if (Player.instance.mode == (int)Player.Mode.normal && isIncreased)
            {
                BulletModeExitOrEnter(false);
            }

            if (ShiftLeft && tempRotCounter < 60)
            {
                this.transform.RotateAround(Vector3.zero, Vector3.forward, 2f);
                tempRotCounter+=2;
            }
            if(ShiftRight && tempRotCounter < 60)
            {
                this.transform.RotateAround(Vector3.zero, Vector3.back, 2f);
                tempRotCounter+=2;
            }
        }
    }

    private void BulletModeExitOrEnter(bool bulletMode) // true for enter false for exit
    {
        if (bulletMode)
        {
            isIncreased = true;
            shiftProbability += 35;
        }
        else
        {
            isIncreased = false;
            shiftProbability -= 35;
        }
    }

    private void ShouldShift() // change blocks shift probabilty
    {
        if(GameConst.instance.gameMode == 0)
        {
            if (GameConst.Level >= 5)
            {
                //shiftProbability = (GameConst.Level * GameConst.Level) / (int)(GameConst.Level / 3.5f);
                shiftProbability = (20f) / (5f/(float)GameConst.Level); //basicly start with 20 and increase 4 by 4 each level
                if (shiftProbability > 70)
                    shiftProbability = 70;
            }
            else
            {
                shiftProbability = 0;
            }
        }
        else if(GameConst.instance.gameMode == 1)
        {
            if (GameConst.Level > 1 && shiftProbability < 70)
            {
                shiftProbability += 7;
            }
        }
        Debug.Log(shiftProbability);
    }

    public void Shift()
    {
        shiftDecider = Random.Range(0, 100);
        Debug.Log(shiftDecider + " " + shiftProbability);
        if (shiftDecider <= shiftProbability)
        {
            willShift = true;
        }
        else
        {
            willShift = false;
        }

        if(this.willShift && this.gameObject.activeInHierarchy)
        {
            willShift = false;
            if (this.transform.GetChild(1).gameObject.GetComponent<BoxCollider>().enabled)
            {
                if (Mathf.Abs(childPos[0] - childPos[1]) == 2)
                {
                    return;
                }
                else if (Mathf.Max(childPos[0], childPos[1]) == 1)
                {
                    ShiftLeft = true;
                }
                else if (Mathf.Max(childPos[0], childPos[1]) == 2)
                {
                    ShiftRight = true;
                }
            }
            else
            {
                if (childPos[0] == 0)
                {
                    ShiftLeft = true;
                }
                else if (childPos[0] == 1)
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        ShiftLeft = true;
                    }
                    else if (r == 1)
                    {
                        ShiftRight = true;
                    }
                }
                else
                {
                    ShiftRight = true;
                }
            }
        }
    }

   /* public void Shift(float pos)
    {
        willShift = false;
        if (pos == BlockCreater.BlockPos[0])
        {
            GameConst.instance.SwipeBlockRight(this.gameObject);
            //StartCoroutine(GameConst.instance.swipeRight(this.gameObject,false,1f,10f));
        }
        else if (pos == BlockCreater.BlockPos[1])
        {
            int r = Random.Range(0, 2);
            if (r == 0)
            {
                //StartCoroutine(GameConst.instance.swipeRight(this.gameObject, false,1f, 10f));
                GameConst.instance.SwipeBlockRight(this.gameObject);
            }
            else if (r == 1)
            {
                //StartCoroutine(GameConst.instance.swipeLeft(this.gameObject, false,1f, 10f));
                GameConst.instance.SwipeBlockLeft(this.gameObject);
            }
        }
        else if (pos == BlockCreater.BlockPos[2])
        {
            GameConst.instance.SwipeBlockLeft(this.gameObject);
            //StartCoroutine(GameConst.instance.swipeLeft(this.gameObject, false,1f, 10f)); 
        }

    }*/
}