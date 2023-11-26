using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAnimatorHandler : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    float maxVelocity;

    int _velocity;
    int _attack;

    //float activateDelay = 1;
    //bool isActive;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();

        maxVelocity = agent.speed;

        _velocity = Animator.StringToHash("Velocity");
        _attack = Animator.StringToHash("Attack");

        //activateDelay = CalculateActivateDelay();
        
        AnimatorUpdateAttack(-1);
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(DelayedActivateCoroutine(activateDelay));
    //}

    private void Update()
    {
        AnimatorUpdateMove();
    }

    private void AnimatorUpdateMove()
    {
        float velocity = agent.velocity.magnitude / maxVelocity;
        animator.SetFloat(_velocity, velocity);
        //Debug.Log("agent velosicty: " + velocity);
    }

    public void AnimatorUpdateAttack(int attackType)
    {
        animator.SetInteger(_attack, attackType);
        Debug.Log("anim attack set to: " + attackType);
    }


    public float CalculateActivateDelay()
    {
        animator = GetComponent<Animator>();
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        return delay;
    }

    //IEnumerator DelayedActivateCoroutine(float delay, NpcNavigation npcNavigation)
    //{
    //    yield return new WaitForSeconds(delay);
    //    //Activate(true);
    //    npcNavigation.enabled = true;
    //}

    //private void Activate(bool activate)
    //{
    //    isActive = activate;
    //    //npcNavigation.enabled = activate;
    //}

    //public void HandleSpawnAnimation(NpcNavigation npcNavigation)
    //{
    //    npcNavigation.enabled = false;
    //    StartCoroutine(DelayedActivateCoroutine(activateDelay, npcNavigation));
    //}
}
