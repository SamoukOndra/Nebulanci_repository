using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    
    private void Awake()
    {
        weaponID = 1;
        cooldown = 0.5f;
    }




    public override void Attack()
    {
        animatorHandler.FirePistol();
    }

    public override void Reload()
    {

    }
}
