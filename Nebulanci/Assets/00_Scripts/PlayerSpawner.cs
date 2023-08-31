using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner playerSpawnerSingleton;

    [SerializeField] GameObject player;

    [SerializeField] List<GameObject> playerModels;

    //tyhle nahradi docasny methods
    string selectedControlScheme;
    GameObject selectedPlayerModel;

    [SerializeField] int maximumOfPlayers = 2;
    int playersCount = 0;

    public static List<GameObject> players = new();
    private List<string> activeControlSchemes = new();
    
    [SerializeField] float respawnPlayerWaitTime = 2f;


    private void Awake()
    {
        playerSpawnerSingleton = this;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerAdded += AddPlayer;
        EventManager.OnPlayerDeath += RespawnPlayer;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= AddPlayer;
        EventManager.OnPlayerDeath -= RespawnPlayer;
    }

    public void AddPlayer()
    {
        Vector3 spawnPosition = Util.GetRandomSpawnPosition();
        GameObject newPlayer = Instantiate(player, spawnPosition, Quaternion.identity);
        
        AddPlayerModel(newPlayer);
        InitializePlayerMovement(newPlayer);
        InitializeCombatHandler(newPlayer);

        int _playerID = playersCount;
        
        newPlayer.GetComponent<PlayerID>().SetPlayerID(_playerID);
        players.Add(newPlayer);

        string _controlScheme = DecideControlScheme(newPlayer);///////
        activeControlSchemes.Add(_controlScheme);

        SetControlScheme(_playerID, _controlScheme);

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
            default: controlScheme = "Player_1"; Debug.Log("default case"); break;
        }

        return controlScheme;
    }

    private void SetControlScheme(int playerID, string controlScheme)
    {
        PlayerInput playerInput = players[playerID].GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(controlScheme, Keyboard.current);
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

    public void RespawnPlayer(int playerID)
    {
        StartCoroutine(RespawnPlayerCoroutine(playerID));
    }

    IEnumerator RespawnPlayerCoroutine(int playerID)
    {
        player = players[playerID];

        yield return new WaitForSeconds(respawnPlayerWaitTime);
       
        player.SetActive(true);
        
        player.transform.position = Util.GetRandomSpawnPosition();
        
        SetControlScheme(playerID);
    }
}
