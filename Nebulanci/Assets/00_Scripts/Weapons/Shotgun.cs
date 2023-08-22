using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Weapons
{
    

    private void Awake()
    {
        weaponID = 3;
        cooldownDuration = 1.5f;

        maxAmmo = 5;
        currentAmmo = maxAmmo;
    }




    public override int Attack()
    {
        if (currentAmmo <= 0) return currentAmmo;

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
