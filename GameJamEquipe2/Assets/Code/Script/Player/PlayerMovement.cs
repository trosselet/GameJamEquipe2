using System;
using System.Collections;
using System.Collections.Generic;
using Code.Script;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public MovementState currentState { get; set; }
    
    [Header("Speeds")]
    [SerializeField] private float walkSpeed;
    [field: SerializeField] public float sprintSpeed { get; private set; }
    private float moveSpeed;
    public float desiredMoveSpeed { private get; set; }
    private float lastDesiredMoveSpeed;
    
    [Header("Resistance")]
    [SerializeField] private float groundDrag;
    [SerializeField, Range(0f, 1f)] private float airMultiplier;
    
    [Header("Size")]
    [SerializeField] private float playerHeight;
    public float yScale { get; private set; }
    
    [field: Header("Inputs")]
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }

    [field: Header("Ground Check")]
    public bool onGround { get; set; }
    
    [Header("Slope Handling")]
    [SerializeField, Range(0f, 90f)] private float maxSlopeAngle;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float slopeMultiplier;
    public bool exitingSlope { private get; set; }
    private RaycastHit slopeHit;

    [Header("References")]
    public Transform orientation;
    private Rigidbody rigidBody;

    private CinemachineImpulseSource impulseSource;

    [Header("Speed Effect")] 
    [SerializeField] private FullScreenPassRendererFeature speedEffect;
    [SerializeField] private Material speedMaterial;
    private Material speedMaterialCopy;
    private CinemachineVirtualCamera camera;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        // rigidBody.freezeRotation = true;
        
        impulseSource = GetComponent<CinemachineImpulseSource>();
        speedMaterialCopy = new Material(speedMaterial);
        speedEffect.passMaterial = speedMaterialCopy;
        camera = GetComponentInChildren<CinemachineVirtualCamera>();

        yScale = transform.localScale.y;
    }

    private void OnDestroy()
    {
        speedEffect.passMaterial = speedMaterial;
    }

    private void Update()
    {
        // onGround = false;
        // if (Physics.Raycast(transform.position, Vector3.down, out groundHit, playerHeight * 0.5f + 0.3f))
        //     onGround = groundHit.transform.gameObject.CompareTag("Ground");
        // if (transform.position.y <= -20)
        // {
        //     KillPlayer();
        //     return;
        // }
        
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, playerHeight * 0.5f + 0.3f))
            if (isOnGround == false)
                impulseSource.GenerateImpulse(0.4f);
        
        MyInput();
        SpeedControl();
        StateHandler();

        rigidBody.drag = onGround ? groundDrag : 0f;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void StateHandler()
    {
        if (onGround)
        {
            if (currentState == MovementState.Walking)
            {
                desiredMoveSpeed = walkSpeed;
            }
            if (currentState == MovementState.Sprinting)
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0f)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else moveSpeed = desiredMoveSpeed;

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private void KillPlayer()
    {
        
    }
    
    private void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        speedMaterialCopy.SetFloat("_Speed", rigidBody.velocity.magnitude * 0.02f);

        camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, 100 + rigidBody.velocity.magnitude * 1.2f,
            Time.deltaTime * 4.0f);
        if (OnSlope())
        {
            rigidBody.useGravity = false;
            
            if (exitingSlope == false)
            {
                rigidBody.AddForce(GetSlopeMoveDirection(moveDirection) * ( 20f * moveSpeed ), ForceMode.Force);
                
                if (rigidBody.velocity.y > 0f)
                    rigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else
        {
            rigidBody.useGravity = true;
            
            if (onGround) rigidBody.AddForce(moveDirection.normalized * ( 10f * moveSpeed ), ForceMode.Force);
            else rigidBody.AddForce(moveDirection.normalized * ( 10f * moveSpeed * airMultiplier ), ForceMode.Force);
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



    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (currentState == MovementState.Sliding || currentState == MovementState.Crouching) return;
        
        if (ctx.performed) currentState = MovementState.Sprinting;
        else if (ctx.canceled) currentState = MovementState.Walking;
    }

    
    
    public bool OnSlope()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f);
        if (hits.Length <= 0) return false;
        
        for ( int i = 0; i < hits.Length; ++i )
        {
            if (hits[i].transform.gameObject.CompareTag("Ground") == false) continue;
                
            slopeHit = hits[i];
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle <= maxSlopeAngle && angle != 0f;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0f;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;
        
        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
        
            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1f + slopeAngle / 90f;
        
                time += Time.deltaTime * speedMultiplier * slopeMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedMultiplier;
            
            yield return null;
        }
        
        moveSpeed = desiredMoveSpeed;
    }
}
