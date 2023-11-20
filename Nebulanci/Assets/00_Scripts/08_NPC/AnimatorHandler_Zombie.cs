using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorHandler_Zombie : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    NpcNavigation npcNavigation;

    float maxVelocity;
    float velocity;

    int _velocity;

    float activateDelay = 1;
    bool isActive;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        npcNavigation = GetComponentInParent<NpcNavigation>();

        maxVelocity = agent.speed;

        _velocity = Animator.StringToHash("Velocity");
        activateDelay = CalculateActivateDelay();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedActivateCoroutine(activateDelay));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Activate(false);
    }

    private void Update()
    {
        if (isActive)
        {
            UpdateMove();
        }

    }

    private void UpdateMove()
    {
        velocity = agent.velocity.magnitude / maxVelocity;
        animator.SetFloat(_velocity, velocity);
    }

    private void Activate(bool activate)
    {
        isActive = activate;
        npcNavigation.enabled = activate;
    }

    private float CalculateActivateDelay()
    {
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        return delay;
    }

    IEnumerator DelayedActivateCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Activate(true);
    }
}
