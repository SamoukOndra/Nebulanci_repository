using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    
    private void Awake()
    {
        WeaponID = 1;
        CooldownDuration = 0.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;

        GetAnimatorHandlerInParent();
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
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }
}
