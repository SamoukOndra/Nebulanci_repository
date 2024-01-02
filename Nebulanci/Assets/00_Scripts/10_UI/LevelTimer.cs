using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelTimer;
    [SerializeField] PauseGame tempEndGameSolution;
    bool tempEndFlag = false;

    float countdown;

    private void Start()
    {
        countdown = SetUp.levelTimer;
    }

    private void Update()
    {
        UpdateTimer();

        if(countdown <= 0 && !tempEndFlag)
        {
            TempEndGame();
        }
    }

    private void UpdateTimer()
    {
        countdown -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60);

        levelTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TempEndGame()
    {
        tempEndFlag = true;
        tempEndGameSolution.OnPauseMenu();
    }
}
