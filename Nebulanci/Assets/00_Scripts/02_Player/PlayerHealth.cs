using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [HideInInspector]
    public PlayerUIHandler playerUIHandler;

    [SerializeField] float maxArmor = 100;
    [SerializeField] float currentArmor = 0;

    private bool isAlive = true;
    private bool hasArmor;

    public override bool DamageAndReturnValidKill(float dmg)
    {
        if (hasArmor)
            dmg = ReducedByArmor(dmg);

        currentHealth -= dmg;

        playerUIHandler.UpdateHealth(maxHealth, currentHealth);

        if (currentHealth <= 0 && isAlive)
        {
            WhenZeroHealth();
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

    private float ReducedByArmor(float dmg)
    {
        float reducedDmg = 0;

        currentArmor -= dmg;

        if(currentArmor <= 0)
        {
            reducedDmg = -currentArmor;
            currentArmor = 0;
            hasArmor = false;
        }

        playerUIHandler.UpdateArmor(currentArmor / maxArmor);

        return reducedDmg;
    }

    public void ArmorBuff()
    {
        hasArmor = true;
        currentArmor = maxArmor;
        playerUIHandler.UpdateArmor(1);
    }

    IEnumerator ResetHealthCoroutine()
    {
        yield return new WaitForSeconds(PlayerSpawner.respawnPlayerWaitTime);
        isAlive = true;
        currentHealth = maxHealth;
        playerUIHandler.UpdateHealth(maxHealth, currentHealth);
    }
}
