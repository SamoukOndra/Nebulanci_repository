using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCM : CollisionMaterials
{
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] AudioSource hitsAudioSource;

    public override void Interact(Vector3 hitPoint, Quaternion rotation, GameObject _null)
    {
        particleSystem.transform.position = hitPoint;
        particleSystem.transform.rotation = rotation;
        particleSystem.Play();

        Util.RandomizePitch(audioSource, .2f);
        audioSource.PlayOneShot(AudioManager.audioList.GetZombieScream());

        Util.RandomizePitch(hitsAudioSource, .2f);
        hitsAudioSource.PlayOneShot(AudioManager.audioList.GetFleshHit());
    }
}
