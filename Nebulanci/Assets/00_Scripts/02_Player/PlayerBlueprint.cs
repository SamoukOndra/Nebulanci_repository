using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlueprint// : MonoBehaviour
{
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3", "Player_s", "Gamepad" };

    public MenuCharacterPlaceholder menuCharacterPlaceholderScript;

    //public GameObject character;
    public int characterIndex = -1;
    public int controlsIndex;
    public string name = "Player";

    public string GetControlScheme()
    {
        string scheme = controlSchemes[controlsIndex];
        return scheme;
    }
}
