using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterPlaceholder : MonoBehaviour
{
    [HideInInspector]
    public GameObject character;

    //[SerializeField] RuntimeAnimatorController menuCharacterController;
    Animator animator;

    int _isPointed;
    int _isSelected;

    bool isSelected;
    bool isBlocked;

    public void SetAnimatorController(RuntimeAnimatorController animatorController)
    {
        animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;

        _isPointed = Animator.StringToHash("IsPointed");
        _isSelected = Animator.StringToHash("IsSelected");
    }

    public void SetIsPointed(bool isPointed)
    {
        animator.SetBool(_isPointed, isPointed);
    }

    public void SetIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        animator.SetBool(_isSelected, isSelected);
    }


    public void SetIsSelected(bool isSelected, out MenuCharacterPlaceholder selectedCharacterScript)
    {
        if (isBlocked)
        {
            selectedCharacterScript = null;
            return;
        }

        this.isSelected = isSelected;
        animator.SetBool(_isSelected, isSelected);
 
        if (isSelected)
            selectedCharacterScript = this;
        else selectedCharacterScript = null;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void Block(bool isBlocked)
    {
        this.isBlocked = isBlocked;
    }

}
