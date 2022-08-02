using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    private EnemyAI enemyAi;
    private EnemyMovement enemyMovement;

    public override void Start()
    {
        base.Start();
        enemyAi = GetComponent<EnemyAI>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public override IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        pointScript.UsePoint(attackPointCost);
        enemyMovement.CanMove = true;
        IsAttacking = false;
        enemyAi.NextMove = false;
    }
}
