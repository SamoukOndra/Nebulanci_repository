using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCM : CollisionMaterials
{

    // inheritne nesmysle...

    const int id = 0;

    public override void Interact(Vector3 hitPoint, Quaternion rotation)
    {
        GameObject cmHit = CmHitPool.singl.GetPooledHit(id);

        cmHit.transform.SetPositionAndRotation(hitPoint, rotation);
        cmHit.SetActive(true);
    }
}
