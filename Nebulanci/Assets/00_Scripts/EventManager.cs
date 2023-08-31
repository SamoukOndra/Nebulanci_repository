using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerAdded();
    public static event PlayerAdded OnPlayerAdded;

    public delegate void PlayerDeath(int playerID);
    public static event PlayerDeath OnPlayerDeath;


    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))//////////////////////
        {
            OnPlayerAdded?.Invoke();
        }
    }

    public static void PlayerDied(int playerID)
    {
        OnPlayerDeath?.Invoke(playerID);
    }
}
