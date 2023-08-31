using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPoint;

    public AnimatorOverrideController animatorOverrideController;
    public AnimatorHandler animatorHandler;
    private ParticleSystem muzzleFlash;

    public int WeaponID { get; protected set; }
    public float CooldownDuration { get; protected set; }

    public int MaxAmmo { get; protected set; }
    public int currentAmmo;

    protected virtual void Awake()
    {
        animatorHandler = GetComponentInParent<AnimatorHandler>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    protected void GetAnimatorHandlerInParent()
    {
        animatorHandler = GetComponentInParent<AnimatorHandler>();
    }

    public int EvaluateAttackCondition() 
    {
        if (currentAmmo <= 0) return currentAmmo;

        Attack();

        currentAmmo--;
        return currentAmmo;
    }   //vzdy returne pokud ammo nula, to pro pripad, ze by to byla defaultni zbran. Bo pokud neni, v okamziku kdy ma ammo nula je znicena. return -1 znamena, že uz probiha reload defaultni zbrane

    protected virtual void Attack()
    {
        Debug.Log("Shot fired");

        muzzleFlash.Play();

        animatorHandler.ActivateAnimatorAttack();
    }
    public abstract int Reload();// v pripade pusek doplni maximum, u granatu +1 do maxima 5?
    // returnou current ammo, pro UI, destroyWeapon atd

    //pokud pridam nejakou macetu ci jinou melee weapon, Attack vzdycky returne +1. Tak bude moct bejt defaultni zbran palna
    protected void SpawnBullet()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = projectileSpawnPoint.transform.position;
            bullet.transform.rotation = projectileSpawnPoint.transform.rotation;
            bullet.SetActive(true);
        }
    }
}
