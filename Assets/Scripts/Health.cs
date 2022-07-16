using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Health : MonoBehaviour
{
    [Tooltip("How much health you have")]
    [SerializeField] private protected int health;
    [Tooltip("The health ui")]
    [SerializeField] private protected TextMeshProUGUI healthUI;
    [SerializeField] private protected UnityEvent onHealthZero;

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
            Destroy(gameObject);
        }
    }
}
