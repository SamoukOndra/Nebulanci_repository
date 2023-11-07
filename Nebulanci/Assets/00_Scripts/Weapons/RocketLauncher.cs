using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapons
{
    [SerializeField] GameObject launcherRocket;

    protected override void Awake()
    {
        base.Awake();

        WeaponID = 10;
        CooldownDuration = 3f;

        MaxAmmo = 5;
        currentAmmo = 1;
    }


    protected override void Attack()
    {
        animatorHandler.ActivateAnimatorAttack();

        StartCoroutine(LauncherRocketVisibilityCoroutine(CooldownDuration - 0.05f));

        SpawnRocket(shootingPlayer);
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
