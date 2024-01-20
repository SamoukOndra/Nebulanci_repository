using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCharacterPlaceholder : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController characterController;
    private GameObject character;

    private PlayerStatistics playerStatistics;

    Animator animator;

    public void AddPlayerStatistics(PlayerStatistics playerStatistics)
    {
        this.playerStatistics = playerStatistics;
    }

    public void AddCharacter(GameObject character)
    {
        this.character = character;
    }

    public void SetAnimatorControler()
    {
        animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = characterController;

        //_isPointed = Animator.StringToHash("IsPointed");
        //_isSelected = Animator.StringToHash("IsSelected");
    }
}
