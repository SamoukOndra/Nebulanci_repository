using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelTimer;
    [SerializeField] float countdownStart = 5;

    private AudioSource audioSource;
    
    private AudioClip beepShort;
    private AudioClip beepLong;

    float countdown;

    bool finalCountdown = false;

    private void Start()
    {
        countdownStart++;

        countdown = SetUp.levelTimer + 1;

        audioSource = GetComponent<AudioSource>();

        beepShort = AudioManager.audioList.beepShort;
        beepLong = AudioManager.audioList.beepLong;
    }

    private void Update()
    {
        
        if(countdown <= 1)
        {
            EventManager.EndGame();

            audioSource.PlayOneShot(beepLong);

            levelTimer.text = " ";
            this.enabled = false;
            return;
        }

        if (countdown <= countdownStart && !finalCountdown)
        {
            InvokeRepeating("PlayBeep", 0, 1);
            levelTimer.color = Color.red;
            finalCountdown = true;
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        //if (countdown <= 0) return;

        countdown -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60);

        levelTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PlayBeep()
    {
        audioSource.PlayOneShot(beepShort);
    }
}
