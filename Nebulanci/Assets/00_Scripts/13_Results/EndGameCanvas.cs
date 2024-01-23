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
        EventManager.OnGameOver += EnableCanvas;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= EnableCanvas;
    }

    private void EnableCanvas()
    {
        canvas.enabled = true;
        Cursor.visible = true;
    }
}
