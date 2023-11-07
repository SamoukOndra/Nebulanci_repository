using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPool : MonoBehaviour
{
    public static RocketPool rocketPoolSingleton;
    public List<GameObject> pooledRockets;
    public GameObject rocketToPool;
    public int amountToPool;

    [SerializeField] float speed = 20f;
    [SerializeField] float dmg = 150;

    void Awake()
    {
        rocketPoolSingleton = this;
    }

    void Start()
    {
        pooledRockets = new List<GameObject>();
        GameObject pooledRocket;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledRocket = Instantiate(rocketToPool);
            SetRocketSpeedAndDmg(pooledRocket);
            pooledRocket.SetActive(false);
            pooledRockets.Add(pooledRocket);
        }
    }
    public GameObject GetPooledRocket()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledRockets[i].activeInHierarchy)
            {
                return pooledRockets[i];
            }
        }
        return null;
    }

    private void SetRocketSpeedAndDmg(GameObject pooledRocket)
    {
        if (pooledRocket.TryGetComponent(out Rocket rocketScript))
        {
            rocketScript.speed = this.speed;
            rocketScript.dmg = this.dmg;
        }
    
        else Debug.Log("Rocket script missing!!!");
    }

    [ContextMenu("Set rockets speed and dmg")]
    private void SetRocketsSpeedAndDamage()
    {
        foreach (GameObject rocket in pooledRockets)
        {
            SetRocketSpeedAndDmg(rocket);
        }
    }
}
