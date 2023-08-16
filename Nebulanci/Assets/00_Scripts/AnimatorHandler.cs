using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{

    //misto awake ma SG spesl fci, snad se to nekupi nebo co

    Animator anim;

    int move;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        move = Animator.StringToHash("Move");
    }

    public void UpdateAnimatorMove(Vector3 moveDirection)
    {
        //Debug.Log("UpdateAnimMove");
        if (moveDirection == Vector3.zero) { anim.SetFloat(move, 0/*, 0.1f, Time.deltaTime*/); }
        else { anim.SetFloat(move, 1/*, 0.1f, Time.deltaTime*/); }
    }
}
