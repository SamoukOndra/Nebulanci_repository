using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshCM : CollisionMaterials
{
    [SerializeField] new ParticleSystem particleSystem;

    public override bool Interact(Vector3 hitPoint, Quaternion rotation, GameObject _null)
    {
        particleSystem.transform.position = hitPoint;
        particleSystem.transform.rotation = rotation;
        particleSystem.Play();

        Util.RandomizePitch(audioSource, .2f);
        audioSource.PlayOneShot(AudioManager.audioList.GetFleshHit());

        return isBulletProof;
    }
}
