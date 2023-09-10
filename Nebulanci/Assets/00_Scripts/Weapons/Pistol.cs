using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    //private Animator animator;
    ////private AnimationClip pistolRecoil;
    //private int pistolRecoil;


    protected override void Awake()
    {
        base.Awake();

        WeaponID = 1;
        CooldownDuration = 0.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;
    }


    protected override void Attack()
    {
        base.Attack();
        SpawnBullet(shootingPlayer);
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }
}
