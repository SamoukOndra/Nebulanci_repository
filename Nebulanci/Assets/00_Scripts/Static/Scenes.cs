using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scenes
{
    public static readonly string startMenu = "level_menu";
    public static readonly string endMenu = "level_end";
    public static readonly string backyard = "level_01";

    public static readonly string[] allScenes = { startMenu, endMenu, backyard };


    public static string GetScene(int sceneIndex)
    {
        if (sceneIndex >= allScenes.Length || sceneIndex < 0)
        {
            Debug.Log("ERROR: " + sceneIndex + " is invalid scene index !!!!");
            return startMenu;
        }

        return allScenes[sceneIndex];
    }
}
