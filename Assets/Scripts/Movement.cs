using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("How long walking takes")]
    [SerializeField] private protected float desiredWalkTime = 2;
    [SerializeField] private protected GameObject tileCheckerPrefab;
    [SerializeField] private protected AudioClip walkSound;

    public bool goForward;
    public bool goBack;
    public bool goLeft;
    public bool goRight;

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private bool canMove;
    private protected PointScript pointScript;
    private protected Animator anim;
    private protected Vector3 nextPosition;
    private protected Vector3 currentPosition;
    private protected float elapsidedTime;
    private protected AudioSource audioSource;
    private protected GameManager gameManager;

    private void Awake()
    {
        pointScript = GetComponent<PointScript>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GameObject checker = Instantiate(tileCheckerPrefab);
        checker.GetComponent<FollowObject>().followGameObject = gameObject;
        CanMove = true;
        currentPosition = transform.position;
        gameManager = GameManager.Instance;
    }

    public virtual void Update() { }

    public virtual void Move() { }

    public virtual void NewPosition(Vector3 addPosition, Vector3 direction)
    {
        nextPosition = new Vector3(currentPosition.x + addPosition.x, currentPosition.y + addPosition.y, currentPosition.z + addPosition.z);
        currentPosition = transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public virtual void LerpMove()
    {
        if (!canMove /*&& !isAttacking*/)
        {
            elapsidedTime += Time.deltaTime;
            float percentage = elapsidedTime / desiredWalkTime;
            transform.position = Vector3.Lerp(currentPosition, nextPosition, percentage);
            if (percentage > 0.98f)
            {
                transform.position = nextPosition;
                currentPosition = transform.position;
            }
        }
    }

    public virtual void UseAction()
    {
        CanMove = false;
        if (walkSound)
            audioSource.PlayOneShot(walkSound);
        StartCoroutine(SetCanMoveToTrue());
    }

    public virtual IEnumerator SetCanMoveToTrue()
    {
        yield return new WaitForSeconds(desiredWalkTime + 0.5f);
        pointScript.UsePoint(1);
        elapsidedTime = 0;
        CanMove = true;
    }
}
