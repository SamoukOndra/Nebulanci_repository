using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    [SerializeField] float deactivateInSecs;

    private void OnEnable()
    {
        StartCoroutine(DeactivateCoroutine(deactivateInSecs));
    }

    IEnumerator DeactivateCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
