using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    public bool isTurn;
    public bool canThrow;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        canThrow = true;
        if (gameObject.CompareTag("Player"))
            isTurn = true;
    }

    private void Update()
    {
        if (!isTurn)
            return;

        if (canThrow)
        {
            if (gameObject.CompareTag("Player"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gameManager.ThrowDice();
                    canThrow = false;
                }
            }
            else
            {
                gameManager.ThrowDice();
                canThrow = false;
            }
        }
    }
}
