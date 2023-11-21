using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmHit : MonoBehaviour
{
    private float duration = 1f;
    private int id;

    [Tooltip("0 brick; 1 metal; 2 wood")]
    [SerializeField] ParticleSystem[] hits;
    private AudioClip[] audioClips;// = new AudioClip[3];

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        AudioList _list = AudioManager.audioList;
        AudioClip ap_0 = _list.cm_brick;
        AudioClip ap_1 = _list.cm_metal;
        AudioClip ap_2 = _list.cm_wood;

        audioClips = new[]{ ap_0, ap_1, ap_2};
    }

    private void OnEnable()
    {
        StartCoroutine(DisableSelfCoroutine(duration));
        PlayHit(id);
    }

    IEnumerator DisableSelfCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void PlayHit(int identifier)
    {
        if (identifier < 0 || identifier > hits.Length) return;

        hits[identifier].Play();

        Util.RandomizePitch(audioSource, .2f);
        audioSource.PlayOneShot(audioClips[identifier]);
    }

    public void SetIdentifier(int id)
    {
        this.id = id;
    }
}
