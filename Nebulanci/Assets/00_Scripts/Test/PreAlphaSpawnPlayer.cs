using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreAlphaSpawnPlayer : MonoBehaviour
{
    public int playerCount = 0;
    public int maxPlayers = 3;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject characterList;
    private List<GameObject> models;
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };

    private void Start()
    {
        models = characterList.GetComponent<CharacterList>().characters;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && maxPlayers > playerCount)//////////////////////
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            
            PlayerBlueprint playerBlueprint = new();
            
            //playerBlueprint.controlScheme = controlSchemes[playerCount];
            playerBlueprint.controlsIndex = playerCount;

            playerBlueprint.characterIndex = playerCount;
            playerBlueprint.name = "Player " + (playerCount + 1);

            EventManager.InvokeOnPlayerAdded(newPlayer, playerBlueprint);
            
            playerCount++;
        }
    }

    private void SpawnPlayers()
    {
        for(int i = 0; i < SetUp.playersAmount; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);

            PlayerBlueprint playerBlueprint = SetUp.playerBlueprints[i];

            EventManager.InvokeOnPlayerAdded(newPlayer, playerBlueprint);
        }
    }
}
