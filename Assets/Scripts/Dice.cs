using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value;

    private Rigidbody rb;
    private bool hasNumber;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        
        return 1;
    }
}
