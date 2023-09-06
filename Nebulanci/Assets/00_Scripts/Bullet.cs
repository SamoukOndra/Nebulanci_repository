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
        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (health.gameObject == shootingPlayer) return;
            if (health.DamageAndReturnValidKill(dmg))
                EventManager.InvokeOnPlayerKill(shootingPlayer);
        }

        gameObject.SetActive(false);
    }
}
