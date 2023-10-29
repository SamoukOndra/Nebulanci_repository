using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        while (!floorHit)
        {
            if(Physics.Raycast(new Vector3(Random.Range(minX, maxX), height, Random.Range(minZ, maxZ)), Vector3.down, out hit, height + 1))
            {
                if (currentFloor.int_floorLayerMask == hit.transform.gameObject.layer)
                    floorHit = true;
            }
            else
            {
                Debug.Log("ERROR: Invalid spawn area !!!!!!!");
                return Vector3.down;
            }
        }

        Vector3 spawnPos = hit.point;

        return spawnPos;
    }

    public static bool MouseHit(float rayLength, LayerMask layerMask, out Collider hitCollider)
    {
        bool hitSomething = false;

       //Vector3 screenPosition = Mouse.current.position.ReadValue();
       //screenPosition.z = Camera.main.nearClipPlane;
        //Debug.Log(screenPosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
        {
            hitCollider = hit.collider;
            hitSomething = true;
        }
        else hitCollider = null;
    
        return hitSomething;
    }
}
