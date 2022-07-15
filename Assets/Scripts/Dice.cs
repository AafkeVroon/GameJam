using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value;
    public bool hasNumber;

    private Rigidbody rb;
    private GameManager pointManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pointManager = GameManager.Instance;
    }

    private void Update()
    {
        if (hasNumber)
            return;

        if (rb.velocity.magnitude <= 0.01f)
        {
            hasNumber = true;
            pointManager.AddPoints(value);
        }
    }
}
