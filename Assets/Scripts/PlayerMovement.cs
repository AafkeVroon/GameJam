using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public override void Update()
    {
        if (InterfaceManager.Instance.isPaused || !pointScript.DiceThrower.isTurn)
            return;

        Move();
    }

    public override void Move()
    {
        if (pointScript.CurrentAmountPoints > 0)
        {
            if (Input.GetKeyDown(KeyCode.W) && CanMove && goForward)
            {
                NewPosition(new Vector3(0, 0, 2), Vector3.forward);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.S) && CanMove && goBack)
            {
                NewPosition(new Vector3(0, 0, -2), Vector3.back);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.D) && CanMove && goRight)
            {
                NewPosition(new Vector3(2, 0, 0), Vector3.right);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.A) && CanMove && goLeft)
            {
                NewPosition(new Vector3(-2, 0, 0), Vector3.left);
                UseAction();
            }
        }

        if (!CanMove)
        {
            LerpMove();
        }
    }
}
