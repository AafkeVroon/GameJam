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
    [SerializeField] private List<GameObject> characterSpawnpoints;

    public int MaxPoints { get { return maxPoints; } set { maxPoints = value; } }

    private Camera cam;
    private int amountOfCharacters;
    private int currentTurn;
    private List<GameObject> characters = new List<GameObject>();

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
        SpawnCharacters();
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

    private void SpawnCharacters()
    {
        for (int i = 0; i < amountOfCharacters; i++)
        {
            GameObject character = Instantiate(characterPrefabs[i], characterSpawnpoints[i].transform.position, characterSpawnpoints[i].transform.rotation);
            characters.Add(character);
        }
    }

    public void NextTurn()
    {
        if (currentTurn < characters.Count - 1)
        {
            currentTurn++;
            characters[currentTurn].GetComponent<DiceThrower>().isTurn = true;
            characters[currentTurn].GetComponent<DiceThrower>().canThrow = true;
        }
        else
        {
            currentTurn = 0;
        }
    }
}
