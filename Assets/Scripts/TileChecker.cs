using UnityEngine;

public class TileChecker : MonoBehaviour
{
    [SerializeField] private int direction;

    private Movement movement;

    private void Start()
    {
        GameObject followObject = GetComponentInParent<FollowObject>().followGameObject;
        movement = followObject.GetComponent<Movement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile") || other.gameObject.CompareTag("Gate")
             || other.gameObject.CompareTag("Checker"))
        {
            switch (direction)
            {
                case 0:
                    movement.goForward = true;
                    break;
                case 1:
                    movement.goBack = true;
                    break;
                case 2:
                    movement.goLeft = true;
                    break;
                case 3:
                    movement.goRight = true;
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case 0:
                    movement.goForward = false;
                    break;
                case 1:
                    movement.goBack = false;
                    break;
                case 2:
                    movement.goLeft = false;
                    break;
                case 3:
                    movement.goRight = false;
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
                    movement.goForward = false;
                    break;
                case 1:
                    movement.goBack = false;
                    break;
                case 2:
                    movement.goLeft = false;
                    break;
                case 3:
                    movement.goRight = false;
                    break;
            }
        }
    }
}
