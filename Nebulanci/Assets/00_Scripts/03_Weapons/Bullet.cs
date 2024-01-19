using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject shootingPlayer;

    public float speed;
    public float dmg;

    Collider trigger;
    Transform thisTransform;

    private bool isBulletProofCM;

    private void Start()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;

        thisTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        thisTransform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (health.gameObject == shootingPlayer) return;
            if (health.DamageAndReturnValidKill(dmg))
                EventManager.InvokeOnPlayerKill(shootingPlayer);
        }
    
        if (other.TryGetComponent(out CollisionMaterials collisionMaterial))
        {
            isBulletProofCM = collisionMaterial.Interact(thisTransform.position, thisTransform.rotation, shootingPlayer);
            if (!isBulletProofCM) return;
                
        }

        gameObject.SetActive(false);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision");
    //
    //    if (collision.gameObject.TryGetComponent(out Health health))
    //    {
    //        if (health.gameObject == shootingPlayer) return;
    //        if (health.DamageAndReturnValidKill(dmg))
    //            EventManager.InvokeOnPlayerKill(shootingPlayer);
    //    }
    //
    //    gameObject.SetActive(false);
    //}
}
