using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetUp
{
    public static readonly int maxPlayers = 4;

    public static int playersAmount;
    public static PlayerBlueprint[] playerBlueprints = new PlayerBlueprint[maxPlayers];

    public static void EraseAllBlueprints()
    {
        foreach(PlayerBlueprint pb in playerBlueprints)
        {
            if(pb != null)
            {
                pb.menuCharacterPlaceholderScript.Block(false);
                pb.menuCharacterPlaceholderScript.SetIsSelected(false);
                pb.menuCharacterPlaceholderScript.SetIsPointed(false);
            }
        }

        System.Array.Clear(playerBlueprints, 0, playerBlueprints.Length);
    }
    //game mode
}
