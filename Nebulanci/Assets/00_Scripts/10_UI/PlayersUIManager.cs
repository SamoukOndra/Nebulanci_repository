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
        EventManager.OnGatherFinalScores += SendFinalScores;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerAdded -= InitializePlayerUI;
        EventManager.OnGatherFinalScores -= SendFinalScores;
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

    
    public void SendFinalScores()
    {
        foreach(PlayerUIHandler uiHandler in playerUIHandlers)
        {
            if (!uiHandler.gameObject.activeInHierarchy) return;

            GameObject _player = uiHandler.player;
            int _score = uiHandler.GetScore();

            EventManager.InvokeOnFinalScore(_player, _score);
        }
    }
}
