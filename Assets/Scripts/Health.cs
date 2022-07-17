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

    private void Awake()
    {
        Damage = GameObject.Find("damage");
    }

    public virtual void Start()
    {
        Damage.SetActive(false);

        if (healthUI)
            healthUI.text = health.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ModifyHealth(-1);
    }

    public virtual void ModifyHealth(int hp)
    {
    StartCoroutine(ShowDamage());

        HP += hp;

        if (healthUI)
            healthUI.text = health.ToString();

        if (HP <= 0)
        {
            onHealthZero.Invoke();
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator ShowDamage()
    {
        print("Show Damage");
        Damage.SetActive(true);
        yield return new WaitForSeconds(ShowHitAnim);
        Damage.SetActive(false);
    }
}
