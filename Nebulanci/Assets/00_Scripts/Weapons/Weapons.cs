using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapons : MonoBehaviour
{
    public AnimatorHandler animatorHandler;

    public int WeaponID { get; protected set; }
    public float CooldownDuration { get; protected set; }

    public int MaxAmmo { get; protected set; }
    public int currentAmmo;


    protected void GetAnimatorHandlerInParent()
    {
        animatorHandler = GetComponentInParent<AnimatorHandler>();
    }

    public abstract int Attack();//vzdy returne pokud ammo nula, to pro pripad, ze by to byla defaultni zbran. Bo pokud neni, v okamziku kdy ma ammo nula je znicena. return -1 znamena, že uz probiha reload defaultni zbrane
    public abstract int Reload();// v pripade pusek doplni maximum, u granatu +1 do maxima 5?
    // returnou current ammo, pro UI, destroyWeapon atd

    //pokud pridam nejakou macetu ci jinou melee weapon, Attack vzdycky returne +1. Tak bude moct bejt defaultni zbran palna

}
