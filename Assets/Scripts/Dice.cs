using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value;
    public bool hasNumber;

    private Rigidbody rb;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (rb.velocity.magnitude <= 0.01f)
        {
            if (hasNumber)
                return;

            hasNumber = true;
            gameManager.AddPoints(value);
        }
        else
            hasNumber = false;
    }

    public void AddForce(Vector3 force, float forcePower)
    {
        rb.AddForce(force * forcePower);
    }
}
