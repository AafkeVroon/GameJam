using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    [SerializeField] private int currentAmountPoints;

    public int CurrentAmountPoints { get { return currentAmountPoints; } set { currentAmountPoints = value; } }

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void UsePoint(int amount)
    {
        if (CurrentAmountPoints > 0 && amount <= CurrentAmountPoints)
            CurrentAmountPoints -= amount;
        else
            Debug.Log("No points");//Show that you have no points left
    }

    public void GetPoints()
    {
        CurrentAmountPoints = gameManager.MaxPoints;
    }
}
