using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    Volume volume;
    [SerializeField] float activationLerpDuration = 1.5f;

    private void Awake()
    {
        volume = GetComponentInChildren<Volume>();
        volume.weight = 0;
    }

    private void OnEnable()
    {
        EventManager.OnGameOver += ActivateFinalVolume;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= ActivateFinalVolume;
    }

    private void ActivateFinalVolume()
    {
        StartCoroutine(ActivateVolumeSmooth(volume, activationLerpDuration));
    }

    IEnumerator ActivateVolumeSmooth(Volume volume, float duration)
    {
        float timer = 0;
        float lerp = 0;

        while(timer < duration)
        {
            float delta = Time.unscaledDeltaTime;
            timer += delta;

            lerp += (delta / duration);
            volume.weight = Mathf.Lerp(0, 1, lerp);

            yield return null;
        }
    }
}
