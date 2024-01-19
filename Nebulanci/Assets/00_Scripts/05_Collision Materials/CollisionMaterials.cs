using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionMaterials : MonoBehaviour
{
    protected AudioSource audioSource;
    protected AudioClip audioClip;
    [SerializeField] protected bool isBulletProof = true;

    public abstract bool Interact(Vector3 hitPoint, Quaternion rotation, GameObject shootingPlayer = null);
    //return true if isBulletProofCM, else false

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool GetIsBulletProof()
    {
        return isBulletProof;
    }
}
