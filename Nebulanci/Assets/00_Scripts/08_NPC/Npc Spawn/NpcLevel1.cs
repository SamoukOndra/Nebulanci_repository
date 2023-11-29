using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLevel1 : NpcLevels
{
    protected override void Start()
    {
        base.Start();
        repeatRate = 10;
        InvokeRepeating("SpawnNpc", repeatRate, repeatRate);
    }
}
