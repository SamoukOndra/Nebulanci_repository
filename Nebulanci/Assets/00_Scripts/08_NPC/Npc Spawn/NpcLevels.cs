using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLevels : MonoBehaviour
{
    NpcPool pool;
    protected float repeatRate;

    protected virtual void Start()
    {
        pool = NpcPool.singl;
        //InvokeRepeating("SpawnNpc", repeatRate, repeatRate);
    }

    protected void SpawnNpc()
    {
        GameObject npc = pool.GetPooledNpc();

        if (npc == null) return;

        npc.transform.position = Util.GetRandomSpawnPosition();
        npc.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
        npc.SetActive(true);
    }
}
