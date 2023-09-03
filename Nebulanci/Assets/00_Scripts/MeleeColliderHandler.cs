using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeColliderHandler : MonoBehaviour
{
    [SerializeField] float meleeDmg = 20f;

    private List<Health> healthsToHit = new();


    private void OnDisable()
    {
        if (healthsToHit != null)
        {
            foreach (Health h in healthsToHit)
            {
                h.Damage(meleeDmg);
            }
        }
        else Debug.Log("no melee targets");

        healthsToHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (!healthsToHit.Contains(health))
                healthsToHit.Add(health);
        }
                //health.Damage(meleeDmg);
    }
}
