using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : PickUpBuff
{
    [SerializeField] float healAmount = 50;

    public override void Interact(GameObject player)
    {
        if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Heal(healAmount);
        }

        else Debug.Log("ERROR: Invalid PickUp target !!!!");
    }
}
