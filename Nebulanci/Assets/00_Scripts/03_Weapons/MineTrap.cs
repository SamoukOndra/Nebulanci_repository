using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrap : MonoBehaviour
{

    [SerializeField] float activateTriggerTime = 1.5f;
    private SphereCollider trigger;

    private void Awake()
    {
        trigger = GetComponentInChildren<SphereCollider>();
        trigger.enabled = false;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(activateTriggerTime);
        trigger.enabled = true;
    }

}
