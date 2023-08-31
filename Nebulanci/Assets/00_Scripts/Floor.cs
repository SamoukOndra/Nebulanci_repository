using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public LayerMask floorLayerMask;

    [HideInInspector]
    public float spawnRaycastHeight = 5f;

    [Header("Floor boundaries")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private void OnEnable()
    {
        Util.currentFloor = this;
        
    }
}
