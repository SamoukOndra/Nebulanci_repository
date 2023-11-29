using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcSimpleHandler : MonoBehaviour
{
    NpcMeleeTriggerHandler meleeTriggerhandler;

    AudioSource audioSource;

    NpcNavigation navigation;

    NpcAnimatorHandler animatorHandler;

    [SerializeField] Collider attackRadiusTrigger;

    [SerializeField] List<AnimationClip> attackAnimations;
    List<float> attackAnimationsLengths = new();
    readonly List<float> attackTimeFractions = new List<float> { .33f, .33f }; // protoze animation event transform bug
    readonly float attackAnimationSpeed = 2; //musi souhlasit s animation speed v animatoru

    float activationOnSpawnDelay;

    bool canAttack;

    private void Awake()
    {
        meleeTriggerhandler = GetComponentInChildren<NpcMeleeTriggerHandler>();

        audioSource = GetComponent<AudioSource>();

        navigation = GetComponent<NpcNavigation>();
        animatorHandler = GetComponentInChildren<NpcAnimatorHandler>();

        foreach(AnimationClip clip in attackAnimations)
        {
            attackAnimationsLengths.Add(clip.length/attackAnimationSpeed);
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
        yield return new WaitForSeconds(attackTimeFractions[attackType] * attackAnimationsLengths[attackType]);

        meleeTriggerhandler.HitPlayer();

        animatorHandler.AnimatorUpdateAttack(-1);
        yield return new WaitForSeconds((1 - attackTimeFractions[attackType]) * attackAnimationsLengths[attackType]);

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

        Util.RandomizePitch(audioSource, .2f);
        audioSource.PlayOneShot(AudioManager.audioList.GetZombieSpawn());
    }

    IEnumerator DelayedActivateCoroutine()
    {
        Activate(false);
        yield return new WaitForSeconds(activationOnSpawnDelay);
        Activate(true);
        
    }

    private void Activate(bool acitvate)
    {
        navigation.enabled = acitvate;
        canAttack = acitvate;
    }
}
