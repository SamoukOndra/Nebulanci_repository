using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionMaterials : MonoBehaviour
{
    protected AudioSource audioSource;
    protected AudioClip audioClip;

    public abstract void Interact(Vector3 hitPoint, Quaternion rotation, GameObject shootingPlayer = null);
    //public abstract void Interact(Vector3 hitPoint, Quaternion rotation);

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
