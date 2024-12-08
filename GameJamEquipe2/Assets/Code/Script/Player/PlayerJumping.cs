using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    private bool keyPressed;
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    private bool readyToJump = true;
    private float jumpTimer;

    [Header("References")]
    private Rigidbody rigidBody;
    private PlayerMovement playerMovement;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (keyPressed) Jump();
        JumpDelay();
    }

    private void Jump()
    {
        if ((readyToJump && playerMovement.onGround) == false) return;
        
        readyToJump = false;
        playerMovement.exitingSlope = true;
        
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void JumpDelay()
    {
        if (readyToJump) return;
        
        jumpTimer += Time.deltaTime;
        
        if (jumpTimer <= 0.25f) return;
        
        readyToJump = true;
        playerMovement.exitingSlope = false;
        jumpTimer = 0f;
    }
    
    public void SetKeyPressed(InputAction.CallbackContext ctx)
    {
        keyPressed = ctx.performed;
    }
}
