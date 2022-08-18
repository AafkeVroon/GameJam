using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offSet;

    public Transform target;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        target = gameManager.PlayerObject.transform;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offSet;
        Vector3 smoothPosiotion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosiotion;
    }
}
