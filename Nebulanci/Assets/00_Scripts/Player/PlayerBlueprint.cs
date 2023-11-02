using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlueprint// : MonoBehaviour
{
    private readonly string[] controlSchemes = { "Player_1", "Player_2", "Player_3" };

    public MenuCharacterPlaceholder menuCharacterPlaceholderScript;

    public GameObject character;
    //public string controlScheme;
    public int controlsIndex;
    public string name = "Player";

    public string GetControlScheme()
    {
        string scheme = controlSchemes[controlsIndex];
        return scheme;
    }
}
