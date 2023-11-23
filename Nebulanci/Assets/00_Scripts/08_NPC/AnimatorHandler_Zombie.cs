using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorHandler_Zombie : MonoBehaviour
{
    private const int amountOfAttackTypes = 3;

    NavMeshAgent agent;
    Animator animator;
    NpcNavigation npcNavigation;

    float maxVelocity;
    float velocity;

    int _velocity;
    int _attack;

    float activateDelay = 1;
    bool isActive;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        npcNavigation = GetComponentInParent<NpcNavigation>();

        maxVelocity = agent.speed;

        _velocity = Animator.StringToHash("Velocity");
        _attack = Animator.StringToHash("Attack");

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

    public void AnimateAttack()
    {
        int attackType = Random.Range(1, amountOfAttackTypes); // 0 pro exit attack animace
        animator.SetInteger(_attack, attackType);
        StartCoroutine(AttackCoroutine());
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

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetInteger(_attack, 0);
    }
}
