using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPool : MonoBehaviour
{
    public static PickUpPool pickUpPoolSingleton;
    
    public List<GameObject> pooledPickUps;
    [SerializeField] GameObject pickUp;
    public int amountToPool;

    [SerializeField] GameObject buffList;
    private List<GameObject> itemsToPool;

    private List<GameObject> pooledPickUpsItems = new();

    
    //public List<int> itemsAmountsToPool = new();


    void Awake()
    {
        pickUpPoolSingleton = this;
    }

    void Start()
    {
        itemsToPool = buffList.GetComponent<BuffList>().allBuffs;

        for (int i = 0; i < itemsToPool.Count; i++)
        {
            PoolItem(i);
        }

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject _pickUp = Instantiate(pickUp);
            pooledPickUps.Add(_pickUp);
            _pickUp.SetActive(false);
        }
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
        if (listLength < 1) return null;

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

    private void PoolItem(int index)
    {
        int quantity = SetUp.GetQuantityFromPickUpPair(index);
        if(quantity > 0)
        {
            for(int i = 0; i < quantity; i++)
            {
                GameObject item = Instantiate(itemsToPool[index]);
                pooledPickUpsItems.Add(item);
                item.SetActive(false);
            }
        }
    }
}
