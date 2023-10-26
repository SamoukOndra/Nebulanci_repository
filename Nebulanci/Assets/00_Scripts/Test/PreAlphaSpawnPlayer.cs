using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreAlphaSpawnPlayer : MonoBehaviour
{
    public int playerCount = 0;
    public int maxPlayers = 3;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> models;
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && maxPlayers > playerCount)//////////////////////
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            
            PlayerBlueprint playerBlueprint = new();
            playerBlueprint.controlScheme = controlSchemes[playerCount];
            playerBlueprint.model = models[playerCount];
            playerBlueprint.name = "Player " + (playerCount + 1);

            EventManager.InvokeOnPlayerAdded(newPlayer, playerBlueprint);
            
            playerCount++;
        }
    }
}
