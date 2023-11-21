using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmHitPool : MonoBehaviour
{
    public static CmHitPool singl;
    public List<GameObject> pooledHits;
    public GameObject hitToPool;
    public int amountToPool;

    void Awake()
    {
        singl = this;
    }

    void Start()
    {
        this.pooledHits = new List<GameObject>();
        GameObject pooledHit;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledHit = Instantiate(hitToPool);
            
            CmHit hit = pooledHit.GetComponent<CmHit>();
            hit.Initialize();

            pooledHit.SetActive(false);
            pooledHits.Add(pooledHit);
        }
    }
    public GameObject GetPooledHit(int identifier)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledHits[i].activeInHierarchy)
            {
                CmHit hit = pooledHits[i].GetComponent<CmHit>();
                hit.SetIdentifier(identifier);

                return pooledHits[i];
            }
        }
        return null;
    }
}
