using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelectMap : MonoBehaviour
{
    private readonly string[] scenes = { "level_menu", "level_01" };
    private string selectedScene;

    public void SelectScene(int sceneIndex)
    {
        if (sceneIndex >= scenes.Length) return;

        selectedScene = scenes[sceneIndex];
    }

    public void LoadSelectedScene()
    {
        if(selectedScene != null)
            SceneManager.LoadScene(selectedScene);
    }

}
