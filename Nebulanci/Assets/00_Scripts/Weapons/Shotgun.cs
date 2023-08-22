using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Weapons
{
    

    private void Awake()
    {
        weaponID = 3;
        cooldown = 1.5f;
    }




    public override void Attack()
    {
        animatorHandler.FireShotgun();
    }

    public override void Reload()
    {
        
    }
}
