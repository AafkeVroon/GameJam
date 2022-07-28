using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointScript : MonoBehaviour
{
    [SerializeField] private int currentAmountPoints;
    [SerializeField] private TextMeshProUGUI pointsText1;
    [SerializeField] private TextMeshProUGUI pointsText2;

    public int CurrentAmountPoints { get { return currentAmountPoints; } set { currentAmountPoints = value; } }
    public DiceThrower DiceThrower { get { return diceThrower; } }

    private GameManager gameManager;
    private DiceThrower diceThrower;

    private void Start()
    {
        gameManager = GameManager.Instance;
        diceThrower = GetComponent<DiceThrower>();
        if (gameObject.CompareTag("Player"))
        {
            pointsText1 = InterfaceManager.Instance.pointAmountText1;
            pointsText2 = InterfaceManager.Instance.pointAmountText2;
            pointsText1.text = currentAmountPoints.ToString();
            pointsText2.text = currentAmountPoints.ToString();
        }
    }

    public void UsePoint(int amount)
    {
        if (CurrentAmountPoints > 0 && amount <= CurrentAmountPoints)
        {
            CurrentAmountPoints -= amount;

            if (CurrentAmountPoints <= 0)
            {
                CurrentAmountPoints = 0;
                diceThrower.isTurn = false;
                gameManager.NextTurn();
                Debug.Log("No points");
            }

            if (pointsText1)
            {
                pointsText1.text = currentAmountPoints.ToString();
                pointsText2.text = CurrentAmountPoints.ToString();
            }
        }
    }

    public void AddPoints(int amount)
    {
        CurrentAmountPoints += amount;
        if (pointsText1)
        {
            pointsText1.text = currentAmountPoints.ToString();
            pointsText2.text = CurrentAmountPoints.ToString();
        }
    }

    public bool CheckEnoughPoints(int amount)
    {
        if (CurrentAmountPoints - amount < 0)
            return false;

        return true;
    }

    public void GetPoints()
    {
        CurrentAmountPoints = gameManager.MaxPoints;
        if (pointsText1)
        {
            pointsText1.text = CurrentAmountPoints.ToString("00");
            pointsText2.text = CurrentAmountPoints.ToString("00");
        }
    }
}
