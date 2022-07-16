using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject followGameObject;

    private void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        while (gameObject.activeSelf)
        {
            FollowTarget();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void FollowTarget()
    {
        transform.position = followGameObject.transform.position;
    }
}
