using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableExplosive : Throwable
{
    [SerializeField] float timeToExplode;

    [SerializeField] float dmg = 90f;
    [SerializeField] float explosionForce = 100;


    private void OnEnable()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private void Explode()
    {
        GameObject explosionGO = ExplosionPool.explosionPoolSingleton.GetPooledExplosion(shootingPlayer, dmg, explosionForce);
        if (explosionGO != null)
        {
            explosionGO.transform.position = gameObject.transform.position;
            explosionGO.SetActive(true);
        }  
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(timeToExplode);
        Explode();
        Destroy(gameObject);
    }
}
