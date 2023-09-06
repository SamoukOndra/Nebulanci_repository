using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private bool isAlive = true;

    public override bool DamageAndReturnValidKill(float dmg)
    {
        currentHealth -= dmg;

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

    IEnumerator ResetHealthCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        isAlive = true;
        currentHealth = maxHealth;
    }
}
