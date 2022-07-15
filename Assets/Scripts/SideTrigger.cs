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

    private Dice dice;

    private void Start()
    {
        dice = GetComponent<Dice>();
    }

    private void OnTriggerStay(Collider other)
    {
        switch (direction)
        {
            case 0://Up

                break;
            case 1://Under
                break;
            case 2://Left
                break;
            case 3://Right
                break;
            case 4://Front
                break;
            case 5://Back
                break;

        }
    }
}
