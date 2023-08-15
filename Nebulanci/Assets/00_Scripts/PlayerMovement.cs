using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementForce = 100f;
    [SerializeField] float rotationSpeed = 10f;

    Rigidbody rigidbody;
    PlayerInput playerInput;
    PlayerControls inputActions;


    float delta = 0;
    Vector2 moveInput;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //playerInput = GetComponent<PlayerInput>();
        //inputActions = playerInput.c;
        ////inputActions = GetComponent<PlayerControls>();
        //inputActions.Player.Movement.performed += inputActions => moveInput = inputActions.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        delta = 1;// Time.deltaTime;
        MovePlayer(moveInput, delta);
    }

    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //MovePlayer(moveInput, delta);
    }


    
    private void MovePlayer(Vector2 moveInput, float delta)
    {
        Vector3 moveDirection = GetMoveDirectionFromInput(moveInput);
        //rigidbody.AddForce(moveDirection * movementForce * delta, ForceMode.Force);
        rigidbody.velocity = moveDirection * movementForce;
        Debug.Log(/*moveDirection * movementForce */delta);

    }

    private Vector3 GetMoveDirectionFromInput(Vector2 moveInput)
    {
        float x = moveInput.x;
        float z = moveInput.y;
        Vector3 moveDirection = new Vector3(x, 0, z);
        if (moveDirection != Vector3.zero) moveDirection.Normalize();
        return moveDirection;
    }
}
