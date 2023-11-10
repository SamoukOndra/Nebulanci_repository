using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropaneTankHealth : Health
{
    PropaneTankCM collMat;
    

    [SerializeField] float dmg = 150;
    [SerializeField] float explosionForce = 5000;
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
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        GameObject explosionGO = ExplosionPool.explosionPoolSingleton.GetPooledExplosion(gameObject, dmg, explosionForce);
        if (explosionGO != null)
        {
            explosionGO.transform.position = gameObject.transform.position;
            explosionGO.SetActive(true);
        }
    }
}
