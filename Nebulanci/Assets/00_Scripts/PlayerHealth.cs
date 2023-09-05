using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void WhenZeroHealth()
    {
        EventManager.InvokeOnPlayerDeath(gameObject);

        currentHealth = maxHealth;
        //gameObject.SetActive(false);
        //smrt poresena v samostatnym death scriptu kterej se zaregistruje do eventu
    }
}
