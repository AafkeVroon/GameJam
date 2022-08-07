using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private protected float attackRange = 1;
    [SerializeField] private protected int attackDamage = 2;
    [SerializeField] private protected float attackTime = 1;
    [SerializeField] private protected int attackPointCost = 2;
    [SerializeField] private protected LayerMask hitLayer;
    [SerializeField] private protected AudioClip[] attackSounds;

    public bool EnemyFound { get { return enemyFound; } set { enemyFound = value; } }
    public GameObject Enemy { get { return enemy; } set { enemy = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    private GameObject enemy;
    private bool enemyFound;
    private bool isAttacking;
    private protected PointScript pointScript;
    private protected Movement movement;
    private protected Animator anim;
    private protected RaycastHit hit;
    private protected AudioSource audioSource;
    private protected GameManager gameManager;

    public virtual void Start()
    {
        pointScript = GetComponent<PointScript>();
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }

    public virtual void Update()
    {
        if (gameManager.GetGameState() != GameState.Game || !pointScript.DiceThrower.isTurn || !EnemyFound | !movement.CanMove)
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, hitLayer))
        {
            IsAttacking = true;
            movement.CanMove = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
