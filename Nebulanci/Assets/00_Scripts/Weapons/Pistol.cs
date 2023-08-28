using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    private Animator animator;
    //private AnimationClip pistolRecoil;
    private int pistolRecoil;


    protected override void Awake()
    {
        base.Awake();
        
        animator = GetComponentInChildren<Animator>();
        pistolRecoil = Animator.StringToHash("Pistol Recoil");

        WeaponID = 1;
        CooldownDuration = 0.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;
    }


    public override int Attack()
    {
        int currentAmmo = base.Attack();
        
        if(currentAmmo >= 0)
            animator.Play(pistolRecoil);


        return currentAmmo;
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }
}
