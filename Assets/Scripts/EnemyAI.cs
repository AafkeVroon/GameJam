using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject tileCheckerPrefab;

    public bool NextMove { get { return nextMove; } set { nextMove = value; } }

    private PointScript pointScript;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private bool nextMove;
    private float timer;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        GameObject checker = Instantiate(tileCheckerPrefab);
        checker.GetComponent<FollowObject>().followGameObject = gameObject;
        timer = 0.5f;
    }

    private void Update()
    {
        if (!pointScript.DiceThrower.isTurn || InterfaceManager.Instance.isPaused)
            return;

        if (!nextMove && pointScript.CurrentAmountPoints > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                nextMove = true;
                timer = 0.5f;
                CheckAction();
            }
        }
    }

    private void CheckAction()
    {
        if (enemyAttack.EnemyFound)
        {
            float rnd = Random.Range(0, 101);
            if (rnd < 20)//20
                enemyMovement.Move();
            else
            {
                if (!pointScript.CheckEnoughPoints(2))//attackPointCost
                    enemyMovement.Move();
                else
                    enemyAttack.AttackTarget();
            }
        }
        else
            enemyMovement.Move();
    }
}
