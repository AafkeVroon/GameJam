using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    [SerializeField] private int direction;

    public PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<FollowObject>().followGameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            switch (direction)
            {
                case 0:
                    playerMovement.goForward = true;
                    break;
                case 1:
                    playerMovement.goBack = true;
                    break;
                case 2:
                    playerMovement.goLeft = true;
                    break;
                case 3:
                    playerMovement.goRight = true;
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case 0:
                    playerMovement.goForward = false;
                    break;
                case 1:
                    playerMovement.goBack = false;
                    break;
                case 2:
                    playerMovement.goLeft = false;
                    break;
                case 3:
                    playerMovement.goRight = false;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            switch (direction)
            {
                case 0:
                    playerMovement.goForward = false;
                    break;
                case 1:
                    playerMovement.goBack = false;
                    break;
                case 2:
                    playerMovement.goLeft = false;
                    break;
                case 3:
                    playerMovement.goRight = false;
                    break;
            }
        }
    }
}
