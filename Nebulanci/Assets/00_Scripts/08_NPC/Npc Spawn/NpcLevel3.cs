using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLevel3 : NpcLevels
{
    protected override void Start()
    {
        base.Start();
        repeatRate = 1.5f;
        InvokeRepeating("SpawnNpc", repeatRate, repeatRate);
    }
}
