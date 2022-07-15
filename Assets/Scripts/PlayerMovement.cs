using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PointScript pointScript;
    private bool canMove;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
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
                transform.position += new Vector3(0, 0, 1);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.S) && canMove)
            {
                transform.position += new Vector3(0, 0, -1);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.D) && canMove)
            {
                transform.position += new Vector3(1, 0, 0);
                UseAction();
            }
            if (Input.GetKeyDown(KeyCode.A) && canMove)
            {
                transform.position += new Vector3(-1, 0, 0);
                UseAction();
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
        yield return new WaitForSeconds(1);
        canMove = true;
    }
}
