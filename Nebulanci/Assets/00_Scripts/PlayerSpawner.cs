using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    int selectedPlayer = 0;
    int playersCount = 0;

    private void OnEnable()
    {
        EventManager_UI.OnPlayerAdded += AddPlayer;
    }

    private void OnDisable()
    {
        EventManager_UI.OnPlayerAdded -= AddPlayer;
    }

    public void AddPlayer()
    {
        Vector3 spawnPosition = SetPlayerSpawnPosition();
        GameObject newPlayer = Instantiate(player, spawnPosition, Quaternion.identity);
        SetInputControlScheme(newPlayer);

        playersCount++;
    }

    private Vector3 SetPlayerSpawnPosition()
    {
        /////////////////////////////////////////////////////////////////////////
        return Vector3.zero;
    }

    private void SetInputControlScheme(GameObject player)
    {
        string controlScheme;/////////////////////
        switch (playersCount)
        {
            case 0: controlScheme = "Player_1"; Debug.Log("case0"); break;
            case 1: controlScheme = "Player_2"; Debug.Log("case1"); break;
            default: controlScheme = "Player_1"; Debug.Log("default case"); break;
        }

        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(controlScheme, Keyboard.current);

    }
}
