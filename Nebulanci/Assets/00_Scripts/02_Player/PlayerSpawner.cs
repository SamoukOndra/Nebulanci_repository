using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner playerSpawnerSingleton;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject characterList;
    
    private List<GameObject> availableCharacters;

    public static float respawnPlayerWaitTime = 3.5f;

    private List<Transform> playersTransforms = new();

    private void Awake()
    {
        playerSpawnerSingleton = this;
        availableCharacters = characterList.GetComponent<CharacterList>().characters;
    }

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < SetUp.playersAmount; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            playersTransforms.Add(newPlayer.transform);

            PlayerBlueprint playerBlueprint = SetUp.playerBlueprints[i];

            EventManager.InvokeOnPlayerAdded(newPlayer, playerBlueprint);
        }
    }

    private void OnEnable()
    {
        EventManager.OnPlayerAdded += AddPlayer;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= AddPlayer;
    }

    public void AddPlayer(GameObject newPlayer, PlayerBlueprint playerBlueprint)
    {
        newPlayer.transform.position = Util.GetRandomSpawnPosition();

        Instantiate(availableCharacters[playerBlueprint.characterIndex], newPlayer.transform, false);
        
        InitializePlayerMovement(newPlayer);
        InitializeCombatHandler(newPlayer);

        PlayerInput playerInput = newPlayer.GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(playerBlueprint.GetControlScheme(), Keyboard.current);

        InitializePlayerDeath(newPlayer);
    }

    public Vector3 GetClosestPlayerPos(Vector3 myPosition)
    {
        if (playersTransforms.Count == 1) return playersTransforms[0].position;

        Vector3 closestPos = Vector3.down;
        float distance = Mathf.Infinity;

        foreach (Transform t in playersTransforms)
        {
            float _distance = Vector3.Distance(myPosition, t.position);
            if(_distance < distance)
            {
                distance = _distance;
                closestPos = t.position;
            }
        }

        return closestPos;
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
