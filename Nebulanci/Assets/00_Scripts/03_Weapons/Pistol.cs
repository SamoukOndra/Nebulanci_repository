using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 1;
        CooldownDuration = 0.5f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        attack_sfx = AudioManager.audioList.shot_pistol;
    }


    protected override void Attack()
    {
        base.Attack();
        SpawnBullet(shootingPlayer);
        HandleShotSFX(projectileSpawnPoint.position, audioSource);
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }
}
