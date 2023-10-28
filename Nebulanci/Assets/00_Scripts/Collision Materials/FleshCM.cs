using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshCM : CollisionMaterials
{
    [SerializeField] new ParticleSystem particleSystem;

    public override void Interact(Vector3 hitPoint, Quaternion rotation)
    {
        particleSystem.transform.position = hitPoint;
        particleSystem.transform.rotation = rotation;
        particleSystem.Play();
    }
}
