using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCanvas : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        canvas.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.OnGatherFinalScores += EnableCanvas;
    }

    private void OnDisable()
    {
        EventManager.OnGatherFinalScores -= EnableCanvas;
    }

    private void EnableCanvas()
    {
        canvas.enabled = true;
    }
}
