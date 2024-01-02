using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetVcamView : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    private void Awake()
    {
        toggle.isOn = GetTheView();
    }

    private void OnEnable()
    {
        SetTheView();
    }

    public void SetTheView()
    {
        SetUp.topdownCam = toggle.isOn;
    }

    private bool GetTheView()
    {
        return SetUp.topdownCam;
    }
}
