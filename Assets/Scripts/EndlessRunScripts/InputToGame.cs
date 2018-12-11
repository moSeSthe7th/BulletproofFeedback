using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToGame : MonoBehaviour {

    private void Update () {
		
        if (GameConst.instance.isGameOn)
        {
            if (InputManager.instance.directionGetter() == InputManager.direction.right) /*&& transform.position.x < 10*/ //&& (state == (int)State.forward || state == (int)State.ToUp))
            {
                //StartCoroutine(GameConst.instance.swipeRight(this.gameObject, true, 2f,10f));
                //GameConst.instance.SwipePlayerRight();
                PlatformTurnScript.instance.SwipePlatform((int)InputManager.direction.right);
                //state = (int)State.swipingRight;
            }
            else if (InputManager.instance.directionGetter() == InputManager.direction.left) /*&& transform.position.x > -10*/ //&& (state == (int)State.forward || state == (int)State.ToUp))
            {
                //StartCoroutine(GameConst.instance.swipeLeft(this.gameObject, true, 2f,10f));
                //GameConst.instance.SwipePlayerLeft();
                PlatformTurnScript.instance.SwipePlatform((int)InputManager.direction.left);
                //state = (int)State.swipingLeft;
            }
            else if (InputManager.instance.directionGetter() == InputManager.direction.down ) //&& state == (int)State.forward && !GameConst.instance.playerLock)
            {
                Player.instance.SwipeDownInitiate();
            }
        }
	}
}
