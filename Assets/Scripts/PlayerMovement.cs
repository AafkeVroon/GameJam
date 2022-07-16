using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float desiredWalkTime = 2;

    private PointScript pointScript;
    private bool canMove;
    private Animator anim;
    private Vector3 nextPosition;
    private float elapsidedTime;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        anim = GetComponent<Animator>();
        canMove = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (pointScript.CurrentAmountPoints > 0)
        {
            if(Input.GetKeyDown(KeyCode.W) && canMove)
            {
                nextPosition = transform.position += new Vector3(0, 0, 2);
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.S) && canMove)
            {
                nextPosition = transform.position += new Vector3(0, 0, -2);
                transform.rotation = Quaternion.LookRotation(Vector3.back);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.D) && canMove)
            {
                nextPosition = transform.position += new Vector3(2, 0, 0);
                transform.rotation = Quaternion.LookRotation(Vector3.right);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.A) && canMove)
            {
                nextPosition = transform.position += new Vector3(-2, 0, 0);
                transform.rotation = Quaternion.LookRotation(Vector3.left);
                UseAction();
            }

            if (!canMove)
            {
                elapsidedTime += Time.deltaTime;
                float percentage = elapsidedTime / desiredWalkTime;
                Debug.Log(percentage);
                transform.position = Vector3.Lerp(transform.position, nextPosition, percentage);
            }
        }
    }

    private void UseAction()
    {
        canMove = false;
        pointScript.UsePoint(1);
        StartCoroutine(SetCanMoveToTrue());
    }

    private IEnumerator SetCanMoveToTrue()
    {
        yield return new WaitForSeconds(desiredWalkTime + 0.05f);
        canMove = true;
    }
}
