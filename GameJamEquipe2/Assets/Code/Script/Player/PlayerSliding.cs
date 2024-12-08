using System;
using System.Collections;
using System.Collections.Generic;
using Code.Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSliding : MonoBehaviour
{
    private bool keyPressed;
    
    [Header("Sliding")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideForce;
    [SerializeField] private float maxSlideTime;
    [SerializeField, Range(0f, 1f)] private float slideYMultiplier;
    private float slideTimer;
    
    [Header("Crouching")]
    [SerializeField] private float crouchSpeed;
    [SerializeField, Range(0f, 1f)] private float crouchYMultiplier;
    
    [Header("References")]
    private Transform player;
    private Transform orientation;
    private Rigidbody rigidBody;
    private PlayerMovement playerMovement;

    private void Start()
    {
        player = transform;
        rigidBody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        orientation = playerMovement.orientation;
    }

    private void FixedUpdate()
    {
        if (keyPressed == false) return;
        
        if (playerMovement.currentState == MovementState.Sliding) SlidingMovement();
        else if (playerMovement.currentState == MovementState.Crouching) playerMovement.desiredMoveSpeed = crouchSpeed;
    }
    
    

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * playerMovement.verticalInput + orientation.right * playerMovement.horizontalInput;
        
        bool onSlope = playerMovement.OnSlope();
        
        if (rigidBody.velocity.y < 0.1f && onSlope) playerMovement.desiredMoveSpeed = slideSpeed;
        else playerMovement.desiredMoveSpeed = playerMovement.sprintSpeed;
        
        if (rigidBody.velocity.y > -0.1f || onSlope == false)
        {
            rigidBody.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
        
            slideTimer -= Time.deltaTime;
        }
        else rigidBody.AddForce(playerMovement.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        
        if (slideTimer <= 0f) StopSlide();
    }
    
    

    private void StartSlide()
    {
        playerMovement.currentState = MovementState.Sliding;

        player.localScale = new Vector3(player.localScale.x, slideYMultiplier, player.localScale.z);
        rigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void StartCrouch()
    {
        playerMovement.currentState = MovementState.Crouching;

        player.localScale = new Vector3(player.localScale.x, crouchYMultiplier, player.localScale.z);
        rigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }
    
    

    private void StopSlide()
    {
        switch (playerMovement.currentState)
        {
            case MovementState.Sliding:
                if (keyPressed)
                {
                    player.localScale = new Vector3(player.localScale.x, crouchYMultiplier, player.localScale.z);
                    playerMovement.currentState = MovementState.Crouching;
                    return;
                }
                playerMovement.currentState = MovementState.Walking;
                break;
            case MovementState.Crouching:
                playerMovement.currentState = MovementState.Walking;
                break;
        }
        
        player.localScale = new Vector3(player.localScale.x, playerMovement.yScale, player.localScale.z);
    }

    public void KeyPressed(InputAction.CallbackContext ctx)
    {
        keyPressed = ctx.performed;

        if (keyPressed)
        {
            if (playerMovement.horizontalInput != 0f || playerMovement.verticalInput != 0f) StartSlide();
            else StartCrouch();
        }
        else StopSlide();
    }
}
