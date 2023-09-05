using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapons
{
    public GameObject meleeTriger;

    private float activateTriggerDelay = 0.2f;
    private float triggerActiveForSecs = 0.2f;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 0;
        CooldownDuration = 0.6f;// 0.552f;

        MaxAmmo = 1;
        currentAmmo = 1;
    }

    public override int EvaluateAttackCondition()
    {
        Attack();
        return 1;
    }


    protected override void Attack()
    {
        animatorHandler.ActivateAnimatorAttack();
        StartCoroutine(AttackCoroutine());

        //test
        muzzleFlash.Play();
    }

    public override int Reload()
    {
        return 1;
    }

        IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(activateTriggerDelay);
        meleeTriger.SetActive(true);
        yield return new WaitForSeconds(triggerActiveForSecs);
        meleeTriger.SetActive(false);
    }
}
