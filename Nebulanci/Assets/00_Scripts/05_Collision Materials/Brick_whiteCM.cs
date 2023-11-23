using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_whiteCM : CollisionMaterials
{
    const int id = 1;

    public override void Interact(Vector3 hitPoint, Quaternion rotation)
    {
        Util.SimpleCM_Interact(hitPoint, rotation, id);
    }
}
