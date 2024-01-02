using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    Vector3 target;
    float colliderOffset = 0.1f;

    [SerializeField] LayerMask layerMasks;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, layerMasks))
        {
            target = hit.point + transform.forward * colliderOffset;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target);
    }
}
