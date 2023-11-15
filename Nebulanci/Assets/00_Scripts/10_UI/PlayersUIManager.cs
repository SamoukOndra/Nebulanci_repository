using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersUIManager : MonoBehaviour
{
    //[SerializeField] GameObject UI_1;
    //[SerializeField] GameObject UI_2;
    //[SerializeField] GameObject UI_3;
    //[SerializeField] GameObject UI_4;

    [SerializeField] List<PlayerUIHandler> playerUIHandlers;

    private int playersInitializedCount = 0;


    private void OnEnable()
    {
        EventManager.OnPlayerAdded += InitializePlayerUI;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= InitializePlayerUI;
    }

    public void InitializePlayerUI(GameObject player, PlayerBlueprint playerBlueprint)
    {
        PlayerUIHandler _playerUIHandler = playerUIHandlers[playersInitializedCount];
        _playerUIHandler.gameObject.SetActive(true);

        _playerUIHandler.player = player;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.playerUIHandler = _playerUIHandler;

        CombatHandler combatHandler = player.GetComponent<CombatHandler>();
        combatHandler.playerUIHandler = _playerUIHandler;

        _playerUIHandler.SetPlayerName(playerBlueprint.name);


        playersInitializedCount++;
    }
}
