using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public float repeatRate = 1;
    public float pickUpDuration = 10f;
    private float spawnSpacingMultipier = 5f;

    private void Awake()
    {
        PickUp.pickUpDuration = pickUpDuration;
    }

    private void Start()
    {
        //PickUp.pickUpDuration = pickUpDuration;
        repeatRate = SetUp.spawnSpacing * spawnSpacingMultipier;
        InvokeRepeating("SpawnRandomPickUp", repeatRate, repeatRate);
    }

    private void SpawnRandomPickUp()
    {
        GameObject pickUp = PickUpPool.pickUpPoolSingleton.GetRandomPickup();
        if (pickUp == null) return;
        pickUp.transform.position = Util.GetRandomSpawnPosition();
        pickUp.SetActive(true);
        pickUp.GetComponent<PickUp>().DecideIfWeapon();
    }
}
