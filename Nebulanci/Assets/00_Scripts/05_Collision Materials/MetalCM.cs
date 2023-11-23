using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCM : CollisionMaterials
{
    const int id = 2;

    public override void Interact(Vector3 hitPoint, Quaternion rotation)
    {
        Util.SimpleCM_Interact(hitPoint, rotation, id);
    }
}