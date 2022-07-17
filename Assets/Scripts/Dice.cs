using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value;
    public bool hasNumber;

    private Rigidbody rb;
    private GameManager gameManager;
    private bool check;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        Destroy(gameObject, 6);
        StartCoroutine(StartCheck());
    }

    private void Update()
    {
        if (!check)
            return;

        if (rb.velocity.magnitude <= 0.01f)
        {
            if (hasNumber)
                return;

            hasNumber = true;
            gameManager.AddPoints(value);
            Debug.Log("JUHDGIHVDWGIDWGUIDWGVHDW");
        }
        //else
        //    hasNumber = false;
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
}
