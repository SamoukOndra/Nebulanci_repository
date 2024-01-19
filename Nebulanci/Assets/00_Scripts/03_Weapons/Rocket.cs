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
    Transform thisTransform;

    private void Start()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;

        thisTransform = GetComponent<Transform>();
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
        if(other.TryGetComponent(out CollisionMaterials Cm))
        {
            if(Cm.GetIsBulletProof() == false)
            {
                Cm.Interact(thisTransform.position, thisTransform.rotation);
                return;
            }
        }

        Explode();
        gameObject.SetActive(false);
    }

    private void Explode()
    {
        GameObject explosionGO = ExplosionPool.explosionPoolSingleton.GetPooledExplosion(shootingPlayer, dmg, explosionForce);
        if (explosionGO != null)
        {
            explosionGO.transform.position = thisTransform.position;
            explosionGO.SetActive(true);
        }
    }
}
