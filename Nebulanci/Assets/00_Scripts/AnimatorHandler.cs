using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{
    public Transform weaponSlotTransform;

    //misto awake ma SG spesl fci, snad se to nekupi nebo co

    Animator anim;

    int _move;
    int _weaponID;
    //int _cooldownActive; netreba, cd se resi v CombatHandleru
    int _attack;

    //int attacksLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        _move = Animator.StringToHash("Move");
        _weaponID = Animator.StringToHash("Weapon ID");
        //_cooldownActive = Animator.StringToHash("Cooldown Active");
        _attack = Animator.StringToHash("Attack");

        //attacksLayer = anim.GetLayerIndex("Attack Layer");
    }

    public void UpdateAnimatorMove(Vector3 moveDirection)
    {
        if (moveDirection == Vector3.zero) { anim.SetFloat(_move, 0, 0.1f, Time.deltaTime); }
        else { anim.SetFloat(_move, 1, 0.1f, Time.deltaTime); }
    }

    public void SetAnimatorWeaponID(int weaponID)
    {
        anim.SetInteger(_weaponID, weaponID);
    }

    public void ActivateAnimatorAttack()
    {
        anim.SetBool(_attack, true);
        StartCoroutine(SetAttackToFalseInSecs(0.1f));
    }

    IEnumerator SetAttackToFalseInSecs(float countdown)
    {
        yield return new WaitForSeconds(countdown);
        anim.SetBool(_attack, false);
    }

    //public void ActivateAttackLayer(bool activate)
    //{
    //    float weight;
    //
    //    if (activate)
    //        weight = 1;
    //
    //    else weight = 0;
    //
    //    anim.SetLayerWeight(attacksLayer, weight);
    //}

    //public void SetAnimatorAttack(bool startAttack)
    //{
    //    anim.SetBool(_attack, startAttack);
    //}

    //public void AnimatorFireShotgun()
    //{
    //    anim.SetLayerWeight(attacksLayer, 1f);
    //    anim.SetBool(_onAttack, true);
    //    
    //    //StartCoroutine(ResetLayerWeightInSecs(attacksLayer, shotgunCooldown));
    //}
    //
    //public void AnimatorFirePistol()
    //{
    //
    //}

    //IEnumerator ResetLayerWeightInSecs(int layerIndex, float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    anim.SetLayerWeight(layerIndex, 0f);
    //}
    //
    //IEnumerator CooldownCoroutine(float duration)
    //{
    //    anim.SetBool(_cooldownActive, true);
    //    yield return new WaitForSeconds(duration);
    //    anim.SetBool(_cooldownActive, false);
    //}
}
