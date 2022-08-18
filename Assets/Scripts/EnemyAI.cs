using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool NextMove { get { return nextMove; } set { nextMove = value; } }

    private PointScript pointScript;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private GameManager gameManager;
    private bool nextMove;
    private float timer;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        gameManager = GameManager.Instance;
        timer = 0.5f;
    }

    private void Update()
    {
        if (!pointScript.DiceThrower.isTurn || gameManager.GetGameState() != GameState.Game)
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
            Debug.Log("ENEMYFOUNNDDD");
            float rnd = Random.Range(0, 101);
            if (rnd < 20)//20
            {
                Debug.Log("TOLOWNUMBERGOMOVEW");
                enemyMovement.Move();
            }
            else
            {
                if (!pointScript.CheckEnoughPoints(2))//attackPointCost
                {
                    Debug.Log("NOMOREPOINTASTOATTACKSOMOVEME");
                    enemyMovement.Move();
                }
                else
                {
                    Debug.Log("ATTACKMATYEYKILLHIM");
                    enemyAttack.AttackTarget();
                }
            }
        }
        else
        {
            Debug.Log("GOTTATOMOVEAWAY");
            enemyMovement.Move();
        }
    }
}
