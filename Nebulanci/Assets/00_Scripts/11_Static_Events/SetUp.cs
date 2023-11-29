using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetUp
{
    public static readonly int maxPlayers = 3;
    private const int maxNpcLevel = 3;

    public static int playersAmount;
    //public static PlayerBlueprint[] playerBlueprints = new PlayerBlueprint[maxPlayers];
    public static List<PlayerBlueprint> playerBlueprints = new(maxPlayers);

    public static Dictionary<int, int> pickUpDictionary;
    public static int meleeWeaponIndex;
    public static int defaultWeaponInex;

    public static int buffSpawnSpacing;

    public static int npcLevel;

    public static void DebugMsg()
    {
        Debug.Log("SetUp debug msg");
        //playerBlueprints = new(maxPlayers);
    }

    public static void EraseAllBlueprints()
    {
        //if (Util.IsEmpty(ref playerBlueprints)) return;
        if (playerBlueprints == null)
        {
            playerBlueprints = new(maxPlayers);
            return;
        }

        foreach (PlayerBlueprint pb in playerBlueprints)
        {
            if (pb.menuCharacterPlaceholderScript != null)
            {
                pb.menuCharacterPlaceholderScript.Block(false);
                pb.menuCharacterPlaceholderScript.SetIsSelected(false);
                pb.menuCharacterPlaceholderScript.SetIsPointed(false);
            }
        }

        playerBlueprints = new(maxPlayers);

        if (playerBlueprints.Count > 1) Debug.Log(playerBlueprints[1].GetControlScheme());
        //Util.CleanArray(ref playerBlueprints);
        //playerBlueprints = new PlayerBlueprint[maxPlayers];
        //System.Array.Clear(playerBlueprints, 0, playerBlueprints.Length);
    }
    //game mode
    public static void ResetPickUpDictionary()
    {
        pickUpDictionary = new();
    }

    public static void AddPickUpPair(int index, int quantity)
    {
        pickUpDictionary.Add(index, quantity);
    }

    public static int GetQuantityFromPickUpPair(int index)
    {
        if (pickUpDictionary != null && pickUpDictionary.ContainsKey(index))
        {
            int quantity = pickUpDictionary[index];
            return quantity;
        }

        else return 0;
    }

    public static void SetStartWeapons(int meleeIndex, int defaultIndex)
    {
        meleeWeaponIndex = (meleeIndex == defaultIndex ? -1 : meleeIndex); // pokud melee a def stejny, hodi meleeIndex -1, v tom pripade by mel bejt meleeGO null !!!
        defaultWeaponInex = defaultIndex;
        
    }

    //public static void SetSpawnSpacing(float sliderValue, float multiplier)
    //{
    //    spawnSpacing = sliderValue * multiplier;
    //}

    public static void SetNpcLevel(int npcLevel)
    {
        Mathf.Clamp(npcLevel, 0, maxNpcLevel);
        SetUp.npcLevel = npcLevel;
    }
}
