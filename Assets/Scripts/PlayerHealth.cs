using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        if (HP <= 0)
            onHealthZero.Invoke();
    }

    /// <summary>
    /// This is called when player dies
    /// </summary>
    public void Dead()
    {
        SceneManager.LoadScene("GameOver");
    }
}
