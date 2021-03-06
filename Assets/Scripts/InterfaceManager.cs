using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;

    public TextMeshProUGUI rollAmountText;
    public TextMeshProUGUI pointAmountText1;
    public TextMeshProUGUI pointAmountText2;
    public TextMeshProUGUI healthText;
    public GameObject spritePlayer;
    public GameObject spriteSlime;
    public bool isPaused;

    private int currentScene;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("CurrentScene", currentScene);
    }

    private void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused == true)
                {
                    pauseMenu.SetActive(false);
                    isPaused = false;
                }
                else if (isPaused == false)
                {
                    pauseMenu.SetActive(true);
                    isPaused = true;
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void LoadNewScene(string newLevel)
    {
        SceneManager.LoadScene(newLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }
}
