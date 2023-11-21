using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPool : MonoBehaviour
{
    public static NpcPool singl;
    private List<GameObject> pooledNpcs;
    [SerializeField] GameObject npcToPool;

    private List<GameObject> pooledNpcDeaths;
    [SerializeField] GameObject npcDeathToPool;

    public int amountToPool;

    //[SerializeField] float npcHealth = 50;

    void Awake()
    {
        singl = this;
    }

    void Start()
    {
        pooledNpcs = new List<GameObject>();
        GameObject pooledNpc;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledNpc = Instantiate(npcToPool);

            //NpcHealth npcHealth = pooledNpc.GetComponent<NpcHealth>();
            //npcHealth.maxHealth = this.npcHealth;

            pooledNpc.SetActive(false);
            pooledNpcs.Add(pooledNpc);
        }

        pooledNpcDeaths = new List<GameObject>();
        GameObject pooledDeath;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledDeath = Instantiate(npcDeathToPool);
            NpcDeath death = pooledDeath.GetComponent<NpcDeath>();
            death.Initialize();
            pooledDeath.SetActive(false);
            pooledNpcDeaths.Add(pooledDeath);
        }
    }
    public GameObject GetPooledNpc()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledNpcs[i].activeInHierarchy)
            {
                return pooledNpcs[i];
            }
        }
        return null;
    }

    public GameObject GetPooledNpcDeath()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledNpcDeaths[i].activeInHierarchy)
            {
                return pooledNpcDeaths[i];
            }
        }
        return null;
    }
}
