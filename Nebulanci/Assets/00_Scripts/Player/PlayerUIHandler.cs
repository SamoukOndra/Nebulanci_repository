using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;

    [SerializeField] TextMeshProUGUI tmpName;
    [SerializeField] TextMeshProUGUI tmpScore;
    [SerializeField] Image healthBarFilling;

    int score;

    private void OnEnable()
    {
        score = 0;

        tmpScore.text = score.ToString();
        UpdateHealth(1, 1);


        EventManager.OnPlayerDeath += PlayerDied;
        EventManager.OnPlayerKill += PlayerKilled;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= PlayerDied;
        EventManager.OnPlayerKill -= PlayerKilled;
    }

    public void PlayerDied(GameObject deadPlayer)
    {
        if (deadPlayer == player)
            UpdateScore(-1);
    }

    public void PlayerKilled(GameObject killByPlayer)
    {
        if (killByPlayer == player)
            UpdateScore(+1);
    }


    private void UpdateScore(int addValue)
    {
        score += addValue;
        tmpScore.text = score.ToString();
    }

    public void SetPlayerName(string playerName)
    {
        tmpName.text = playerName;
    }

    public void UpdateHealth(float maxHealth, float currentHealth)
    {
        if (currentHealth <= 0)
            healthBarFilling.fillAmount = 0;

        else
            healthBarFilling.fillAmount = currentHealth / maxHealth;
    }
}
