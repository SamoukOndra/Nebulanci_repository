using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class EscToBackButton : MonoBehaviour
{  
    [SerializeField] Button backButton;
    PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme("Menu", Keyboard.current);
    }
    public void OnPauseMenu(InputValue value)
    {
        if (value.isPressed) {
            ForceClick();
            Debug.Log("ForceClick call");
        }
        
    }

    private void ForceClick()
    {
        backButton.onClick.Invoke();
    }
}