using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBuff : PickUpBuff
{
    public override void Interact(GameObject player)
    {
        if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.ArmorBuff();
        }

        else Debug.Log("ERROR: Invalid PickUp target !!!!");
    }
}
