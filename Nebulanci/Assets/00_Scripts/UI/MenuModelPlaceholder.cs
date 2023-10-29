using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModelPlaceholder : MonoBehaviour
{
    public GameObject character;

    [SerializeField] RuntimeAnimatorController menuCharacterController;
    Animator animator;

    private void Start()
    {
        animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = menuCharacterController;
    }
}
