using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject shootingPlayer;

    private SphereCollider trigger;

    private new ParticleSystem particleSystem;

    public float explosionForce;
    public float dmg;

    private float radius;
    private float duration = 2f;

    private void Awake()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;

        radius = trigger.radius;

        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        trigger.enabled = true;
        particleSystem.Play();
        EventManager.InvokeOnExplosion(gameObject.transform.position);
        StartCoroutine(DisableSelfCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent(out Health health))
        {
            float distance = Vector3.Distance(gameObject.transform.position, other.transform.position);
            float dmgPortion = 1 - (distance/radius);

            Debug.Log(other + " received dmg: " + (dmgPortion * dmg) + " from " + dmgPortion + " distance.");

            if(health.DamageAndReturnValidKill(dmgPortion * dmg))
            {
                if(other.CompareTag("Player"))
                    EventManager.InvokeOnPlayerKill(shootingPlayer);

                return;
            }
                
        }

        if (other.TryGetComponent(out Rigidbody rb))
        {
            rb.AddExplosionForce(explosionForce, gameObject.transform.position, radius);
        }
    }

    IEnumerator DisableSelfCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        trigger.enabled = false;

        yield return new WaitForSeconds(duration - 0.1f);
        gameObject.SetActive(false);
    }
}
