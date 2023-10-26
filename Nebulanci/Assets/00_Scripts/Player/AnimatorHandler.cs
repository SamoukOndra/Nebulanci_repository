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
    int _attack;
    int _itemGrabbed;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        _move = Animator.StringToHash("Move");
        _attack = Animator.StringToHash("Attack");
        _itemGrabbed = Animator.StringToHash("Item Grabbed"); // zatim nepouzity
    }

    public void UpdateAnimatorMove(Vector3 moveDirection)
    {
        if (moveDirection == Vector3.zero) { anim.SetFloat(_move, 0, 0.1f, Time.deltaTime); }
        else { anim.SetFloat(_move, 1, 0.1f, Time.deltaTime); }
    }

    public void UpdateAnimatorItemGrabbed(bool isGrabbed)
    {
        anim.SetBool(_itemGrabbed, isGrabbed);
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

    public void SetAnimatorOverrideController(AnimatorOverrideController overrideController)
    {
        anim.runtimeAnimatorController = overrideController;
    }



    // TEST ////////////////////////////////////////////////////////////
    //bool grabbed = false;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        grabbed = !grabbed;
    //        UpdateAnimatorItemGrabbed(grabbed);
    //    }
    //}
}
