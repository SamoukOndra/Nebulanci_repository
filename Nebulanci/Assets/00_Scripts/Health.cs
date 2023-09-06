using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;//make protected?

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual bool DamageAndReturnValidKill(float dmg)
    {
        currentHealth -= dmg;
        
        if (currentHealth <= 0)
            WhenZeroHealth();

        return false;
    }

    protected virtual void WhenZeroHealth()
    {
        gameObject.SetActive(false);
    }
}
