using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool audioSourcePoolSingleton;
    public List<GameObject> pooledAS_rifleShot;
    public GameObject AS_rifleShotToPool;
    public int amountToPool;

    void Awake()
    {
        audioSourcePoolSingleton = this;
    }

    void Start()
    {
        pooledAS_rifleShot = new List<GameObject>();
        GameObject pooledAS;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledAS = Instantiate(AS_rifleShotToPool);
            pooledAS.SetActive(false);
            pooledAS_rifleShot.Add(pooledAS);
        }
    }
    public GameObject GetPooledAS_rifleShot()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledAS_rifleShot[i].activeInHierarchy)
            {
                return pooledAS_rifleShot[i];
            }
        }
        return null;
    }
}
