using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_redCM : CollisionMaterials
{

    // inheritne nesmysle...

    const int id = 0;

    public override void Interact(Vector3 hitPoint, Quaternion rotation)
    {
        Util.SimpleCM_Interact(hitPoint, rotation, id);
    }
}
