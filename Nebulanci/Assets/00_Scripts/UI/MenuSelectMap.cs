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
        FillInCharacters();

        if(selectedScene != null)
            SceneManager.LoadScene(selectedScene);
    }

    private void FillInCharacters()
    {
        foreach(PlayerBlueprint pb in SetUp.playerBlueprints)
        {
            if(pb.characterIndex == -1)
            {
                pb.characterIndex = GetFreeCharacter();
            }
        }
    }

    private int GetFreeCharacter()
    {       
        for(int i = 0; i < MenuSelectCharacter.placeholderScripts.Count; i++)
        {
            if (!MenuSelectCharacter.placeholderScripts[i].GetIsBlocked())
            {
                MenuSelectCharacter.placeholderScripts[i].Block(true);
                return MenuSelectCharacter.placeholderScripts[i].characterIndex;
            }
        }

        return 0; // todle nekde spatne
    }
}
