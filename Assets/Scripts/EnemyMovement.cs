using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private EnemyAI enemyAi;

    private void Start()
    {
        enemyAi = GetComponent<EnemyAI>();
    }

    public override void Update()
    {
        if (!pointScript.DiceThrower.isTurn || gameManager.GetGameState() != GameState.Game)
            return;

        if (!CanMove)
            LerpMove();
    }

    public override void Move()
    {
        if (!pointScript.CheckEnoughPoints(1))
            return;

        float direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                if (CanMove && goForward)//for
                {
                    NewPosition(new Vector3(0, 0, 2), Vector3.forward);
                    UseAction();
                }
                else
                    Move();
                break;
            case 1:
                if (CanMove && goBack)//back
                {
                    NewPosition(new Vector3(0, 0, -2), Vector3.back);
                    UseAction();
                }
                else
                    Move();
                break;
            case 2:
                if (CanMove && goRight)//right
                {
                    NewPosition(new Vector3(2, 0, 0), Vector3.right);
                    UseAction();
                }
                else
                    Move();
                break;
            case 3:
                if (CanMove && goLeft)//left
                {
                    NewPosition(new Vector3(-2, 0, 0), Vector3.left);
                    UseAction();
                }
                else
                    Move();
                break;
        }
    }

    public override IEnumerator SetCanMoveToTrue()
    {
        yield return new WaitForSeconds(desiredWalkTime + 0.15f);
        pointScript.UsePoint(1);
        elapsidedTime = 0;
        CanMove = true;
        enemyAi.NextMove = false;
    }
}
