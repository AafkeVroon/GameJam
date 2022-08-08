using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public override void Start()
    {
        healthUI = InterfaceManager.Instance.healthText;

        if (healthUI)
            healthUI.text = health.ToString();
    }

    public override void ModifyHealth(int hp)
    {
        HP += hp;

        if (healthUI)
            healthUI.text = health.ToString();
        if (hurtSound)
            audioSource.PlayOneShot(hurtSound);
        if (hurtEffect)
            Instantiate(hurtEffect, transform.position, transform.rotation);
        if (HP <= 0)
            onHealthZero.Invoke();
    }

    /// <summary>
    /// This is called when player dies
    /// </summary>
    public void Dead()
    {
        InterfaceManager.Instance.LoadLevelWithScreen("GameOver");
    }
}
