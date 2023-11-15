using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smg : Weapons
{
    [SerializeField] float timePerBullet = 0.15f;
    [SerializeField] int bulletAmount = 5;
    [SerializeField] float bulletSpread = 10f;
    private float halfSpread;


    protected override void Awake()
    {
        base.Awake();

        WeaponID = 4;
        CooldownDuration = 2f;

        MaxAmmo = 5;
        currentAmmo = MaxAmmo;

        halfSpread = bulletSpread * 0.5f;
    }

    private void Start()
    {
        attack_sfx = AudioManager.audioList.shot_rifle;
    }


    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(SmgAttackCoroutine(timePerBullet));
    }

    public override int Reload()
    {
        currentAmmo = MaxAmmo;
        return currentAmmo;
    }

    private IEnumerator SmgAttackCoroutine(float timePerBullet)
    {
        float timer = timePerBullet;
        int i = 0;
        
        while(i < bulletAmount)
        {
            if (timer >= timePerBullet)
            {
                float yOffset = Random.Range(-halfSpread, halfSpread);
                Vector3 offset = Vector3.up * yOffset;
                SpawnBullet(shootingPlayer, offset);

                HandleShotSFX(projectileSpawnPoint.position);

                timer = 0;
                i++;
            }

            else timer += Time.deltaTime;

            yield return null;
        }
    }
}
