using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private List<Vector3> directions;
    [SerializeField] private List<int> diceRotation;

    private Rigidbody rb;
    private bool hasNumber;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(directions.Count == 0)
        {
            directions.Add(Vector3.up);
            diceRotation.Add(0);//1
            directions.Add(Vector3.down);
            diceRotation.Add(6);//6

            directions.Add(Vector3.forward);
            diceRotation.Add(2);//2
            directions.Add(Vector3.back);
            diceRotation.Add(5);//5

            directions.Add(Vector3.right);
            diceRotation.Add(3);//3
            directions.Add(Vector3.left);
            diceRotation.Add(4);//4
        }

        //GetNumber();
    }

    private void Update()
    {
        if (hasNumber)
            return;

        if (rb.velocity.magnitude <= 0.06f)
        {
            hasNumber = true;
            GetNumber();
            Debug.Log(transform.up);
        }
    }

    private int GetNumber()
    {
        int currentValue = 0;
        Vector3 vectorTest = transform.up;

        if(vectorTest == Vector3.up)
        {

        }
        return 1;
    }
}
