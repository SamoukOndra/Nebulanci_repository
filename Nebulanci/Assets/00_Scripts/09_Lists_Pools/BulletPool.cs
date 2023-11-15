using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolSingleton;
    public List<GameObject> pooledBullets;
    public GameObject bulletToPool;
    public int amountToPool;

    [SerializeField] float speed;
    [SerializeField] float dmg;

    void Awake()
    {
        bulletPoolSingleton = this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject pooledBullet;
        for (int i = 0; i < amountToPool; i++)
        {
            pooledBullet = Instantiate(bulletToPool);
            SetBulletSpeedAndDmg(pooledBullet);            
            pooledBullet.SetActive(false);
            pooledBullets.Add(pooledBullet);
        }
    }
    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }

    private void SetBulletSpeedAndDmg(GameObject pooledBullet)
    {
        if (pooledBullet.TryGetComponent(out Bullet bulletScript))
        {
            bulletScript.speed = this.speed;
            bulletScript.dmg = this.dmg;
        }
            
        else Debug.Log("Bullet script missing!!!");
    }

    [ContextMenu("Set bullets speed and dmg")]
    private void SetBulletsSpeedAndDamage()
    {
        foreach(GameObject bullet in pooledBullets)
        {
            SetBulletSpeedAndDmg(bullet);
        }
    }
}