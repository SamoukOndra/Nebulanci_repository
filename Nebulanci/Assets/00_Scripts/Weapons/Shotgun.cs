using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Weapons
{

    [SerializeField] int bulletAmount = 5;
    [SerializeField] float bulletSpread = 15f;

    float halfSpread;
    float spreadOffset;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 3;
        CooldownDuration = 1.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;

        halfSpread = bulletSpread * 0.5f;
        spreadOffset = bulletSpread / (bulletAmount - 1);
    }


    protected override void Attack()
    {
        base.Attack();
        ShotgunAttack();
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }

    private void ShotgunAttack()
    {
        float yOffset = -halfSpread;

        for (int i = 0; i < bulletAmount; i++)
        {
            Vector3 offset = Vector3.up * yOffset;

            SpawnBullet(shootingPlayer, offset);
            
            yOffset += spreadOffset;
        }
    }
}
