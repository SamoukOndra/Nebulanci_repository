using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatisticsCanvas : MonoBehaviour
{
    [SerializeField] List<Sprite> medals;
    [Space]

    [SerializeField] Image medal;
    [Space]

    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI kills;
    [SerializeField] TextMeshProUGUI deaths;
    [SerializeField] TextMeshProUGUI score;

    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void FillStatisticsCanvas(PlayerStatistics playerStatistics)
    {
        playerName.text = playerStatistics.playerName;
        kills.text = "kills: " + playerStatistics.kills;
        deaths.text = "deaths: " + playerStatistics.deaths;
        score.text = "score: " + playerStatistics.finalScore;

        int rank = playerStatistics.rank;
        
        if(rank <= medals.Count)
        {
            medal.sprite = medals[rank - 1];
        }

        canvas.enabled = true;
    }
}
