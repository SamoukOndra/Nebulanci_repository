using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    public delegate void PlayerAdded(GameObject newPlayer, PlayerBlueprint playerBlueprint);
    public static event PlayerAdded OnPlayerAdded;

    public delegate void PlayerDeath(GameObject deathPlayer);
    public static event PlayerDeath OnPlayerDeath;

    public delegate void PlayerKill(GameObject killedByPlayer);
    //public static event PlayerDeath OnPlayerKill;
    public static event PlayerKill OnPlayerKill;

    public delegate void Explosion(Vector3 position);
    public static event Explosion OnExplosion;




    public static void InvokeOnPlayerAdded(GameObject newPlayer, PlayerBlueprint playerBlueprint)
    {
        OnPlayerAdded?.Invoke(newPlayer, playerBlueprint);
    }

    public static void InvokeOnPlayerDeath(GameObject deathPlayer)
    {
        OnPlayerDeath?.Invoke(deathPlayer);
    }

    public static void InvokeOnPlayerKill(GameObject killedByPlayer)
    {
        OnPlayerKill?.Invoke(killedByPlayer);
    }

    public static void InvokeOnExplosion(Vector3 position)
    {
        OnExplosion?.Invoke(position);
    }
}
