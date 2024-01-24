using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeColliderHandler : MonoBehaviour
{
    [SerializeField] float meleeDmg = 20f;

    private List<Health> healthsToHit = new();


    private void OnDisable()
    {

        if (healthsToHit != null && healthsToHit.Count > 0)
        {
            foreach (Health h in healthsToHit)
            {
                if (h.DamageAndReturnValidKill(meleeDmg))
                    EventManager.InvokeOnPlayerKill(transform.parent.gameObject);
            }
        }

        healthsToHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (!healthsToHit.Contains(health))
            {
                healthsToHit.Add(health);
            }
                
        }

        if (other.TryGetComponent(out CollisionMaterials cm))
        {
            Vector3 hitPoint = gameObject.transform.position;
            Vector3 hitDirection = (cm.gameObject.transform.position - (hitPoint + Vector3.down)).normalized;
            Quaternion rotation = Quaternion.LookRotation(hitDirection);
            cm.Interact(hitPoint, rotation, gameObject.transform.parent.gameObject);
        }
    }
}
