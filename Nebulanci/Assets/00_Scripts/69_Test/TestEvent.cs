using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnPlayerKill += TestIfKill;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerKill -= TestIfKill;
    }

    public void TestIfKill(GameObject killer)
    {
        Debug.Log(killer + " zabil!");
    }
}
