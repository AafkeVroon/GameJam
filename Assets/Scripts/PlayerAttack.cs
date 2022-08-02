using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    public override void Update()
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

    //private void Attack()
    //{
    //    if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, 10, hitLayer))
    //    {
    //        playerMovement.CanMove = false;
    //        //transform.LookAt(hit.collider.gameObject.transform, Vector3.up);
    //        anim.SetTrigger("Attack");
    //        audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);
    //        hit.collider.gameObject.GetComponent<Health>().ModifyHealth(-attackDamage);
    //        StartCoroutine(AttackCooldown());
    //    }
    //    //if(Physics.SphereCast(transform.position,attackRange,transform.forward, out hit, 5, hitLayer))
    //    //{
    //    //    anim.SetTrigger("Attack");
    //    //}
    //}

    //public override IEnumerator AttackCooldown()
    //{
    //    yield return new WaitForSeconds(attackTime);
    //    pointScript.UsePoint(attackPointCost);
    //    playerMovement.CanMove = true;
    //}
}
