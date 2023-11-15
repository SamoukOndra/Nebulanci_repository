using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickUpSpawner : MonoBehaviour
{
    public List<GameObject> pooledPickUps;
    [SerializeField] GameObject pickUp;
    public int amountToPool;

    private List<GameObject> pooledPickUpsItems = new();

    public List<GameObject> itemsToPool;
    public List<int> itemsAmountsToPool;

    [SerializeField] float spawnRate = 5;


    void Start()
    {
        for (int i = 0; i < itemsToPool.Count; i++)
        {
            int amount = itemsAmountsToPool[i];
            for (int a = 0; a < amount; a++)
            {
                GameObject item = Instantiate(itemsToPool[i]);
                pooledPickUpsItems.Add(item);
                item.SetActive(false);
            }
        }


        for (int i = 0; i < amountToPool; i++)
        {
            GameObject _pickUp = Instantiate(pickUp);
            pooledPickUps.Add(_pickUp);
            _pickUp.SetActive(false);
        }

        InvokeRepeating("SpawnRandomPickUp", spawnRate, spawnRate);
    }

    public GameObject GetRandomPickup()
    {
        GameObject pickUp = GetPickUp();
        GameObject item = GetRandomItem();

        if (pickUp == null || item == null) return null;

        PickUp script = pickUp.GetComponent<PickUp>();
        script.pickUpItem = item;

        return pickUp;
    }


    private GameObject GetPickUp()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledPickUps[i].activeInHierarchy)
            {
                return pooledPickUps[i];
            }
        }
        return null;
    }

    private GameObject GetRandomItem()
    {
        int listLength = pooledPickUpsItems.Count;
        int rand = Random.Range(0, listLength);
        int i = 1;

        while (pooledPickUpsItems[rand].activeInHierarchy && i < listLength)
        {
            if (rand == (listLength - 1))
                rand = 0;
            else rand++;

            i++;
        }

        if (!pooledPickUpsItems[rand].activeInHierarchy)
        {
            return pooledPickUpsItems[rand];
        }

        else return null;
    }

    private void SpawnRandomPickUp()
    {
        GameObject pickUp = GetRandomPickup();
        if (pickUp == null) return;
        pickUp.transform.position = Util.GetRandomSpawnPosition();
        pickUp.SetActive(true);
        pickUp.GetComponent<PickUp>().DecideIfWeapon();
    }
}
