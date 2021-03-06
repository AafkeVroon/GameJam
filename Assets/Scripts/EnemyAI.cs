using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float desiredWalkTime = 2;
    [SerializeField] private GameObject tileCheckerPrefab;
    [SerializeField] private int attackRange = 1;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackTime = 1;
    [SerializeField] private int attackPointCost = 2;
    [SerializeField] private LayerMask hitLayer;

    public bool goForward;
    public bool goBack;
    public bool goLeft;
    public bool goRight;

    public bool EnemyFound { get { return enemyFound; } set { enemyFound = value; } }

    public bool enemyFound;

    private PointScript pointScript;
    private bool canMove;
    private bool nextMove;
    private bool isAttacking;
    private Animator anim;
    private Vector3 nextPosition;
    private Vector3 currentPosition;
    private float elapsidedTime;
    private float timer;
    private RaycastHit hit;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        anim = GetComponent<Animator>();
        GameObject checker = Instantiate(tileCheckerPrefab);
        checker.GetComponent<FollowObject>().followGameObject = gameObject;
        canMove = true;
        currentPosition = transform.position;
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

        LerpMove();
    }

    private void LerpMove()
    {
        if (!canMove && !isAttacking)
        {
            elapsidedTime += Time.deltaTime;
            float percentage = elapsidedTime / desiredWalkTime;
            transform.position = Vector3.Lerp(currentPosition, nextPosition, percentage);
            if (percentage > 0.97f)
            {
                transform.position = new Vector3(nextPosition.x, nextPosition.y, nextPosition.z);
                currentPosition = transform.position;
            }
        }
    }

    private void UseAction()
    {
        canMove = false;
        //anim.SetTrigger("Hop");
        //pointScript.UsePoint(1);
        StartCoroutine(SetCanMoveToTrue());
    }

    private void CheckAction()
    {
        if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit))
        {
            if (EnemyFound)
            {
                float rnd = Random.Range(0, 101);
                Debug.Log(rnd + "RNDNRNDRNDRNDRNDRNDRND");
                if (rnd < 101)//20
                {
                    Move();
                }
                else
                {
                    //Attack
                    Attack();
                    Debug.Log(",,.,..,,.,..,..,.,,.,..,,.,.,.,.,..,.,,.`1");
                }
            }
            else
            {
                Debug.Log("1823784789438793247923547903245790");
                Move();
            }
        }
    }

    private void Attack()
    {
        if (!pointScript.CheckEnoughPoints(attackPointCost))
            return;

        canMove = false;
        isAttacking = true;
        if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, 10, hitLayer))
        {
            anim.SetTrigger("Attack");
            hit.collider.gameObject.GetComponent<Health>().ModifyHealth(-attackDamage);
            StartCoroutine(AttackCooldown());
        }
            Debug.Log("ICANATTACKHELPMEEEEE" );
    }

    private void Move()
    {
        float direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                if (canMove && goForward)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 2);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    UseAction();
                }
                else
                    Move();
                break;
            case 1:
                if (canMove && goBack)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 2);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    UseAction();
                }
                else
                    Move();
                break;
            case 2:
                if (canMove && goRight)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x + 2, currentPosition.y, currentPosition.z);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.right);
                    UseAction();
                }
                else
                    Move();
                break;
            case 3:
                if (canMove && goLeft)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x - 2, currentPosition.y, currentPosition.z);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.left);
                    UseAction();
                }
                else
                    Move();
                break;
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        pointScript.UsePoint(attackPointCost);
        canMove = true;
        isAttacking = false;
        nextMove = false;
    }

    private IEnumerator SetCanMoveToTrue()
    {
        yield return new WaitForSeconds(desiredWalkTime + 0.5f);
        pointScript.UsePoint(1);
        elapsidedTime = 0;
        canMove = true;
        nextMove = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
