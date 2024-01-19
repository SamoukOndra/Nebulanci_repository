using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCM : CollisionMaterials
{
    [SerializeField] float pushForce = 100f;

    Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    public override bool Interact(Vector3 hitPoint, Quaternion rotation, GameObject shootingPlayer = null)
    {
        Vector3 direction = (rotation * Vector3.forward).normalized;
        rb.AddForceAtPosition(direction * pushForce, hitPoint);
        return isBulletProof;
    }
}
