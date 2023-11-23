using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDeath : MonoBehaviour
{
    new ParticleSystem particleSystem;
    AudioSource audioSource;
    AudioClip audioClip;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void OnEnable()
    {
        particleSystem.Play();
        Util.RandomizePitch(audioSource, .2f);
        audioSource.PlayOneShot(audioClip);
        DisableObjectInSecs(gameObject, 1);
    }

    public void Initialize()
    {
        audioClip = AudioManager.audioList.npcDeath;
    }


    public void DisableObjectInSecs(GameObject gameObject, float secs)
    {
        StartCoroutine(Coroutine(gameObject, secs));

        IEnumerator Coroutine(GameObject go, float time)
        {
            yield return new WaitForSeconds(time);
            go.SetActive(false);
        }
    }

    
}
