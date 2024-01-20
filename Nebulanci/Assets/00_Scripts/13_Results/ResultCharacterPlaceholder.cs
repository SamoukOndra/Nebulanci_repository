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

    public void ActivateCharacter(bool activate)
    {
        character.SetActive(activate);
    }

    public void SetAnimatorControler()
    {
        if (characterController == null) return;

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = characterController;

        //_isPointed = Animator.StringToHash("IsPointed");
        //_isSelected = Animator.StringToHash("IsSelected");
    }

    public int GetRank()
    {
        return playerStatistics.rank;
    }
}
