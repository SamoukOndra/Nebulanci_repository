using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //PlayerControls inputActions;
    public Vector2 moveInput;

    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
}
