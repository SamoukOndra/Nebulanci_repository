using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreAlphaMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme("Menu", Keyboard.current);
    }

    public void OnOpenCloseMenu()
    {
        Debug.Log("666666666666666666666666666666666666666666666666666");
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void OnPauseMenu(InputValue value)
    {
        Debug.Log(value);
        menu.SetActive(!menu.activeInHierarchy);
    }
}
