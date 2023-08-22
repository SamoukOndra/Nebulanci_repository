using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    
    private void Awake()
    {
        weaponID = 1;
        cooldownDuration = 0.5f;

        maxAmmo = 5;
        currentAmmo = maxAmmo;
    }




    public override int Attack()
    {
        if (currentAmmo <= 0) return currentAmmo;

        Debug.Log("Shot fired");

        animatorHandler.SetAnimatorAttack(true);
        currentAmmo--;
        return currentAmmo;
    }

    public override int Reload()
    {
        currentAmmo = maxAmmo;
        return currentAmmo;
    }
}
