using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float desiredWalkTime = 2;
    [SerializeField] private GameObject tileCheckerPrefab;
    [SerializeField] private AudioClip walkSound;

    public bool goForward;
    public bool goBack;
    public bool goLeft;
    public bool goRight;

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private PointScript pointScript;
    private bool canMove;
    private Animator anim;
    private Vector3 nextPosition;
    private Vector3 currentPosition;
    private float elapsidedTime;
    private AudioSource audioSource;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GameObject checker = Instantiate(tileCheckerPrefab);
        checker.GetComponent<FollowObject>().followGameObject = gameObject;
        canMove = true;
        currentPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!InterfaceManager.Instance.isPaused)
        {
            if (pointScript.CurrentAmountPoints > 0)
            {
                if (Input.GetKeyDown(KeyCode.W) && canMove && goForward)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;

                    nextPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 2);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    UseAction();
                }
                if (Input.GetKeyDown(KeyCode.S) && canMove && goBack)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 2);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    UseAction();
                }
                if (Input.GetKeyDown(KeyCode.D) && canMove && goRight)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x + 2, currentPosition.y, currentPosition.z);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.right);
                    UseAction();
                }
                if (Input.GetKeyDown(KeyCode.A) && canMove && goLeft)
                {
                    if (!pointScript.CheckEnoughPoints(1))
                        return;
                    nextPosition = new Vector3(currentPosition.x - 2, currentPosition.y, currentPosition.z);
                    currentPosition = transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.left);
                    UseAction();
                }
            }
        }

        if (!canMove)
        {
            elapsidedTime += Time.deltaTime;
            float percentage = elapsidedTime / desiredWalkTime;
            transform.position = Vector3.Lerp(currentPosition, nextPosition, percentage);
            if (percentage > 0.97f)
            {
                transform.position = new Vector3(nextPosition.x, nextPosition.y, nextPosition.z);
                currentPosition = transform.position;
            }
        }
    }

    private void UseAction()
    {
        canMove = false;
        anim.SetTrigger("Hop");
        audioSource.PlayOneShot(walkSound);
        StartCoroutine(SetCanMoveToTrue());
    }

    private IEnumerator SetCanMoveToTrue()
    {
        yield return new WaitForSeconds(desiredWalkTime + 0.5f);
        pointScript.UsePoint(1);
        elapsidedTime = 0;
        canMove = true;
    }
}
