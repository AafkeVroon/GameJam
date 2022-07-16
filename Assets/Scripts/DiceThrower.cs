using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    public bool isTurn;

    private GameManager gameManager;
    private bool canThrow;

    private void Start()
    {
        gameManager = GameManager.Instance;
        canThrow = true;
    }

    private void Update()
    {
        if (!isTurn)
            return;

        if (canThrow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameManager.ThrowDice();
                canThrow = false;
            }
        }
    }
}
