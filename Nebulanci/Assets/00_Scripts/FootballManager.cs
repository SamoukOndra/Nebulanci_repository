using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballManager : MonoBehaviour
{
    public static FootballManager singleton;

    private List<TeamAffiliation> teamRed = new();
    private List<TeamAffiliation> teamBlue = new();

    private bool nextIsRed = true;

    [SerializeField] Color redTeamColor;
    [SerializeField] Color blueTeamColor;

    public int goalScoreValue = 10;

    public delegate void TeamScore(List<TeamAffiliation> team);
    public event TeamScore OnTeamScore;


    private void Awake()
    {
        singleton = this;
    }

    public void AddTeamAffiliation(TeamAffiliation ta)
    {
        if (nextIsRed)
        {
            teamRed.Add(ta);
            ta.SetTeamColor(redTeamColor);
        }

        else
        {
            teamBlue.Add(ta);
            ta.SetTeamColor(blueTeamColor);
        }

        nextIsRed = !nextIsRed;
    }

    public void InvokeOnTeamScore(bool redTeamScored)
    {
        List<TeamAffiliation> scoringTeam = new();

        if (redTeamScored) scoringTeam = teamRed;
        else scoringTeam = teamBlue;

        OnTeamScore?.Invoke(scoringTeam);
    }
}
