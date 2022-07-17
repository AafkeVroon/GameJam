using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class SceneLoading : MonoBehaviour
{
    public bool SCREAM = false;

    AudioClip DiceOfTheMICE;

    private float ScreamDuration;

    string sceneName;

    public void LoadScene(string scene)
    {
        if (SCREAM)
        {
            AudioSource audio = GetComponent<AudioSource>();

            audio.Play();
            //audio.Play();
            //yield return new WaitForSeconds(audio.clip.length);
            //audio.clip = otherClip;
            //audio.Play();
            ScreamDuration = audio.clip.length;
            StartCoroutine(startGame());
            sceneName = scene;
        }
        else
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator startGame()
    {
        yield return new WaitForSeconds(ScreamDuration - 2f);
        SceneManager.LoadScene(sceneName);
    }
}
