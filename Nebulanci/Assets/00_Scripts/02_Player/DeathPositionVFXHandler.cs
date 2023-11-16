using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPositionVFXHandler : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;
    private AudioSource audioSource;
    private AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = AudioManager.audioList.playerDeath;
    }

    public void PlayDeathVFX()
    {
        particleSystem.Play();
        audioSource.PlayOneShot(audioClip);
    }


}
