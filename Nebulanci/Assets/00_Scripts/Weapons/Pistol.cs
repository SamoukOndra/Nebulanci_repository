using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    
    protected override void Awake()
    {
        base.Awake();

        WeaponID = 1;
        CooldownDuration = 0.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;
    }


    public override int Attack()
    {
        int currentAmmo = base.Attack();


        return currentAmmo;
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }
}
