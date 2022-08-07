using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Game,
    Menu,
    Paused,
    Restarting
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("The current state of the game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private int amountOfDices = 3;
    [SerializeField] private float throwForce = 5;
    [SerializeField] private int maxPoints;
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private List<GameObject> characterPrefabs;
    [SerializeField] private List<GameObject> characterSpawnpoints;

    public int MaxPoints { get { return maxPoints; } set { maxPoints = value; } }
    public int AmountOfDices { get { return amountOfDices; } }
    public GameObject PlayerObject { get { return characters[0]; } }

    private InterfaceManager interfaceManager;
    private int amountOfCharacters;
    private int currentTurn;
    private List<GameObject> characters = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        interfaceManager = InterfaceManager.Instance;
        amountOfCharacters = characterPrefabs.Count;
        SpawnCharacters();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState != GameState.Restarting && gameState != GameState.Menu)
        {
            if (gameState == GameState.Paused)
            {
                UnPause();
            }
            else if (gameState == GameState.Game)
            {
                Pause();
            }
        }
    }

    public void UnPause()
    {
        interfaceManager.ShowPauseMenu(false);
        SetGameState(GameState.Game);
        //Time.timeScale = 1;
    }

    public void Pause()
    {
        interfaceManager.ShowPauseMenu(true);
        SetGameState(GameState.Paused);
        //Time.timeScale = 0;
    }

    public void AddPoints(int amount)
    {
        MaxPoints = amount;
        characters[currentTurn].GetComponent<PointScript>().AddPoints(amount);
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
            InterfaceManager.Instance.spritePlayer.SetActive(false);
            InterfaceManager.Instance.spriteSlime.SetActive(true);
            characters[currentTurn].GetComponent<DiceThrower>().isTurn = true;
            characters[currentTurn].GetComponent<DiceThrower>().canThrow = true;
        }
        else
        {
            currentTurn = 0;
            InterfaceManager.Instance.spritePlayer.SetActive(true);
            InterfaceManager.Instance.spriteSlime.SetActive(false);
            characters[currentTurn].GetComponent<DiceThrower>().isTurn = true;
            characters[currentTurn].GetComponent<DiceThrower>().canThrow = true;
        }
    }

    public void RemoveEnemy(GameObject self)
    {
        characters.Remove(self);
        if (characters.Count <= 1)
            InterfaceManager.Instance.ShowWinMenu();
    }

    public void EndTurn()
    {
        characters[currentTurn].GetComponent<PointScript>().CurrentAmountPoints = 0;
        NextTurn();
    }

    /// <summary>
    /// Get the current game state
    /// </summary>
    /// <returns></returns>
    public GameState GetGameState()
    {
        return gameState;
    }

    /// <summary>
    /// Set the game state with enum
    /// </summary>
    /// <param name="state">What state it needs to be</param>
    public void SetGameState(GameState state)
    {
        gameState = state;
    }
}
