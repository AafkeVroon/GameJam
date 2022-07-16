using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    [SerializeField] private int direction;

    private Dice dice;

    private void Start()
    {
        dice = GetComponentInParent<Dice>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (dice.hasNumber)
            return;

        if (other.gameObject.CompareTag("Tile"))
        {
            switch (direction)
            {
                case 0://Up
                    dice.value = 6;
                    break;
                case 1://Under
                    dice.value = 1;
                    break;
                case 2://Left
                    dice.value = 5;
                    break;
                case 3://Right
                    dice.value = 2;
                    break;
                case 4://Front
                    dice.value = 4;
                    break;
                case 5://Back
                    dice.value = 3;
                    break;

            }
        }
    }
}
