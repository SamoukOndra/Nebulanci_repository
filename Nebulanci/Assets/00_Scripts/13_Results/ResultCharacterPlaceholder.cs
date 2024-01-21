using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCharacterPlaceholder : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController characterController;
    private GameObject character;

    private PlayerStatistics playerStatistics;
    private StatisticsCanvas statisticsCanvas;

    Animator animator;

    int _rank;

    private void Awake()
    {
        statisticsCanvas = GetComponentInChildren<StatisticsCanvas>();
    }

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

        animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = characterController;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        _rank = Animator.StringToHash("Rank");

        //_isPointed = Animator.StringToHash("IsPointed");
        //_isSelected = Animator.StringToHash("IsSelected");
    }

    public int GetRank()
    {
        return playerStatistics.rank;
    }

    public void FillStatisticsUI()
    {
        statisticsCanvas.FillStatisticsCanvas(playerStatistics);
    }

    public void PlayAnimation()
    {
        animator.SetInteger(_rank, GetRank());
    }
}
