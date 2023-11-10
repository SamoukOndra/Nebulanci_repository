using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [HideInInspector]
    public PlayerUIHandler playerUIHandler;

    private bool isAlive = true;

    public override bool DamageAndReturnValidKill(float dmg)
    {
        currentHealth -= dmg;

        playerUIHandler.UpdateHealth(maxHealth, currentHealth);

        if (currentHealth <= 0 && isAlive)
        {
            WhenZeroHealth();
            Debug.Log(gameObject + " killed");
            return true;
        }
        
        return false;
    }

    protected override void WhenZeroHealth()
    {
        EventManager.InvokeOnPlayerDeath(gameObject);
        isAlive = false;

        StartCoroutine(ResetHealthCoroutine());
        //smrt poresena v samostatnym death scriptu kterej se zaregistruje do eventu
    }

    public void Heal(float heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        playerUIHandler.UpdateHealth(maxHealth, currentHealth);
    }

    IEnumerator ResetHealthCoroutine()
    {
        yield return new WaitForSeconds(PlayerSpawner.respawnPlayerWaitTime);
        isAlive = true;
        currentHealth = maxHealth;
        playerUIHandler.UpdateHealth(maxHealth, currentHealth);
    }
}
