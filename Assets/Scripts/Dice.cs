using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private List<Vector3> directions;
    [SerializeField] private List<int> diceValue;

    private Rigidbody rb;
    private bool hasNumber;

    private void Start()
    {
        if(directions.Count == 0)
        {
            directions.Add(Vector3.up);
            diceValue.Add(6);
            directions.Add(Vector3.down);
            diceValue.Add(1);

            directions.Add(Vector3.forward);
            diceValue.Add(4);
            directions.Add(Vector3.back);
            diceValue.Add(3);

            directions.Add(Vector3.right);
            diceValue.Add(2);
            directions.Add(Vector3.left);
            diceValue.Add(5);
        }

        GetNumber();
    }

    private void Update()
    {
        if (hasNumber)
            return;

        if (rb.velocity.magnitude <= 0.06f)
        {
            hasNumber = true;
            GetNumber();
        }
    }

    private Vector3 GetNumber()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        Debug.Log(currentRotation);
        return currentRotation;
    }
}
