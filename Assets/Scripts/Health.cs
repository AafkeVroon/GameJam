using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private AudioClip hurtSound;
    [Tooltip("How much health you have")]
    [SerializeField] private protected int health;
    [Tooltip("The health ui")]
    [SerializeField] private protected TextMeshProUGUI healthUI;
    [SerializeField] private protected UnityEvent onHealthZero;

    [SerializeField] private GameObject Damage;
    [SerializeField] private float ShowHitAnim = 1f;

    [SerializeField] private bool isShowing;

    public int HP { get { return health; } set { health = value; } }

    private AudioSource audioSource;

    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (healthUI)
            healthUI.text = health.ToString();
    }

    public virtual void ModifyHealth(int hp)
    {
        HP += hp;
        if (hurtSound)
            audioSource.PlayOneShot(hurtSound);
        if (healthUI)
            healthUI.text = health.ToString();

        if (HP <= 0)
        {
            onHealthZero.Invoke();
            GameManager.Instance.RemoveEnemy(gameObject);
            GameManager.Instance.PlayerObject.GetComponent<DiceThrower>().AddRoll(1);
            Destroy(gameObject);
        }
    }
}
