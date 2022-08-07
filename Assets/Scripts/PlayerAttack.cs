using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    public override void Update()
    {
        if (gameManager.GetGameState() != GameState.Game || !pointScript.DiceThrower.isTurn || !EnemyFound | !movement.CanMove)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            if (!pointScript.CheckEnoughPoints(attackPointCost))
                return;

            AttackTarget();
        }
    }
}
