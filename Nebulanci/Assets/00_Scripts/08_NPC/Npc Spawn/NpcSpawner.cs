using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    //NpcLevels lvl;

    private void Start()
    {
        int npcLevel = SetUp.npcLevel;

        switch (npcLevel)
        {
            case 1:
                gameObject.AddComponent<NpcLevel1>();
                //NpcLevel1 lvl1 = new();
                //lvl = lvl1;
                break;

            case 2:
                gameObject.AddComponent<NpcLevel2>();
                //NpcLevel2 lvl2 = new();
                //lvl = lvl2;
                break;

            case 3:
                gameObject.AddComponent<NpcLevel3>();
                //NpcLevel3 lvl3 = new();
                //lvl = lvl3;
                break;

            default:
                Destroy(this);
                break;
        }

        //gameObject.AddComponent(lvl);
    }
}
