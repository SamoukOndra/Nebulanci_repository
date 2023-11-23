using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAttackHandler : MonoBehaviour
{
    [SerializeField] GameObject meleeCollider;

    public void FinishAttack(GameObject attacker)
    {
        if(attacker == gameObject)
        {
            meleeCollider.SetActive(false);
        }
    }
}
