using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public GameObject shootingPlayer;

    private AudioSource audioSource;
    private AudioClip audioClip;
    //private AudioList audioList;

    private SphereCollider trigger;

    private new ParticleSystem particleSystem;

    [HideInInspector] public float explosionForce;
    [HideInInspector] public float dmg;

    private float radius;
    private float duration = 2f;

    private void Awake()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;

        radius = trigger.radius;

        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if(audioClip == null)
        {
            audioClip = AudioManager.audioList.explosion;
        }
            
        trigger.enabled = true;
        particleSystem.Play();
        Util.RandomizePitch(audioSource, .2f);
        audioSource.PlayOneShot(audioClip);
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

        if (other.TryGetComponent(out CollisionMaterials collisionMaterials))
        {
            Vector3 direction = gameObject.transform.position - other.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Vector3 hitPoint = other.transform.position + other.transform.up;

            collisionMaterials.Interact(hitPoint, rotation);
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
