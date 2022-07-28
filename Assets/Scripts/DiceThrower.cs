using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DiceThrower : MonoBehaviour
{
    [SerializeField] private Transform diceSpawnpoint;
    [SerializeField] private TextMeshProUGUI amountOfThrowsText;

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
        {
            isTurn = true;
            amountOfThrowsText = InterfaceManager.Instance.rollAmountText;
            amountOfThrowsText.text = CurrentAmountOfThrows.ToString();
        }
    }

    private void Update()
    {
        if (!isTurn || InterfaceManager.Instance.isPaused)
            return;

        if (canThrow)
        {
            if (gameObject.CompareTag("Player"))
            {
                if (Input.GetMouseButtonDown(0) && CurrentAmountOfThrows > 0)
                {
                    gameManager.ThrowDice(diceSpawnpoint);
                    CurrentAmountOfThrows--;
                    amountOfThrowsText.text = CurrentAmountOfThrows.ToString();
                    canThrow = false;
                }
                else if (CurrentAmountOfThrows <= 0)
                {
                    SceneManager.LoadScene("GameOver");//make asyncloadscene with loading screen
                }
            }
            else
            {
                gameManager.ThrowDice(diceSpawnpoint);
                canThrow = false;
            }
        }
    }

    public void AddRoll(int amount)
    {
        CurrentAmountOfThrows += 1;
        amountOfThrowsText.text = CurrentAmountOfThrows.ToString();
    }
}
