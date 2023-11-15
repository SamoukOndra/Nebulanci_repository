using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionMaterials : MonoBehaviour
{
    public abstract void Interact(Vector3 hitPoint, Quaternion rotation);
}
