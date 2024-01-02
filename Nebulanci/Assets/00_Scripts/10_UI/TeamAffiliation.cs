using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamAffiliation : MonoBehaviour
{
    Image teamColorImage;
    PlayerUIHandler playerUIHandler;

    private void OnEnable()
    {
        teamColorImage = GetComponent<Image>();
        playerUIHandler = GetComponentInParent<PlayerUIHandler>();
        FootballManager.singleton.AddTeamAffiliation(this);
        FootballManager.singleton.OnTeamScore += TeamScore;
    }

    private void OnDisable()
    {
        FootballManager.singleton.OnTeamScore -= TeamScore;
    }

    public void SetTeamColor(Color color)
    {
        teamColorImage.color = color;
        Debug.Log("Team color set to: " + color);
    }

    public void TeamScore(List<TeamAffiliation> team)
    {
        if (team.Contains(this))
        {
            playerUIHandler.UpdateScore(FootballManager.singleton.goalScoreValue);
        }
    }

    
}
