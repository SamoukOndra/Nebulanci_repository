using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;//make private

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float dmg)
    {
        currentHealth -= dmg;
        
        if (currentHealth <= 0)
            WhenZeroHealth();
    }

    protected virtual void WhenZeroHealth()
    {
        gameObject.SetActive(false);
    }
}
