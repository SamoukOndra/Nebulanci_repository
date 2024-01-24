using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealth : Health
{
    private bool isAlive = true;

    private void OnEnable()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }

    private void OnDisable()
    {
        isAlive = false;
    }

    public override bool DamageAndReturnValidKill(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0 && isAlive)
        {
            WhenZeroHealth();
            return true;
        }

        return false;
    }

    protected override void WhenZeroHealth()
    {
        GameObject npcDeath = NpcPool.singl.GetPooledNpcDeath();
        if(npcDeath != null)
        {
            npcDeath.transform.position = gameObject.transform.position;
            npcDeath.SetActive(true);
        }

        isAlive = false;
        gameObject.SetActive(false);
    }
}
