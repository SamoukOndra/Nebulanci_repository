using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetUp
{
    public static readonly int maxPlayers = 3;

    public static int playersAmount;
    //public static PlayerBlueprint[] playerBlueprints = new PlayerBlueprint[maxPlayers];
    public static List<PlayerBlueprint> playerBlueprints = new(maxPlayers);

    public static void EraseAllBlueprints()
    {
        //if (Util.IsEmpty(ref playerBlueprints)) return;

        foreach(PlayerBlueprint pb in playerBlueprints)
        {
            if(pb.menuCharacterPlaceholderScript != null)
            {
                pb.menuCharacterPlaceholderScript.Block(false);
                pb.menuCharacterPlaceholderScript.SetIsSelected(false);
                pb.menuCharacterPlaceholderScript.SetIsPointed(false);
            }
        }

        playerBlueprints = new(maxPlayers);
        

        //Util.CleanArray(ref playerBlueprints);
        //playerBlueprints = new PlayerBlueprint[maxPlayers];
        //System.Array.Clear(playerBlueprints, 0, playerBlueprints.Length);
    }
    //game mode
}
