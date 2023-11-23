using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMeleeColliderHandler : MonoBehaviour
{
    [SerializeField] float dmg = 10f;

    NpcNavigation navigation;
    AnimatorHandler_Zombie animator;
    GameObject targetPlayer;

    private void Awake()
    {
        navigation = GetComponentInParent<NpcNavigation>();
        animator = transform.parent.GetComponentInChildren<AnimatorHandler_Zombie>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            navigation.enabled = false;
            animator.AnimateAttack();
            targetPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetPlayer) targetPlayer = null;
    }

    private void OnDisable()
    {
        if(targetPlayer != null)
        {
            PlayerHealth health = targetPlayer.GetComponent<PlayerHealth>();
            health.DamageAndReturnValidKill(dmg);
        }
    }
}
