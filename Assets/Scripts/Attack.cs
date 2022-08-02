using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private protected int attackRange = 1;
    [SerializeField] private protected int attackDamage = 2;

    [SerializeField] private protected float attackTime = 1;
    [SerializeField] private protected int attackPointCost = 2;
    [SerializeField] private protected LayerMask hitLayer;
    [SerializeField] private protected AudioClip[] attackSounds;

    public bool EnemyFound { get { return enemyFound; } set { enemyFound = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    private bool enemyFound;
    private bool isAttacking;
    private protected PointScript pointScript;
    private protected Movement movement;
    private protected Animator anim;
    private protected RaycastHit hit;
    private protected AudioSource audioSource;

    public virtual void Start()
    {
        pointScript = GetComponent<PointScript>();
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if (InterfaceManager.Instance.isPaused || !pointScript.DiceThrower.isTurn || !EnemyFound | !movement.CanMove)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            if (!pointScript.CheckEnoughPoints(attackPointCost))
                return;

            AttackTarget();
        }
    }

    public virtual void AttackTarget()
    {
        if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, 10, hitLayer))
        {
            IsAttacking = true;
            movement.CanMove = false;
            //transform.LookAt(hit.collider.gameObject.transform, Vector3.up);
            anim.SetTrigger("Attack");
            audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);
            hit.collider.gameObject.GetComponent<Health>().ModifyHealth(-attackDamage);
            StartCoroutine(AttackCooldown());
        }
    }

    public virtual IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        pointScript.UsePoint(attackPointCost);
        movement.CanMove = true;
        IsAttacking = false;
    }
}
