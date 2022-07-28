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
    [SerializeField] private AudioClip[] attackSounds;

    public bool EnemyFound { get { return enemyFound; } set { enemyFound = value; } }

    private bool enemyFound;
    private PointScript pointScript;
    private PlayerMovement playerMovement;
    private Animator anim;
    private RaycastHit hit;
    private AudioSource audioSource;

    private void Start()
    {
        pointScript = GetComponent<PointScript>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
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

            Attack();
        }
    }

    private void Attack()
    {
        if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, 10, hitLayer))
        {
            playerMovement.CanMove = false;
            //transform.LookAt(hit.collider.gameObject.transform, Vector3.up);
            anim.SetTrigger("Attack");
            audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);
            hit.collider.gameObject.GetComponent<Health>().ModifyHealth(-attackDamage);
            StartCoroutine(AttackCooldown());
        }
        //if(Physics.SphereCast(transform.position,attackRange,transform.forward, out hit, 5, hitLayer))
        //{
        //    anim.SetTrigger("Attack");
        //}
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        pointScript.UsePoint(attackPointCost);
        playerMovement.CanMove = true;
    }
}
