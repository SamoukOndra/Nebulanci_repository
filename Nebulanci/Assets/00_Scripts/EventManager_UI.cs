using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_UI : MonoBehaviour
{
    public delegate void PlayerAdded();
    public static event PlayerAdded OnPlayerAdded;



    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))//////////////////////
        {
            OnPlayerAdded?.Invoke();
        }
    }

}
