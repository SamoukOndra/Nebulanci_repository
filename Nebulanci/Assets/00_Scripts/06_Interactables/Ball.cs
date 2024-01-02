using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 startPosition = new Vector3(0, 3, 0);
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Respawn();
    }

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameObject.transform.position = startPosition;
    }
}
