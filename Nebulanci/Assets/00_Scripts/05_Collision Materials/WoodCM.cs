using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCM : CollisionMaterials
{
    const int id = 3;

    public override void Interact(Vector3 hitPoint, Quaternion rotation, GameObject _null)
    {
        Util.SimpleCM_Interact(hitPoint, rotation, id);
    }
}
