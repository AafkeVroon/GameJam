using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackRange = 1;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private float attackTime = 1;
    [SerializeField] private int attackPointCost = 2;
    [SerializeField] private LayerMask hitLayer;

    public bool EnemyFound { get { return enemyFound; } set { enemyFound = value; } }

    private bool enemyFound;
    private PointScript pointScript;
    private PlayerMovement playerMovement;
    private Animator anim;
    private RaycastHit hit;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (InterfaceManager.Instance.isPaused || !pointScript.DiceThrower.isTurn || !EnemyFound | !playerMovement.CanMove)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            if (!pointScript.CheckEnoughPoints(attackPointCost))
                return;

            playerMovement.CanMove = false;
            if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, 10, hitLayer))
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x, hit.collider.gameObject.transform.position.y, transform.position.z));
                anim.SetTrigger("Attack");
                hit.collider.gameObject.GetComponent<Health>().ModifyHealth(-attackDamage);
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        pointScript.UsePoint(attackPointCost);
        playerMovement.CanMove = true;
    }
}
