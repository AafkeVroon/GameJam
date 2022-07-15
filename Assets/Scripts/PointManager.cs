using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance;

    [SerializeField] private int amountOfDices = 3;
    [SerializeField] private int maxPoints;

    public int MaxPoints { get { return maxPoints; } set { maxPoints = value; } }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void AddPoints(int amount)
    {
        MaxPoints += amount;
    }
}
