using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grenade : Weapons
{
    [SerializeField] GameObject grenadeToThrow;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 20;
        CooldownDuration = 0.3f;

        MaxAmmo = 5;
        currentAmmo = 1;
    }

    public override int EvaluateAttackCondition()
    {
        if (currentAmmo <= 0) return currentAmmo;

        currentAmmo--;
        return currentAmmo;
    }   //vzdy returne pokud ammo nula, to pro pripad, ze by to byla defaultni zbran. Bo pokud neni, v okamziku kdy ma ammo nula je znicena. return -1 znamena, že uz probiha reload defaultni zbrane

    protected override void Attack() { }

    public void GrenadeThrow(float forceRation)
    {
        GameObject grenade = Instantiate(grenadeToThrow, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        Throwable throwable = grenade.GetComponent<Throwable>();
        throwable.shootingPlayer = shootingPlayer;
        
        StartCoroutine(throwable.ThrowCoroutine(forceRation));

        animatorHandler.ActivateAnimatorAttack();
    }

    //IEnumerator GrenadeFlightCoroutine(float force, GameObject grenade)
    //{
    //    Rigidbody rb = grenade.GetComponent<Rigidbody>();
    //    rb.freezeRotation = true;
    //
    //    Quaternion startRotation = grenade.transform.rotation * Quaternion.Euler(Vector3.right * -maxThrowAngle);
    //    Quaternion endRotation = grenade.transform.rotation * Quaternion.Euler(Vector3.right * maxThrowAngle);
    //
    //    float timer = 0;
    //
    //    while(timer < flightCurvatureDuration)
    //    {
    //        timer += Time.deltaTime;
    //        float lerp = timer / flightCurvatureDuration;
    //        grenade.transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerp);
    //        
    //        rb.AddForce(grenade.transform.forward * force, ForceMode.Force);
    //
    //        yield return null;
    //    }
    //}

    //public override int EvaluateAttackCondition()
    //{
    //    if (currentAmmo <= 0) return currentAmmo;
    //
    //    Attack();
    //
    //    //currentAmmo--;
    //    return currentAmmo;
    //}
    //
    //
    //protected override void Attack()
    //{
    //    //base.Attack();
    //    //SpawnBullet(shootingPlayer);
    //    animatorHandler.ActivateAnimatorAttack();
    //    StartCoroutine(ThrowGrenadeCoroutine());
    //}

    public override int Reload()
    {
        if (currentAmmo < MaxAmmo)
        {
            if (currentAmmo > 0)
                currentAmmo++;

            else currentAmmo = MaxAmmo;
        }
            

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

    //IEnumerator ThrowGrenadeCoroutine() //tahle coroutine bezi donekonecna. Mozna i cd coroutine s hodnotou 1000...
    //{
    //    float throwForce = 0;
    //
    //    while (isAttacking)
    //    {
    //        if (throwForce < maxForce)
    //            throwForce += forceGrowth * Time.deltaTime;
    //        
    //        Debug.Log("throw forece: " + throwForce);
    //
    //        yield return null;
    //    }
    //
    //    GameObject thrownGrenade = Instantiate(grenadeToThrow, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    //    
    //    if(thrownGrenade.TryGetComponent(out Projectiles projectiles))
    //    {
    //        projectiles.Throw(throwForce);
    //    }
    //
    //    EventManager.InvokeOnGrenadeThrown(shootingPlayer, grenadeCooldown);
    //
    //    //nutno taky predat info o shootingPlayer do projectilu pro kills
    //}
}
