using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneLoading : MonoBehaviour
{
    [SerializeField] private bool SCREAM = false;

    private float ScreamDuration;
    private string sceneName;

    public void LoadScene(string scene)
    {
        if (SCREAM)
        {
            AudioSource audio = GetComponent<AudioSource>();

            audio.Play();
            ScreamDuration = audio.clip.length;
            StartCoroutine(startGame());
            sceneName = scene;
        }
        else
            InterfaceManager.Instance.LoadLevelWithScreen(scene);
    }

    private IEnumerator startGame()
    {
        yield return new WaitForSeconds(ScreamDuration - 2f);
        InterfaceManager.Instance.LoadLevelWithScreen(sceneName);
    }
}
