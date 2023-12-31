using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Floor currentFloor;

    public static AnimatorHandler GetAnimatorHandlerInChildren(GameObject parent)
    {
        AnimatorHandler animatorHandler = parent.GetComponentInChildren<AnimatorHandler>();
        return animatorHandler;
    }

    public static Vector3 GetRandomSpawnPosition()
    {
        float height = currentFloor.spawnRaycastHeight;

        float minX = currentFloor.minX;
        float maxX = currentFloor.maxX;
        float minZ = currentFloor.minZ;
        float maxZ = currentFloor.maxZ;

        bool floorHit = false;
        RaycastHit hit = new();

        while(!floorHit)
        {
            floorHit = Physics.Raycast(new Vector3(Random.Range(minX, maxX), height, Random.Range(minZ, maxZ)), Vector3.down, out hit, height + 1, currentFloor.floorLayerMask);
        }

        Vector3 spawnPos = hit.point;

        return spawnPos;
    }
}
