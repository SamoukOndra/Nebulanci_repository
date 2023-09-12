using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    public delegate void PlayerAdded(GameObject newPlayer);
    public static event PlayerAdded OnPlayerAdded;

    public delegate void PlayerDeath(GameObject deathPlayer);
    public static event PlayerDeath OnPlayerDeath;

    public delegate void PlayerKill(GameObject killedByPlayer);
    public static event PlayerDeath OnPlayerKill;


    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))//////////////////////
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            OnPlayerAdded?.Invoke(newPlayer);
        }
    }

    public static void InvokeOnPlayerDeath(GameObject deathPlayer)
    {
        OnPlayerDeath?.Invoke(deathPlayer);
    }

    public static void InvokeOnPlayerKill(GameObject killedByPlayer)
    {
        OnPlayerKill?.Invoke(killedByPlayer);
    }
}
