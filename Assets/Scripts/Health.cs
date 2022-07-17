using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour
{
    [Tooltip("How much health you have")]
    [SerializeField] private protected int health;
    [Tooltip("The health ui")]
    [SerializeField] private protected TextMeshProUGUI healthUI;
    [SerializeField] private protected UnityEvent onHealthZero;

    [SerializeField] private GameObject Damage;
    [SerializeField]private float ShowHitAnim = 1f;

   [SerializeField] private bool isShowing;

    public int HP { get { return health; } set { health = value; } }

    public virtual void Start()
    {
        if (healthUI)
            healthUI.text = health.ToString();
    }

    public virtual void ModifyHealth(int hp)
    {
        HP += hp;

        if (healthUI)
            healthUI.text = health.ToString();

        if (HP <= 0)
        {
            onHealthZero.Invoke();
            GameManager.Instance.RemoveEnemy(gameObject);
            GameManager.Instance.PlayerObject.GetComponent<DiceThrower>().CurrentAmountOfThrows++;
            Destroy(gameObject);
        }
    }
}
