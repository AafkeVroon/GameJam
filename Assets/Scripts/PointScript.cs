using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointScript : MonoBehaviour
{
    [SerializeField] private int currentAmountPoints;
    [SerializeField] private TextMeshProUGUI pointsText;

    public int CurrentAmountPoints { get { return currentAmountPoints; } set { currentAmountPoints = value; } }
    public DiceThrower DiceThrower { get { return diceThrower; } }

    private GameManager gameManager;
    private DiceThrower diceThrower;

    private void Start()
    {
        gameManager = GameManager.Instance;
        diceThrower = GetComponent<DiceThrower>();
    }

    public void UsePoint(int amount)
    {
        if (CurrentAmountPoints > 0 && amount <= CurrentAmountPoints)
        {
            CurrentAmountPoints -= amount;
            if (pointsText)
                pointsText.text = CurrentAmountPoints.ToString();

            if(CurrentAmountPoints <= 0)
            {
                gameManager.NextTurn();
                diceThrower.isTurn = false;
                Debug.Log("No points");//Show that you have no points lefts
            }
        }
    }

    public void GetPoints()
    {
        CurrentAmountPoints = gameManager.MaxPoints;
    }
}
