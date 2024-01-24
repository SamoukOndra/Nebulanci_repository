using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrapTrigger : MonoBehaviour
{
    private GameObject shootingPlayer;

    private float dmg = 200f;
    private float explosionForce = 5000f;

    public void SetShootingPlayer(GameObject shootingPlayer)
    {
        this.shootingPlayer = shootingPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = ExplosionPool.explosionPoolSingleton.GetPooledExplosion(shootingPlayer, dmg, explosionForce);
        if (explosion == null) return;

        explosion.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        explosion.SetActive(true);

        Destroy(transform.parent.gameObject);
    }
}
