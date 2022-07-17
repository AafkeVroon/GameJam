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
    public GameObject PlayerObject { get { return characters[0]; } }

    private int amountOfCharacters;
    private int currentTurn;
    private List<GameObject> characters = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        amountOfCharacters = characterPrefabs.Count;
        SpawnCharacters();
    }

    public void AddPoints(int amount)
    {
        MaxPoints = amount;
        characters[currentTurn].GetComponent<PointScript>().CurrentAmountPoints += MaxPoints;
        Debug.Log(MaxPoints);
    }

    public void ThrowDice(Transform diceSpawnpoint)
    {
        for (int i = 0; i < amountOfDices; i++)
        {
            GameObject dice = Instantiate(dicePrefab, new Vector3(diceSpawnpoint.position.x, diceSpawnpoint.position.y, diceSpawnpoint.position.z * Vector3.forward.z), diceSpawnpoint.rotation);
            dice.transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
            dice.GetComponent<Rigidbody>().AddForce(diceSpawnpoint.forward * throwForce, ForceMode.Impulse);
            dice.GetComponent<Rigidbody>().AddTorque(diceSpawnpoint.up * throwForce * 1.5f * throwForce * 1.5f);
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
