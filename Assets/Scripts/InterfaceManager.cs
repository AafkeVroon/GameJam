using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

    private int currentScene;

    [Header("Menu's")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject loadingScreen;
    [Tooltip("Menu's to deactivate")]
    [SerializeField] private GameObject[] menus;

    [Header("Fade")]
    [Tooltip("The fade panel")]
    [SerializeField] private Image fadeImage;
    [Tooltip("How long the fading takes")]
    [SerializeField] private float fadeDuration = 3;

    [Header("Other")]
    [SerializeField] private Image loadingProgressBar;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float extraWaitingTime = 3;

    private AsyncOperation nextScene;
    private GameManager gameManager;

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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    }

    #region Menu's
    /// <summary>
    /// Call this to deactivate menu's
    /// </summary>
    private void EnableDisableMenus(bool value)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(value);
        }
    }

    /// <summary>
    /// Call this to enable or disable a menu
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="value"></param>
    public void EnableDisableMenu(GameObject menu, bool value)
    {
        menu.SetActive(value);
    }

    /// <summary>
    /// Enable the win menu
    /// </summary>
    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }

    /// <summary>
    /// Start the game over coroutine
    /// </summary>
    public void ShowGameOver()
    {
        StartCoroutine(ShowMenuWithFade(1, fadeDuration, 1, 0, gameOverMenu, null));
        EnableDisableMenus(false);
    }

    /// <summary>
    /// Call this to start a fade
    /// </summary>
    /// <param name="fadeDuration">How long it takes for fading</param>
    /// <param name="alphaValue">What value the aplha needs to be</param>
    /// <param name="alphaTarget">Set alpha to 0 or 1</param>
    public void StartFade(float fadeDuration, float alphaValue, float alphaTarget, GameObject menu)
    {
        StartCoroutine(ShowMenuWithFade(0.5f, fadeDuration, alphaValue, alphaTarget, menu, null));
    }

    /// <summary>
    /// Enable or disable the pause menu
    /// </summary>
    public void ShowPauseMenu(bool isActive)
    {
        if (pauseMenu)
        {
            pauseMenu.SetActive(isActive);
        }
    }

    /// <summary>
    /// Call this to activate the game over
    /// </summary>
    /// <param name="delay">How much delay the fucntion has</param>
    /// <param name="duration">How long it takes for fading</param>
    /// <param name="alphaValue">What value the aplha needs to be</param>
    /// <param name="alphaTarget">Set alpha to 0 or 1</param>
    /// <param name="menu">What menu to enable</param>
    /// <param name="animator">The animator to play animation</param>
    /// <returns></returns>
    private IEnumerator ShowMenuWithFade(float delay, float duration, float alphaValue, float alphaTarget, GameObject menu, Animator animator, string triggerName = "Win")
    {
        yield return new WaitForSecondsRealtime(delay);
        CrossFadeAlpha(duration, alphaValue, alphaTarget);
        yield return new WaitForSecondsRealtime(duration);
        if (menu)
        {
            menu.SetActive(true);
        }
        if (animator)
        {
            animator.SetTrigger(triggerName);
        }
    }

    /// <summary>
    /// working CrossFadeAlpha for making image alpha go to 0 or 1 over time
    /// </summary>
    /// <param name="duration">How long it takes for fading</param>
    /// <param name="alphaValue">What value the aplha needs to be</param>
    /// <param name="alphaTarget">Set alpha to 0 or 1</param>
    private void CrossFadeAlpha(float duration, float alphaValue, float alphaTarget)
    {
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;
        fadeImage.CrossFadeAlpha(alphaTarget, 0, true);
        fadeImage.CrossFadeAlpha(alphaValue, duration, true);
    }

    #endregion

    public void LoadLevelWithScreen(string sceneName)
    {
        if (gameManager)
            gameManager.SetGameState(GameState.Restarting);
        loadingScreen.SetActive(true);
        nextScene = SceneManager.LoadSceneAsync(sceneName);
        nextScene.allowSceneActivation = false;
        StartCoroutine(GetSceneLoadProgressWithImage());
    }

    public void LoadLevel(string sceneName)
    {
        if (gameManager)
            gameManager.SetGameState(GameState.Restarting);
        loadingScreen.SetActive(true);
        nextScene = SceneManager.LoadSceneAsync(sceneName);
        nextScene.allowSceneActivation = false;
        StartCoroutine(GetSceneLoadProgressWithBar());
    }

    private IEnumerator GetSceneLoadProgressWithBar()
    {
        float totalProgress = 0;
        while (!nextScene.isDone)//Add extra time
        {
            totalProgress += nextScene.progress;
            loadingProgressBar.fillAmount = totalProgress;
            yield return null;
        }

        loadingScreen.SetActive(false);
        if (gameManager)
            gameManager.UnPause();
    }

    private IEnumerator GetSceneLoadProgressWithImage()
    {
        if (gameManager)
            gameManager.SetGameState(GameState.Restarting);
        float sceneProgress = 0;
        float maxProgress = extraWaitingTime;
        while (sceneProgress < maxProgress)
        {
            maxProgress += nextScene.progress;
            sceneProgress += Time.deltaTime;
            if (sceneProgress > extraWaitingTime)
                nextScene.allowSceneActivation = true;
            loadingProgressBar.transform.Rotate(0, 0, 1 * rotationSpeed);
            yield return null;
        }

        loadingScreen.SetActive(false);
        if (gameManager)
            gameManager.UnPause();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
