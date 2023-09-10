using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Throw(float throwForce)
    {
        rigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
