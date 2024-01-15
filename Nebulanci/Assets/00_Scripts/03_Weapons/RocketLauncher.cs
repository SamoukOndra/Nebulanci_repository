using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapons
{
    [SerializeField] GameObject launcherRocket;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 10;
        CooldownDuration = 1.5f;

        MaxAmmo = 5;
        currentAmmo = 1;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        attack_sfx = AudioManager.audioList.shot_rocketLauncher;
    }


    protected override void Attack()
    {
        animatorHandler.ActivateAnimatorAttack();

        StartCoroutine(LauncherRocketVisibilityCoroutine(CooldownDuration - 0.05f));

        SpawnRocket(shootingPlayer);

        HandleShotSFX(projectileSpawnPoint.position, audioSource);
    }

    public override int Reload()
    {
        if (currentAmmo < MaxAmmo)
        {
            if (!IsDefault)
                currentAmmo++;

            else currentAmmo = MaxAmmo;
        }

        return currentAmmo;
    }

    protected void SpawnRocket(GameObject shootingPlayer)
    {
        GameObject rocket = RocketPool.rocketPoolSingleton.GetPooledRocket();
        if (rocket != null)
        {
            rocket.GetComponent<Rocket>().shootingPlayer = shootingPlayer;
            rocket.transform.SetPositionAndRotation(projectileSpawnPoint.transform.position, shootingPlayer.transform.rotation);
            rocket.SetActive(true);
        }
    }

    IEnumerator LauncherRocketVisibilityCoroutine(float deactivationDuration)
    {
        launcherRocket.SetActive(false);

        yield return new WaitForSeconds(deactivationDuration);

        launcherRocket.SetActive(true);
    }
}
