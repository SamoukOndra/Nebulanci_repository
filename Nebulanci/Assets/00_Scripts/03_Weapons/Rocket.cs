using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject shootingPlayer;

    public float dmg = 150f;
    public float speed = 20f;
    [SerializeField] float explosionForce = 5000;

    [SerializeField] new ParticleSystem particleSystem;
    

    Collider trigger;

    private void Start()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
    }

    private void OnEnable()
    {
        particleSystem.Play();
    }

    private void OnDisable()
    {
        particleSystem.Pause();
    }

    private void Update()
    {
        gameObject.transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
        gameObject.SetActive(false);
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
}
