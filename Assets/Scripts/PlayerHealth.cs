using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Start()
    {
        if (healthUI)
            healthUI.text = "Lives: " + health.ToString();
    }

    public override void ModifyHealth(int hp)
    {
        HP += hp;

        if (healthUI)
            healthUI.text = "Lives: " + health.ToString();

        if (HP <= 0)
            onHealthZero.Invoke();
    }

    /// <summary>
    /// This is called when player dies
    /// </summary>
    public void Dead()
    {
        InterfaceManager.Instance.ShowWinMenu();
        //GameManager.instance.SetGameOver();
    }
}
