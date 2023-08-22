using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{
    public Transform weaponTransform;

    //misto awake ma SG spesl fci, snad se to nekupi nebo co

    Animator anim;

    int move;
    int weaponType;
    int cooldownActive;
    int onAttack;

    int attacksLayer;

    // cooldowns //////////////////////////
    float shotgunCooldown = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        move = Animator.StringToHash("Move");
        weaponType = Animator.StringToHash("Weapon Type");
        cooldownActive = Animator.StringToHash("Cooldown Active");
        onAttack = Animator.StringToHash("On Attack");

        attacksLayer = anim.GetLayerIndex("Attacks Layer");
    }

    public void UpdateAnimatorMove(Vector3 moveDirection)
    {
        //Debug.Log("UpdateAnimMove");
        if (moveDirection == Vector3.zero) { anim.SetFloat(move, 0, 0.1f, Time.deltaTime); }
        else { anim.SetFloat(move, 1, 0.1f, Time.deltaTime); }
    }

    public void FireShotgun()
    {
        anim.SetLayerWeight(attacksLayer, 1f);
        anim.SetBool(onAttack, true);
        
        //StartCoroutine(ResetLayerWeightInSecs(attacksLayer, shotgunCooldown));
    }

    public void FirePistol()
    {

    }

    IEnumerator ResetLayerWeightInSecs(int layerIndex, float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.SetLayerWeight(layerIndex, 0f);
    }

    IEnumerator CooldownCoroutine(float duration)
    {
        anim.SetBool(cooldownActive, true);
        yield return new WaitForSeconds(duration);
        anim.SetBool(cooldownActive, false);
    }
}
