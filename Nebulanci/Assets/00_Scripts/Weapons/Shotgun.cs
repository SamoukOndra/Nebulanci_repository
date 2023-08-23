using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Weapons
{
    

    private void Awake()
    {
        WeaponID = 3;
        CooldownDuration = 1.5f;

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
