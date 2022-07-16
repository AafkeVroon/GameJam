using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int amountOfDices = 3;
    [SerializeField] private float throwForce = 5;
    [SerializeField] private int maxPoints;
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private List<GameObject> characterPrefabs;

    public int MaxPoints { get { return maxPoints; } set { maxPoints = value; } }

    private Camera cam;
    private int amountOfCharacters;
    private List<GameObject> players;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        cam = Camera.main;
    }

    private void Start()
    {
        amountOfCharacters = characterPrefabs.Count;
    }

    public void AddPoints(int amount)
    {
        MaxPoints += amount;
    }

    public void ThrowDice()
    {
        for (int i = 0; i < amountOfDices; i++)
        {
            GameObject dice = Instantiate(dicePrefab, cam.transform.position, cam.transform.rotation);
            dice.GetComponent<Dice>().AddForce(cam.transform.forward, throwForce);
        }
    }

    private void AddPlayers()
    {
        for (int i = 0; i < amountOfCharacters; i++)
        {

        }
    }
}
