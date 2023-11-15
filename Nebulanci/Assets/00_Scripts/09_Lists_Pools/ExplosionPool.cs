using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool explosionPoolSingleton;
    public List<GameObject> pooledExplosions;
    public GameObject explosionToPool;
    public int amountToPool;

    void Awake()
    {
        explosionPoolSingleton = this;
    }

    void Start()
    {
        pooledExplosions = new List<GameObject>();
        GameObject pooledExplosion;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledExplosion = Instantiate(explosionToPool);
            pooledExplosion.SetActive(false);
            pooledExplosions.Add(pooledExplosion);
        }
    }
    public GameObject GetPooledExplosion(GameObject shootingPlayer, float dmg, float explosionForce)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledExplosions[i].activeInHierarchy)
            {
                Explosion explosion = pooledExplosions[i].GetComponent<Explosion>();
                
                explosion.shootingPlayer = shootingPlayer;
                explosion.dmg = dmg;
                explosion.explosionForce = explosionForce;

                return pooledExplosions[i];
            }
        }
        return null;
    }
}
