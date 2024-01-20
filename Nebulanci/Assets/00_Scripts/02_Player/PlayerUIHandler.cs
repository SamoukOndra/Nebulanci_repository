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
    [SerializeField] Image armorBarFilling;
    [SerializeField] Image ammoBarFilling;
    [SerializeField] Image reloadFilling;


    int score;

    private void OnEnable()
    {
        score = 0;

        //tmpScore.text = score.ToString();
        UpdateScore(0);
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


    public void UpdateScore(int addValue)
    {
        score += addValue;
        tmpScore.text = score.ToString();

        //EventManager.InvokeOnScoreUpdated(player, score);
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

    public void UpdateArmor(float fraction)
    {
        armorBarFilling.fillAmount = fraction;
    }

    public void UpdateAmmo(float maxAmmo, float currentAmmo)
    {
        //Debug.Log("maxA: " + maxAmmo + "; curA: " + currentAmmo + "; fill: " + (currentAmmo/maxAmmo));

        if (currentAmmo <= 0)
            ammoBarFilling.fillAmount = 0;

        else
            ammoBarFilling.fillAmount = currentAmmo / maxAmmo;
    }

    public void UpdateReload(float fraction)
    {
        if (fraction >= 1)
            fraction = 0;

        reloadFilling.fillAmount = fraction;
    }

    public int GetScore()
    {
        return score;
    }
}
