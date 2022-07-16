using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    [SerializeField] private int direction;

    private PlayerMovement playerMovement;
    private EnemyAI enemyAI;

    private void Start()
    {
        GameObject followObject = GetComponentInParent<FollowObject>().followGameObject;
        if (followObject.CompareTag("Player"))
        {
            playerMovement = followObject.GetComponent<PlayerMovement>();
        }
        else
        {
            enemyAI = followObject.GetComponent<EnemyAI>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile") || other.gameObject.CompareTag("Gate"))
        {
            switch (direction)
            {
                case 0:
                    if (playerMovement)
                        playerMovement.goForward = true;
                    else
                        enemyAI.goForward = true;
                    break;
                case 1:
                    if (playerMovement)
                        playerMovement.goBack = true;
                    else
                        enemyAI.goBack = true;
                    break;
                case 2:
                    if (playerMovement)
                        playerMovement.goLeft = true;
                    else
                        enemyAI.goLeft = true;
                    break;
                case 3:
                    if (playerMovement)
                        playerMovement.goRight = true;
                    else
                        enemyAI.goRight = true;
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case 0:
                    if (playerMovement)
                        playerMovement.goForward = false;
                    else
                        enemyAI.goForward = false;
                    break;
                case 1:
                    if (playerMovement)
                        playerMovement.goBack = false;
                    else
                        enemyAI.goBack = false;
                    break;
                case 2:
                    if (playerMovement)
                        playerMovement.goLeft = false;
                    else
                        enemyAI.goLeft = false;
                    break;
                case 3:
                    if (playerMovement)
                        playerMovement.goRight = false;
                    else
                        enemyAI.goRight = false;
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
                    if (playerMovement)
                        playerMovement.goForward = false;
                    else
                        enemyAI.goForward = false;
                    break;
                case 1:
                    if (playerMovement)
                        playerMovement.goBack = false;
                    else
                        enemyAI.goBack = false;
                    break;
                case 2:
                    if (playerMovement)
                        playerMovement.goLeft = false;
                    else
                        enemyAI.goLeft = false;
                    break;
                case 3:
                    if (playerMovement)
                        playerMovement.goRight = false;
                    else
                        enemyAI.goRight = false;
                    break;
            }
        }
    }
}
