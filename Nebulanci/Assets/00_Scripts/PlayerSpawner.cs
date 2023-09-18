using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner playerSpawnerSingleton;

    //[SerializeField] GameObject player;

    [SerializeField] List<GameObject> playerModels;

    //tyhle nahradi docasny methods
    string selectedControlScheme;
    GameObject selectedPlayerModel;

    [SerializeField] int maximumOfPlayers = 3;
    int playersCount = 0;

    public static List<GameObject> players = new();
    private List<string> activeControlSchemes = new();
    
    public static float respawnPlayerWaitTime = 2f;


    private void Awake()
    {
        playerSpawnerSingleton = this;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerAdded += AddPlayer;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= AddPlayer;
    }

    public void AddPlayer(GameObject newPlayer)
    {
        //Vector3 spawnPosition = Util.GetRandomSpawnPosition();
        //GameObject newPlayer = Instantiate(player, spawnPosition, Quaternion.identity);
        newPlayer.transform.position = Util.GetRandomSpawnPosition();
        
        AddPlayerModel(newPlayer);
        InitializePlayerMovement(newPlayer);
        InitializeCombatHandler(newPlayer);

        int _playerID = playersCount; //tohle vypada provizorne, zatim slouzi k setnuti controlScheme
        
        //newPlayer.GetComponent<PlayerID>().SetPlayerID(_playerID);
        players.Add(newPlayer);

        string _controlScheme = DecideControlScheme(newPlayer);///////
        activeControlSchemes.Add(_controlScheme);

        SetControlScheme(_playerID, _controlScheme);

        InitializePlayerDeath(newPlayer);

        if (playersCount < maximumOfPlayers - 1)/////////////////
            playersCount++;
    }

    private string DecideControlScheme(GameObject player)///////
    {
        string controlScheme;

        switch (playersCount)
        {
            case 0: controlScheme = "Player_1"; Debug.Log("case0"); break;
            case 1: controlScheme = "Player_2"; Debug.Log("case1"); break;
            case 2: controlScheme = "Player_3"; Debug.Log("case2"); break;
            default: controlScheme = "Player_1"; Debug.Log("default case"); break;
        }

        return controlScheme;
    }

    private void SetControlScheme(int playerID, string controlScheme)
    {
        PlayerInput playerInput = players[playerID].GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(controlScheme, Keyboard.current); //rozsirit o gamepad, tez je volana v PlayerDeath
    }

    private void SetControlScheme(int playerID)
    {
        PlayerInput playerInput = players[playerID].GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(activeControlSchemes[playerID], Keyboard.current);
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

    private void InitializePlayerDeath(GameObject player)
    {
        PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
        playerDeath.Initialize();
    }
}
