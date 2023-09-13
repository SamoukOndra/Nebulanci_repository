using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public float repeatRate = 1;
    public float pickUpDuration = 3f;

    private void Start()
    {
        PickUp.pickUpDuration = pickUpDuration;
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
