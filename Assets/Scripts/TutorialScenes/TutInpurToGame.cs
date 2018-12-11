using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutInpurToGame : MonoBehaviour
{

    private void Update()
    {

        if (TutorialManager.instance.isGameOn)
        {
            if (InputManager.instance.directionGetter() == InputManager.direction.right && !TutorialManager.instance.playerLockTurn) /*&& transform.position.x < 10*/ //&& (state == (int)State.forward || state == (int)State.ToUp))
            {
                //StartCoroutine(TutorialManager.instance.swipeRight(this.gameObject, true, 2f,10f));
                //TutorialManager.instance.SwipePlayerRight();
                TutPlatformTurnScript.instance.SwipePlatform((int)InputManager.direction.right);
                TutorialManager.instance.didTurn = true;
                //state = (int)State.swipingRight;
            }
            else if (InputManager.instance.directionGetter() == InputManager.direction.left && !TutorialManager.instance.playerLockTurn) /*&& transform.position.x > -10*/ //&& (state == (int)State.forward || state == (int)State.ToUp))
            {
                //StartCoroutine(TutorialManager.instance.swipeLeft(this.gameObject, true, 2f,10f));
                //TutorialManager.instance.SwipePlayerLeft();
                TutPlatformTurnScript.instance.SwipePlatform((int)InputManager.direction.left);
                TutorialManager.instance.didTurn = true;
                //state = (int)State.swipingLeft;
            }
            else if (InputManager.instance.directionGetter() == InputManager.direction.down) //&& state == (int)State.forward && !TutorialManager.instance.playerLock)
            {
                TutPlayer.instance.SwipeDownInitiate();
            }
        }
    }
}
