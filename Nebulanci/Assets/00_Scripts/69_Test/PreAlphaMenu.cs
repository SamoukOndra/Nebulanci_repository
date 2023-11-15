using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreAlphaMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    public void OnOpenCloseMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);

    }
}
