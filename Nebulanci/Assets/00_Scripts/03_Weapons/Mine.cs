using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Weapons
{
    [SerializeField] GameObject mineTrap;

    protected override void Awake()
    {
        animatorHandler = GetComponentInParent<AnimatorHandler>();

        WeaponID = 21;
        CooldownDuration = 0.6f;

        MaxAmmo = 5;
        currentAmmo = 2;
    }

    protected override void Attack()
    {
        animatorHandler.ActivateAnimatorAttack();
        GameObject _mineTrap = Instantiate(mineTrap, shootingPlayer.transform.position, Quaternion.identity);
        _mineTrap.GetComponentInChildren<MineTrapTrigger>().SetShootingPlayer(shootingPlayer);
    }




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
}
