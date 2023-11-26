using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcSimpleHandler : MonoBehaviour
{
    NpcNavigation navigation;

    NpcAnimatorHandler animatorHandler;

    [SerializeField] Collider attackRadiusTrigger;

    [SerializeField] List<AnimationClip> attackAnimations;
    List<float> attackAnimationsLengths = new();

    float activationOnSpawnDelay;

    bool canAttack;

    private void Awake()
    {
        navigation = GetComponent<NpcNavigation>();
        animatorHandler = GetComponentInChildren<NpcAnimatorHandler>();

        foreach(AnimationClip clip in attackAnimations)
        {
            attackAnimationsLengths.Add(clip.length);
        }

        activationOnSpawnDelay = animatorHandler.CalculateActivateDelay();

        Activate(false);
    }

    private void OnEnable()
    {
        HandleSpawn();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Attack()
    {
        if (!canAttack) return;
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        Activate(false);
        
        int attackType = DecideAttackType();
        animatorHandler.AnimatorUpdateAttack(attackType);
        yield return new WaitForSeconds(.1f);
        animatorHandler.AnimatorUpdateAttack(-1);
        yield return new WaitForSeconds(attackAnimationsLengths[attackType]);

        Activate(true);
    }

    int DecideAttackType()
    {
        int a = Random.Range(0, attackAnimations.Count);
        return a;
    }

    public void HandleSpawn()
    {
        StartCoroutine(DelayedActivateCoroutine());
    }

    IEnumerator DelayedActivateCoroutine()
    {
        yield return new WaitForSeconds(activationOnSpawnDelay);
        Activate(true);
        
    }

    private void Activate(bool acitvate)
    {
        navigation.enabled = acitvate;
        canAttack = acitvate;
    }
}
