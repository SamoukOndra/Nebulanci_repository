using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Weapons
{
    

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 3;
        CooldownDuration = 1.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;

        //GetAnimatorHandlerInParent();
    }


    protected override void Attack()
    {
        base.Attack();
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }
}
