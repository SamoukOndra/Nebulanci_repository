using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics
{
    private GameObject player;

    public string name;
    public int kills = 0;
    public int deaths = 0;
    public int finalScore = 0;

    public int rank;

    public PlayerStatistics(GameObject player, string name)
    {
        this.player = player;
        this.name = name;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += PlayerDied;
        EventManager.OnPlayerKill += PlayerKilled;
        EventManager.OnFinalScore += SetFinalScore;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= PlayerDied;
        EventManager.OnPlayerKill -= PlayerKilled;
        EventManager.OnFinalScore -= SetFinalScore;
    }

    private void PlayerKilled(GameObject killedByPlayer)
    {
        if(player == killedByPlayer)
        {
            kills++;
        }
    }

    private void PlayerDied(GameObject deathPlayer)
    {
        if(player == deathPlayer)
        {
            deaths++;
        }
    }

    private void SetFinalScore(GameObject player, int currentScore)
    {
        if(this.player == player)
        {
            finalScore = currentScore;
        }
    }

}
