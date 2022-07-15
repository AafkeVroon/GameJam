using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    [SerializeField] private int currentAmountPoints;
    [SerializeField] private int amountOfDices = 3;

    public int CurrentAmountPoints { get { return currentAmountPoints; } }

    public void UsePoint(int amount)
    {
        if (currentAmountPoints > 0 && amount <= currentAmountPoints)
            currentAmountPoints -= amount;
        else
            Debug.Log("No points");//Show that you have no points left
    }
}
