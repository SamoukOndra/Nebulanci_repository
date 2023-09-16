using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera cvcamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    [Header("Camera shake")]
    [SerializeField] float maxAmplitude = 2f;
    [SerializeField] float maxFrequency = 2f;
    [SerializeField] float duration = 0.5f;

    private void Awake()
    {
        cvcamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cvcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        EventManager.OnExplosion += CameraShake;
    }

    private void OnDisable()
    {
        EventManager.OnExplosion -= CameraShake;
    }


    public void CameraShake(Vector3 position)
    {
        StartCoroutine(CameraShakeCoroutine());
    }

    IEnumerator CameraShakeCoroutine()
    {
        float timer = 0;
        float lerp;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            lerp = timer / duration;

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(maxAmplitude, 0, lerp);
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = Mathf.Lerp(maxFrequency, 0, lerp);

            yield return null;
        }
    }
}
