using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private int playerID;

    private void Start()
    {
        playerID = GetComponent<PlayerID>().PlayerIDnumber;
    }

    protected override void WhenZeroHealth()
    {
        EventManager.PlayerDied(playerID);

        gameObject.SetActive(false);
    }
}
