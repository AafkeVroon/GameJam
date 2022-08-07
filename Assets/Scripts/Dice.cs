using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private AudioClip[] bounchSounds;

    public int value;
    public bool hasNumber;

    private Rigidbody rb;
    private GameManager gameManager;
    private bool check;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;
        StartCoroutine(StartCheck());
        StartCoroutine(HideDice());
    }

    private void Update()
    {
        if (gameManager.GetGameState() != GameState.Game)
            return;

        if (!check)
            return;

        if (rb.velocity.magnitude <= 0.01f)
        {
            if (hasNumber)
                return;

            hasNumber = true;
            gameManager.AddPoints(value);
        }
    }

    public void AddForce(Vector3 force, float forcePower)
    {
        rb.AddForce(force * forcePower);
    }

    private IEnumerator StartCheck()
    {
        yield return new WaitForSeconds(0.5f);
        check = true;
    }

    private IEnumerator HideDice()
    {
        yield return new WaitForSeconds(6);
        
        gameObject.SetActive(false);
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(bounchSounds[Random.Range(0, bounchSounds.Length)]);
    }
}
