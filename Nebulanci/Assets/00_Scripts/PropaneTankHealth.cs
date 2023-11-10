using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropaneTankHealth : Health
{
    PropaneTankCM collMat;

    [SerializeField] float healthLostPerSec = 10;

    bool f_isDemaged;

    private void Awake()
    {
        collMat = GetComponent<PropaneTankCM>();
        
    }

    private void FixedUpdate()
    {
        if (f_isDemaged)
        {
            currentHealth -= healthLostPerSec * Time.deltaTime;
            collMat.Push();

            if (currentHealth <= 0)
                WhenZeroHealth();
        }
    }

    public override bool DamageAndReturnValidKill(float dmg)
    {
        currentHealth -= dmg;

        f_isDemaged = true;

        if (currentHealth <= 0)
            WhenZeroHealth();

        return false;
    }

    protected override void WhenZeroHealth()
    {
        collMat.Explode();
        Destroy(gameObject);
    }
}
