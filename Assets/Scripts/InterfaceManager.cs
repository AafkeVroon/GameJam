using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;

    private bool isPaused;
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
                if (isPaused)
                    pauseMenu.SetActive(false);
                else
                    pauseMenu.SetActive(true);
            }
        }
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
