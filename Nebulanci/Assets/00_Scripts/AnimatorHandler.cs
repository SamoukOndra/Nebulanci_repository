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
    int _cooldownActive;
    int _attack;

    int attacksLayer;

    // cooldowns //////////////////////////
    float shotgunCooldown = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        _move = Animator.StringToHash("Move");
        _weaponID = Animator.StringToHash("Weapon ID");
        _cooldownActive = Animator.StringToHash("Cooldown Active");
        _attack = Animator.StringToHash("Attack");

        attacksLayer = anim.GetLayerIndex("Attacks Layer");
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

    public void SetAnimatorAttack(bool startAttack)
    {
        anim.SetBool(_attack, startAttack);
    }

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

    IEnumerator ResetLayerWeightInSecs(int layerIndex, float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.SetLayerWeight(layerIndex, 0f);
    }

    IEnumerator CooldownCoroutine(float duration)
    {
        anim.SetBool(_cooldownActive, true);
        yield return new WaitForSeconds(duration);
        anim.SetBool(_cooldownActive, false);
    }
}
