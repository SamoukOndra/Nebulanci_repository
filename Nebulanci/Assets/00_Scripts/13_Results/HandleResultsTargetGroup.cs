using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandleResultsTargetGroup : MonoBehaviour
{
    public static HandleResultsTargetGroup singleton;

    private CinemachineTargetGroup targetGroup;

    private void Awake()
    {
        singleton = this;
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    public void AddTarget(GameObject target)
    {
        targetGroup.AddMember(target.transform, 1, 0);
    }
}
