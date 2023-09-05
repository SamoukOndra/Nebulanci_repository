using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeath : MonoBehaviour
{
    PlayerInput playerInput;
    string controlScheme;

    PlayerMovement playerMovement;

    CombatHandler combatHandler;

    Vector3 hidePosition = new(0, -10, 0);

    public void Initialize()
    {
        playerInput = GetComponent<PlayerInput>();
        controlScheme = playerInput.currentControlScheme;

        playerMovement = GetComponent<PlayerMovement>();

        combatHandler = GetComponent<CombatHandler>();

        EventManager.OnPlayerDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerDeath -= HandleDeath;
    }

    public void HandleDeath(GameObject player)
    {
        if(player == gameObject)
        {
            StartCoroutine(HandleDeathCoroutine(player));
        }
    }

    IEnumerator HandleDeathCoroutine(GameObject player)
    {
        player.transform.position = hidePosition;

        playerMovement.ResetMoveDirection();
               
        playerMovement.enabled = false;
        playerInput.enabled = false;
        
        yield return new WaitForSeconds(PlayerSpawner.respawnPlayerWaitTime);

        playerInput.enabled = true;
        playerInput.actions.Enable();
        playerInput.SwitchCurrentControlScheme(controlScheme, Keyboard.current);
        playerMovement.enabled = true;

        combatHandler.SelectWeapon(0);

        player.transform.position = Util.GetRandomSpawnPosition();
    }
}
