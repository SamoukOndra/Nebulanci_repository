using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grenade : Weapons
{
    [SerializeField] GameObject grenadeToThrow;

    //[HideInInspector]
    //public Transform throwFromHere;

    private bool attackButtonPressed = true;
    //private float throwForce = 0;
    [SerializeField] float forceGrowth = 5;
    [SerializeField] float maxForce = 10;
    private float grenadeCooldown;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 20;
        CooldownDuration = 1000f;
        grenadeCooldown = 0.3f;

        MaxAmmo = 5;
        currentAmmo = 1;
    }

    public override int EvaluateAttackCondition()
    {
        if (currentAmmo <= 0) return currentAmmo;

        Attack();

        //currentAmmo--;
        return currentAmmo;
    }


    protected override void Attack()
    {
        //base.Attack();
        //SpawnBullet(shootingPlayer);
        animatorHandler.ActivateAnimatorAttack();
        StartCoroutine(ThrowGrenadeCoroutine());
    }

    public override int Reload()
    {
        if (currentAmmo < MaxAmmo)
            currentAmmo++;

        return currentAmmo;
    }

    public void SetProjectileSpawnPoint(Transform transform)
    {
        projectileSpawnPoint = transform;
    }

    //public void OnFire(InputValue value)
    //{
    //    attackButtonPressed = value.isPressed;
    //    //samotnej attack v Update(), z activeWeapon
    //    Debug.Log("isPressed " + value.isPressed);
    //}

    IEnumerator ThrowGrenadeCoroutine() //tahle coroutine bezi donekonecna. Mozna i cd coroutine s hodnotou 1000...
    {
        float throwForce = 0;

        while (isAttacking)
        {
            if (throwForce < maxForce)
                throwForce += forceGrowth * Time.deltaTime;
            
            Debug.Log("throw forece: " + throwForce);

            yield return null;
        }

        GameObject thrownGrenade = Instantiate(grenadeToThrow, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        
        if(thrownGrenade.TryGetComponent(out Projectiles projectiles))
        {
            projectiles.Throw(throwForce);
        }

        EventManager.InvokeOnGrenadeThrown(shootingPlayer, grenadeCooldown);

        //nutno taky predat info o shootingPlayer do projectilu pro kills
    }
}
