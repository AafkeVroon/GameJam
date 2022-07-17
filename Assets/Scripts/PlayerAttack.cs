using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackRange = 1;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private float attackTime = 1;
    [SerializeField] private int attackPointCost = 2;

    public bool EnemyFound { get { return enemyFound; } set { enemyFound = value; } }

    public bool enemyFound;
    private PointScript pointScript;
    private PlayerMovement playerMovement;
    private Animator anim;
    private RaycastHit hit;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!pointScript.DiceThrower.isTurn || !EnemyFound | !playerMovement.CanMove)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            playerMovement.CanMove = false;
        }
    }
}
