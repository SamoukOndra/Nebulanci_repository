using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject shootingPlayer;

    public float speed;
    public float dmg;

    Collider trigger;

    private void Start()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
    }

    private void Update()
    {
        gameObject.transform.Translate(speed * Time.deltaTime * Vector3.forward);
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
            collisionMaterial.Interact(gameObject.transform.position, gameObject.transform.rotation, shootingPlayer);
            //if (collisionMaterial is PropaneTankCM)
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
