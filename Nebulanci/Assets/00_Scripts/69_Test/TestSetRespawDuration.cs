using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetRespawDuration : MonoBehaviour
{
    public float respDelay = 6f;

    private void Start()
    {
        PlayerSpawner.respawnPlayerWaitTime = respDelay;
    }
}
