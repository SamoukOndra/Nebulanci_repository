using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLaser : MonoBehaviour
{
    [SerializeField] GameObject laser;
    private GameObject thisLaser;

    Transform weaponProjectileSpawnPoint;

    private void Awake() ////////////////////////////////////////////////vjlfsdvjkvbvjksd
    {
        weaponProjectileSpawnPoint = GetComponent<Weapons>().GetProjectileSpawnPoint();
        thisLaser = Instantiate(laser, weaponProjectileSpawnPoint.position, gameObject.transform.rotation, weaponProjectileSpawnPoint);
        //EnableLaser(false);
    }

    public void AttLaser()
    {
        weaponProjectileSpawnPoint = GetComponent<Weapons>().GetProjectileSpawnPoint();
        Instantiate(laser, weaponProjectileSpawnPoint.position, gameObject.transform.rotation, weaponProjectileSpawnPoint);
    }

    public void EnableLaser(bool enable)
    {
        thisLaser.SetActive(enable);
    }
}
