using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAttackTrigger : MonoBehaviour
{
    NpcSimpleHandler npcHandler;

    private void Awake()
    {
        npcHandler = GetComponentInParent<NpcSimpleHandler>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcHandler.Attack();
        }
    }
}
