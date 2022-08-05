using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private Attack attack;

    private void Start()
    {
        attack = GetComponentInParent<Attack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attack && !attack.EnemyFound && attack.gameObject.tag != other.gameObject.tag && other.gameObject.CompareTag("Enemy") | other.gameObject.CompareTag("Player"))
        {
            attack.EnemyFound = true;
            attack.Enemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") | other.gameObject.CompareTag("Player"))
        {
            attack.EnemyFound = false;
            attack.Enemy = null;
        }
    }
}
