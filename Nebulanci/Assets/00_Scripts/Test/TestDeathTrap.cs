using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeathTrap : MonoBehaviour
{
    Collider trigger;

    private void Start()
    {
        trigger = GetComponent<Collider>();
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
            health.DamageAndReturnValidKill(200);
    }
}
