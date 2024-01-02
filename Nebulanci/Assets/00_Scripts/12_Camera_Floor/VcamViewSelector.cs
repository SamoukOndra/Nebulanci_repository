using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamViewSelector : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera sideVcam;
    [SerializeField] CinemachineVirtualCamera topVcam;

    private void Awake()
    {
        if (SetUp.topdownCam)
        {
            topVcam.Priority = 10;
            sideVcam.Priority = 0;
        }
        else
        {
            topVcam.Priority = 0;
            sideVcam.Priority = 10;
        }
    }
}
