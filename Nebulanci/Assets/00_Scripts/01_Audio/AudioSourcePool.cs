using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool audioSourcePoolSingleton;
    
    [HideInInspector]
    public List<GameObject> pooledAS_rifleShot = new();
    
    [SerializeField] GameObject AS_rifleShotToPool;
    [SerializeField] int amountToPool_rifle;

    [Space]
    [HideInInspector]
    public List<GameObject> pooledAS_pickup = new();

    [SerializeField] GameObject AS_pickupToPool;
    [SerializeField] int amountToPool_pickup;

    void Awake()
    {
        audioSourcePoolSingleton = this;
    }

    void Start()
    {
        //pooledAS_rifleShot = new List<GameObject>();
        GameObject pooledRifleAS;
        for (int i = 0; i < amountToPool_rifle; i++)
        {
            pooledRifleAS = Instantiate(AS_rifleShotToPool);
            pooledRifleAS.SetActive(false);
            pooledAS_rifleShot.Add(pooledRifleAS);
        }

        GameObject pooledPickupAS;
        for (int j = 0; j <amountToPool_pickup; j++)
        {
            pooledPickupAS = Instantiate(AS_pickupToPool);
            pooledPickupAS.SetActive(false);
            pooledAS_pickup.Add(pooledPickupAS);
        }
    }
    public GameObject GetPooledAS_rifleShot()
    {
        for (int i = 0; i < amountToPool_rifle; i++)
        {
            if (!pooledAS_rifleShot[i].activeInHierarchy)
            {
                return pooledAS_rifleShot[i];
            }
        }
        return null;
    }

    public GameObject GetPooledAS_pickup()
    {
        for (int i = 0; i < amountToPool_pickup; i++)
        {
            if (!pooledAS_pickup[i].activeInHierarchy)
            {
                return pooledAS_pickup[i];
            }
        }
        return null;
    }
}
