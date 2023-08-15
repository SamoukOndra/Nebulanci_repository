using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    public List<Vector3> playerSpawnPositions;

    List<GameObject> players;
    int playersCount = 0;
    
    PlayerControls inputActions;
    //InputControlScheme[] controlSchemes;
    

    private void Awake()
    {
        if (inputActions == null)
            inputActions = new PlayerControls();

        //controlSchemes = inputActions.controlSchemes;
    }

    private void Start()
    {
        //test
        AddPlayer();
        AddPlayer();
    }
    public GameObject AddPlayer()
    {
        Vector3 spawnPosition = SetPlayerSpawnPosition();
        GameObject newPlayer = Instantiate(player, spawnPosition, Quaternion.identity);
        
        SetPlayerControlsActionMap(newPlayer);
        
        //players.Add(newPlayer);
        

        playersCount++;

        return newPlayer;
    }

    private Vector3 SetPlayerSpawnPosition()
    {
        if (playerSpawnPositions[playersCount] == null)
            playerSpawnPositions[playersCount] = Vector3.zero;

        return playerSpawnPositions[playersCount];
    }

    private void SetPlayerControlsActionMap(GameObject player)
    {
        //PlayerInput playerInput = player.GetComponent<PlayerInput>();
        //playerInput.SwitchCurrentControlScheme(SetInputControlScheme());
        //playerInput.actions.AddControlScheme(SetInputControlScheme());
        /*InputControlScheme inputControlScheme = SetInputControlScheme();
        playerInput.defaultControlScheme = inputControlScheme.ToString();
        if (playerInput.currentControlScheme == null)
        {

        }*/
        inputActions.bindingMask = new InputBinding { groups = SetInputControlScheme().ToString() };


        /*InputActionMap inputActionMap;
        switch (playersCount)
        {
            case 0: inputActionMap = inputActions.Player_1; Debug.Log("case0"); break;
            case 1: inputActionMap = inputActions.Player_2; Debug.Log("case1"); break;
            default: inputActionMap = inputActions.Player_1; Debug.Log("default case"); break;
        }
        playerInput.currentActionMap = inputActionMap;*/
    }

    private InputControlScheme SetInputControlScheme()
    {
        InputControlScheme inputControlScheme;
        switch (playersCount)
        {
            case 0: inputControlScheme = inputActions.Player_1Scheme; Debug.Log("case0"); break;
            case 1: inputControlScheme = inputActions.Player_2Scheme; Debug.Log("case1"); break;
            default: inputControlScheme = inputActions.Player_1Scheme; Debug.Log("default case"); break;
        }
        return inputControlScheme;
    }

}
