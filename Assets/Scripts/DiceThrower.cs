using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    [SerializeField] private Transform diceSpawnpoint;

    public bool isTurn;
    public bool canThrow;

    public int CurrentAmountOfThrows { get { return currentAmountOfThrows; } set { currentAmountOfThrows = value; } }

    private GameManager gameManager;
    private int currentAmountOfThrows;

    private void Start()
    {
        gameManager = GameManager.Instance;
        CurrentAmountOfThrows = gameManager.AmountOfDices;
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
                if (Input.GetMouseButtonDown(0) && CurrentAmountOfThrows > 0)
                {
                    gameManager.ThrowDice(diceSpawnpoint);
                    CurrentAmountOfThrows--;
                    canThrow = false;
                }
                else
                {
                    //Lose
                }
            }
            else
            {
                gameManager.ThrowDice(diceSpawnpoint);
                canThrow = false;
            }
        }
    }
}
