using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    Waiting,
    Forward,
    Backward,
    Left,
    Right
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float desiredWalkTime = 2;
    [SerializeField] private GameObject tileCheckerPrefab;
    [SerializeField] private Movement movement;
    [SerializeField] private int attackRange = 1;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private int attackPointCost = 2;

    public bool goForward;
    public bool goBack;
    public bool goLeft;
    public bool goRight;

    private PointScript pointScript;
    private bool canMove;
    private bool nextMove;
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
        if (!pointScript.DiceThrower.isTurn)
            return;

        if (!nextMove && pointScript.CurrentAmountPoints > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //Debug.Log("qQQQQqqqqQQQQQQQqqqqqqqQQQQQQQQ1");
                nextMove = true;
                timer = 0.5f;
                CheckAction();
            }
        }

        LerpMove();
    }

    private void LerpMove()
    {
        if (!canMove)
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
        //Debug.Log("JFHFBFJFFJFJFJFJFJFJFJFJFJ");
    }

    private void CheckAction()
    {
        if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                float rnd = Random.Range(0, 101);
                if (rnd < 20)
                {
                    Move();
                }
                else
                {
                    //Attack

                }
            }
            else
            {
                //Debug.Log("0000009998888111111111");
                Move();
            }
        }
    }

    private void Move()
    {
        float direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                if (canMove && goForward)
                {
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

    private IEnumerator SetCanMoveToTrue()
    {
        yield return new WaitForSeconds(desiredWalkTime + 0.5f);
        pointScript.UsePoint(1);
        elapsidedTime = 0;
        canMove = true;
        nextMove = false;
        yield return new WaitForSeconds(0.5f);
        if (pointScript.CurrentAmountPoints > 0)
        {
            canMove = true;
            nextMove = false;
        }
        //Debug.Log("WHYDONTYOUWORRHRBDFHDFHF");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
