using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float playerHeight;
    private float startYScale;

    [SerializeField] private Transform orientation;
    
    [Header("Walking")]
    [SerializeField] private float walkSpeed;
    
    [Header("Sprinting")]
    [SerializeField] private float sprintSpeed;
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    private bool readyToJump;
    private float jumpDelay;
    
    [Header("Crouching")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchYScale;
    
    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode sprintKey;
    [SerializeField] private KeyCode crouchKey;

    // [Header("Ground Check")]
    public bool isOnGround { private get; set; }
    private RaycastHit groundHit;
    
    [Header("Slope Handling")]
    [SerializeField] private float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rigidBody;

    [SerializeField] private MovementState currentState;
    [SerializeField] private enum MovementState
    {
        Walking,
        Sprinting,
        Crouching,
        Air
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;

        readyToJump = true;
        exitingSlope = false;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        isOnGround = false;
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, playerHeight * 0.5f + 0.3f))
            isOnGround = groundHit.transform.gameObject.CompareTag("Ground");
        
        MyInput();
        SpeedControl();
        StateHandler();
        
        if (readyToJump == false)
        {
            jumpDelay += Time.deltaTime;
            if (jumpDelay > 0.25f)
            {
                readyToJump = true;
                exitingSlope = false;
                jumpDelay = 0f;
            }
        }

        rigidBody.drag = isOnGround ? groundDrag : 0f;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && isOnGround)
        {
            readyToJump = false;
            
            Jump();
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
             transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        if (isOnGround)
        {
            if (Input.GetKey(crouchKey))
            {
                currentState = MovementState.Crouching;
                moveSpeed = crouchSpeed;
            }
            else if (Input.GetKey(sprintKey))
            {
                currentState = MovementState.Sprinting;
                moveSpeed = sprintSpeed;
            }
            else
            {
                currentState = MovementState.Walking;
                moveSpeed = walkSpeed;
            }
        }
        
        else
        {
            currentState = MovementState.Air;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope())
        {
            rigidBody.useGravity = false;
            
            if (exitingSlope == false)
            {
                rigidBody.AddForce(GetSlopeMoveDirection() * ( 20f * moveSpeed ), ForceMode.Force);
                
                if (rigidBody.velocity.y > 0f)
                    rigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        else
        {
            rigidBody.useGravity = true;
            
            if (isOnGround)
                rigidBody.AddForce(moveDirection.normalized * ( 10f * moveSpeed ), ForceMode.Force);

            else if (!isOnGround)
                rigidBody.AddForce(moveDirection.normalized * ( 10f * moveSpeed * airMultiplier ), ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        if (OnSlope() && exitingSlope == false)
        {
            if (rigidBody.velocity.magnitude > moveSpeed)
                rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            if (slopeHit.transform.gameObject.CompareTag("Ground"))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0f;
            }
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
