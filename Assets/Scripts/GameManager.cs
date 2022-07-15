using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int amountOfDices = 3;
    [SerializeField] private int maxPoints;
    [SerializeField] private GameObject dicePrefab;

    public int MaxPoints { get { return maxPoints; } set { maxPoints = value; } }

    private Camera cam;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        cam = Camera.main;
    }

    public void AddPoints(int amount)
    {
        MaxPoints += amount;
    }

    private void ThrowDice()
    {
        
    }
}
