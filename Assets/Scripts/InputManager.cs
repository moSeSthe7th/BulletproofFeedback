using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{

	private Vector2 firstPos;
	private Vector2 lastPos;
	private Vector2 currentSwipe;
	//private bool trackSwipe = true;
	public static InputManager instance = null;
	public direction dirr = direction.none;
	// public Queue<direction> InputBuffer;
	private bool trackSwipe = true;
    private float swipeLimit;


	public enum direction
	{
		right,
		left,
		down,
		none
	}

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
        swipeLimit = 30f;
	}

	public direction directionGetter()
	{
		direction dir = direction.none;
		//#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		if (Input.touchCount > 0) 
		{
			Touch playerFinger = Input.GetTouch(0);

			if (playerFinger.phase==TouchPhase.Began) 
			{
				firstPos = new Vector2(playerFinger.position.x, playerFinger.position.y);
			}
			else if (playerFinger.phase == TouchPhase.Ended && trackSwipe) 
			{
				trackSwipe = false;
				lastPos = new Vector2(playerFinger.position.x, playerFinger.position.y);
				currentSwipe = lastPos - firstPos;

                if (Mathf.Abs (currentSwipe.x) > Mathf.Abs(currentSwipe.y)) 
				{
                    if (currentSwipe.x > swipeLimit) 
					{
						//direction value = direction.right;
						dir = direction.right;
						// dir = 0;
					} 
                    else if (currentSwipe.x < -swipeLimit) 
					{
						//direction value = direction.left;
						dir = direction.left;
						// dir = 1;
					}
				} 
				else if(currentSwipe.y < 10f) 
				{
					//direction value = direction.down;
					dir = direction.down;
					// dir = 2;
				}
				else
				{
					dir = direction.none;
				}
				trackSwipe = true;
			}
			return dir;
		}
		//#elif UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetKeyDown("right"))
		{
			dir = direction.right;

		}

		else if (Input.GetKeyDown("left"))
		{
			dir = direction.left;
		}

		else if (Input.GetKeyDown("down"))
		{
			dir = direction.down;
		}
		else
		{
			dir = direction.none;
		}
		//#endif
		
		return dir;

	}
}
