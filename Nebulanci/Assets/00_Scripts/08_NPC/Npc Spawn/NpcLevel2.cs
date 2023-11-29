using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLevel2 : NpcLevels
{
    protected override void Start()
    {
        base.Start();
        repeatRate = 5;
        InvokeRepeating("SpawnNpc", repeatRate, repeatRate);
    }
}
