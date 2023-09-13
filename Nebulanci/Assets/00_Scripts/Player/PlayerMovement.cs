using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementForce = 100f;
    [SerializeField] float rotationSpeed = 10f;

    public bool canMove = true;

    Rigidbody rigidbody;
    PlayerInput playerInput;
    PlayerControls inputActions;
    AnimatorHandler animatorHandler;


    float delta = 0;
    Vector3 moveDirection;
    Vector3 lookDirection;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lookDirection = transform.forward;
    }

    private void Update()
    {
        if (lookDirection != transform.forward.normalized)
            HandleRotation();
        
        if (animatorHandler != null)
            animatorHandler.UpdateAnimatorMove(moveDirection);
    }
    private void FixedUpdate()
    {
        if(canMove)
            MovePlayer(moveDirection);
    }

    public void Initialize()
    {
        animatorHandler = Util.GetAnimatorHandlerInChildren(gameObject);
    }

    public void SetAnimatorHandler(AnimatorHandler referedAnimatorHandler)
    {
        animatorHandler = referedAnimatorHandler;
    }

    public void OnMovement(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        moveDirection = GetMoveDirectionFromInput(moveInput);
        if (moveDirection != Vector3.zero)
            lookDirection = moveDirection;

        
    }


    
    private void MovePlayer(Vector3 moveDirection)
    {
        rigidbody.velocity = moveDirection * movementForce;
    }

    private Vector3 GetMoveDirectionFromInput(Vector2 moveInput)
    {
        float x = moveInput.x;
        float z = moveInput.y;
        Vector3 moveDirection = new(x, 0, z);
        if (moveDirection != Vector3.zero) moveDirection.Normalize();
        return moveDirection;
    }

    private void HandleRotation()
    {
        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(lookDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

        transform.rotation = targetRotation;
    }

    public void ResetMoveDirection()
    {
        moveDirection = Vector3.zero;
    }

    //private void OnDisable()
    //{
    //    animatorHandler.UpdateAnimatorMove(Vector3.zero); //nefunguje:/
    //}
}
