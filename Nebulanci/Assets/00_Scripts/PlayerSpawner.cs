using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] List<GameObject> playerModels;

    //tyhle nahradi docasny methods
    string selectedControlScheme;
    GameObject selectedPlayerModel;

    [SerializeField] int maximumOfPlayers = 2;
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
        AddPlayerModel(newPlayer);
        InitializePlayerMovement(newPlayer);
        InitializeCombatHandler(newPlayer);
        

        if(playersCount < maximumOfPlayers - 1)/////////////////
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

    private void AddPlayerModel(GameObject player)
    {
        GameObject playerModel;////////////////////////////////
        playerModel = playerModels[playersCount];
        Instantiate(playerModel, player.transform, false);
    }

    private void InitializeCombatHandler(GameObject player)
    {
        CombatHandler combatHandler = player.GetComponent<CombatHandler>();
        combatHandler.Initialize();
    }

    private void InitializePlayerMovement(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.Initialize();
    }
}
