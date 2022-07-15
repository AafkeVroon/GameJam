using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    [SerializeField] private int direction;

    [SerializeField] private bool up;
    [SerializeField] private bool down;
    [SerializeField] private bool left;
    [SerializeField] private bool right;
    [SerializeField] private bool back;
    [SerializeField] private bool front;

    private void OnTriggerStay(Collider other)
    {
        //switch (direction)
        //{
        //    case 0:

                
        //}
    }
}
